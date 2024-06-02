using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;

namespace PsychosocialSupportPlatformAPI.Business.Users
{
    public interface IUserService
    {
        Task<object> GetUserByID(string userId, CancellationToken cancellationToken);
        Task<object> GetUserBySlug(string userSlug, CancellationToken cancellationToken);
        Task<IEnumerable<GetPatientDto>> GetAllPatients(CancellationToken cancellationToken);
        Task<IEnumerable<GetDoctorDto>> GetAllDoctors(CancellationToken cancellationToken);
        Task<IdentityResult> DeleteUser(string id, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateDoctor(string doctorId, UpdateDoctorDTO updateDoctorDTO, CancellationToken cancellationToken);
        Task<IdentityResult> UpdateDoctorTitle(string doctorId, int doctorTitleId, CancellationToken cancellationToken);
        Task<IdentityResult> UpdatePatient(string patientId, UpdatePatientDTO updatePatientDTO, CancellationToken cancellationToken);
        Task<IEnumerable<GetPatientDto>> GetAllPatientsByDoctorId(string doctorId);
        Task UploadProfileImage(IFormFile formFile, string userId, CancellationToken cancellationToken);
        Task DeleteProfileImage(string userId, CancellationToken cancellationToken);
        Task ChangePassword(ChangePasswordDTO changePasswordDTO, string currentUserId, CancellationToken cancellationToken);
        Task AddDoctorTitle(AddDoctorTitleDTO AddDoctorTitleDTO, CancellationToken cancellationToken);
        Task DeleteDoctorTitle(int doctorTitleId, CancellationToken cancellationToken);
        Task<GetDoctorTitleDTO?> GetDoctorTitleById(int doctorTitleId, CancellationToken cancellationToken);
        Task<IEnumerable<GetDoctorTitleDTO>> GetAllDoctorTitles(CancellationToken cancellationToken);
        Task<IEnumerable<GetDoctorDto>> GetAllUnConfirmedDoctor(CancellationToken cancellationToken);
        Task ConfirmDoctor(string doctorUserName, CancellationToken cancellationToken);
    }
}
