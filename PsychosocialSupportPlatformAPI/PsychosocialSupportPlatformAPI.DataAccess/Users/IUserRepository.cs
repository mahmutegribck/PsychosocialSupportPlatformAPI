using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Users
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUser(string id, CancellationToken cancellationToken);
        Task<ApplicationUser?> GetUserBySlug(string userSlug, CancellationToken cancellationToken);
        Task<Patient?> GetPatientBySlug(string patientSlug);
        Task<IEnumerable<Patient>> GetAllPatients(CancellationToken cancellationToken);
        Task<IEnumerable<Doctor>> GetAllDoctors(CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUser(string id, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateDoctor(string currentUserId, Doctor doctor);
        Task<IdentityResult> UpdateDoctorTitle(Doctor doctor, DoctorTitle doctorTitle);
        Task<IdentityResult> UpdatePatient(string currentUserId, Patient patient, CancellationToken cancellationToken);
        Task<IEnumerable<Patient>> GetAllPatientsByDoctorId(string doctorId);
        Task AddDoctorTitle(DoctorTitle doctorTitle, CancellationToken cancellationToken);
        Task DeleteDoctorTitle(DoctorTitle doctorTitle, CancellationToken cancellationToken);
        Task<bool> CheckDoctorTitle(string doctorTitle, CancellationToken cancellationToken);
        Task<DoctorTitle?> GetDoctorTitleById(int doctorTitleId, CancellationToken cancellationToken);
        Task<IEnumerable<DoctorTitle>> GetAllDoctorTitles(CancellationToken cancellationToken);
        Task<IEnumerable<Doctor>> GetAllUnConfirmedDoctor(CancellationToken cancellationToken);
        Task ConfirmDoctor(Doctor doctor, CancellationToken cancellationToken);
    }
}
