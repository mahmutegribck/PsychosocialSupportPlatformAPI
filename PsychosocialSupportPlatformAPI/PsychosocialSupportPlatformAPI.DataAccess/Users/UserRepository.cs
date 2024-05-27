﻿using Microsoft.AspNetCore.Identity;
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


        public async Task AddDoctorTitle(DoctorTitle doctorTitle)
        {
            await _context.DoctorTitles.AddAsync(doctorTitle);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteDoctorTitle(DoctorTitle doctorTitle)
        {
            _context.DoctorTitles.Remove(doctorTitle);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> CheckDoctorTitle(string doctorTitle)
        {
            return await _context.DoctorTitles.AnyAsync(t => t.Title == doctorTitle);

        }


        public async Task<DoctorTitle?> GetDoctorTitleById(int doctorTitleId)
        {
            return await _context.DoctorTitles.AsNoTracking().FirstOrDefaultAsync(t => t.Id == doctorTitleId);
        }


        public async Task<IdentityResult> DeleteUser(string id)
        {
            var deletUser = await GetUser(id);
            if (deletUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            var userMessages = await _context.Messages
                .AsNoTracking()
                .Where(m => m.SenderId == id || m.ReceiverId == id)
                .ToListAsync();

            _context.Messages.RemoveRange(userMessages);


            IdentityResult result = await _userManager.DeleteAsync(deletUser);
            await _context.SaveChangesAsync();
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


        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }


        public async Task<ApplicationUser?> GetUserBySlug(string userSlug)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.UserName == userSlug)
                .FirstOrDefaultAsync();
        }


        public async Task<Patient?> GetPatientBySlug(string patientSlug)
        {
            return await _context.Patients
                .AsNoTracking()
                .Where(a => a.UserName == patientSlug)
                .FirstOrDefaultAsync();
        }


        public async Task<IdentityResult> UpdateDoctor(string currentUserID, Doctor doctor)
        {
            Doctor updatedDoctor = await _doctorManager.FindByIdAsync(currentUserID);
            if (updatedDoctor == null) { return IdentityResult.Failed(); }

            updatedDoctor.Name = doctor.Name;
            updatedDoctor.Surname = doctor.Surname;
            updatedDoctor.DoctorTitle.Title = doctor.DoctorTitle.Title;
            updatedDoctor.PhoneNumber = doctor.PhoneNumber;

            IdentityResult result = await _doctorManager.UpdateAsync(updatedDoctor);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<IdentityResult> UpdatePatient(string currentUserID, Patient patient)
        {
            Patient updatedPatient = await _patientManager.FindByIdAsync(currentUserID);
            if (updatedPatient == null) { return IdentityResult.Failed(); }

            updatedPatient.Name = patient.Name;
            updatedPatient.Surname = patient.Surname;
            updatedPatient.PhoneNumber = patient.PhoneNumber;

            IdentityResult result = await _patientManager.UpdateAsync(updatedPatient);
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task<IEnumerable<DoctorTitle>> GetAllDoctorTitles()
        {
            return await _context.DoctorTitles.AsNoTracking().ToListAsync();
        }


        public async Task<IEnumerable<Doctor>> GetAllUnConfirmedDoctor()
        {
            return await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Confirmed == false)
                .ToListAsync();
        }


        public async Task ConfirmDoctor(Doctor doctor)
        {
            doctor.Confirmed = true;
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
