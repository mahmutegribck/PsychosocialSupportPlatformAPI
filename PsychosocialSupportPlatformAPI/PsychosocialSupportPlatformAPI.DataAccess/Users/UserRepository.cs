using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PsychosocialSupportPlatformDBContext _context;
        private readonly UserManager<Doctor> _doctorManager;
        private readonly UserManager<Patient> _patientManager;

        public UserRepository(
            UserManager<ApplicationUser> userManager,
            PsychosocialSupportPlatformDBContext context,
            UserManager<Doctor> doctorManager,
            UserManager<Patient> patientManager)
        {
            _userManager = userManager;
            _context = context;
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }


        public async Task AddDoctorTitle(DoctorTitle doctorTitle, CancellationToken cancellationToken)
        {
            await _context.DoctorTitles.AddAsync(doctorTitle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteDoctorTitle(DoctorTitle doctorTitle, CancellationToken cancellationToken)
        {
            _context.DoctorTitles.Remove(doctorTitle);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<bool> CheckDoctorTitle(string doctorTitle, CancellationToken cancellationTokenc)
        {
            return await _context.DoctorTitles.AnyAsync(t => t.Title == doctorTitle, cancellationTokenc);

        }


        public async Task<DoctorTitle?> GetDoctorTitleById(int doctorTitleId, CancellationToken cancellationToken)
        {
            return await _context.DoctorTitles.AsNoTracking().FirstOrDefaultAsync(t => t.Id == doctorTitleId, cancellationToken);
        }


        public async Task<IdentityResult> DeleteUser(string id, CancellationToken cancellationToken)
        {
            var deletUser = await GetUser(id, cancellationToken);
            if (deletUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            var userMessages = await _context.Messages
                .AsNoTracking()
                .Where(m => m.SenderId == id || m.ReceiverId == id)
                .ToListAsync(cancellationToken);

            _context.Messages.RemoveRange(userMessages);


            IdentityResult result = await _userManager.DeleteAsync(deletUser);
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<IEnumerable<Patient>> GetAllPatientsByDoctorId(string doctorId)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(p => p.AppointmentSchedules
                .Any(a => a.DoctorId == doctorId))
                .Distinct()
                .ToListAsync();
        }


        public async Task<ApplicationUser?> GetUser(string id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<ApplicationUser?> GetUserBySlug(string userSlug, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.UserName == userSlug)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<Patient?> GetPatientBySlug(string patientSlug)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(a => a.UserName == patientSlug)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllPatients(CancellationToken cancellationToken)
        {
            return await _context.Patients
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctors(CancellationToken cancellationToken)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Confirmed == true)
                .ToListAsync(cancellationToken);
        }


        public async Task<IdentityResult> UpdateDoctor(string currentUserId, Doctor doctor)
        {
            Doctor updatedDoctor = await _doctorManager.FindByIdAsync(currentUserId);
            if (updatedDoctor == null) { return IdentityResult.Failed(); }

            updatedDoctor.Name = doctor.Name;
            updatedDoctor.Surname = doctor.Surname;
            updatedDoctor.PhoneNumber = doctor.PhoneNumber;

            IdentityResult result = await _doctorManager.UpdateAsync(updatedDoctor);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<IdentityResult> UpdateDoctorTitle(Doctor doctor, DoctorTitle doctorTitle)
        {
            doctor.DoctorTitleId = doctorTitle.Id;
            IdentityResult result = await _doctorManager.UpdateAsync(doctor);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<IdentityResult> UpdatePatient(string currentUserId, Patient patient, CancellationToken cancellationToken)
        {
            Patient updatedPatient = await _context.Patients.Where(p => p.Id == currentUserId).FirstAsync(cancellationToken);
            if (updatedPatient == null) { return IdentityResult.Failed(); }

            updatedPatient.Name = patient.Name;
            updatedPatient.Surname = patient.Surname;
            updatedPatient.PhoneNumber = patient.PhoneNumber;

            IdentityResult result = await _patientManager.UpdateAsync(updatedPatient);
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<IEnumerable<DoctorTitle>> GetAllDoctorTitles(CancellationToken cancellationToken)
        {
            return await _context.DoctorTitles.AsNoTracking().ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<Doctor>> GetAllUnConfirmedDoctor(CancellationToken cancellationToken)
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Confirmed == false)
                .ToListAsync(cancellationToken);
        }


        public async Task ConfirmDoctor(Doctor doctor, CancellationToken cancellationToken)
        {
            doctor.Confirmed = true;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
