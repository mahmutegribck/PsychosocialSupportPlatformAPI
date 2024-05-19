using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Users
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUser(string id);
        Task<ApplicationUser?> GetUserBySlug(string userSlug);
        Task<IdentityResult> DeleteUser(string id);
        Task<IdentityResult> UpdateDoctor(string currentUserID, Doctor doctor);
        Task<IdentityResult> UpdatePatient(string currentUserID, Patient patient);
        Task<IEnumerable<Patient>> GetAllPatientsByDoctorId(string doctorId);
        Task AddDoctorTitle(DoctorTitle doctorTitle);
        Task DeleteDoctorTitle(DoctorTitle doctorTitle);
        Task<bool> CheckDoctorTitle(string doctorTitle);
        Task<DoctorTitle?> GetDoctorTitleById(int doctorTitleId);
        Task<IEnumerable<DoctorTitle>> GetAllDoctorTitles();
    }
}
