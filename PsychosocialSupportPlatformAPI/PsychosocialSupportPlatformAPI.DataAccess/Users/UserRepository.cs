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

        public UserRepository(UserManager<ApplicationUser> userManager, PsychosocialSupportPlatformDBContext context, UserManager<Doctor> doctorManager, UserManager<Patient> patientManager)
        {
            _userManager = userManager;
            _context = context;
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var deletUser = await GetUser(id);
            if (deletUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            var userMessages = await _context.Messages.Where(m => m.SenderId == id || m.ReceiverId == id).ToListAsync();
            _context.Messages.RemoveRange(userMessages);


            IdentityResult result = await _userManager.DeleteAsync(deletUser);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsByDoctorId(string doctorId)
        {
            return await _context.Patients.Where(p => p.AppointmentSchedules.Any(a => a.DoctorId == doctorId)).Distinct().AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> UpdateDoctor(string currentUserID, Doctor doctor)
        {
            Doctor updatedDoctor = await _doctorManager.FindByIdAsync(currentUserID);
            if (updatedDoctor == null) { return IdentityResult.Failed(); }

            updatedDoctor.Name = doctor.Name;
            updatedDoctor.Surname = doctor.Surname;
            updatedDoctor.Title = doctor.Title;
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
    }
}
