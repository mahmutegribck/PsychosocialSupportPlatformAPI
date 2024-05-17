using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;

namespace PsychosocialSupportPlatformAPI.Business.Users
{
    public interface IUserService
    {
        Task<object> GetUserByID(string userId);
        Task<object> GetUserBySlug(string userSlug);

        Task<IdentityResult> DeleteUser(string id);
        Task<IdentityResult> UpdateDoctor(string currentUserID, UpdateDoctorDTO updateDoctorDTO);
        Task<IdentityResult> UpdatePatient(string currentUserID, UpdatePatientDTO updatePatientDTO);

        Task<IEnumerable<GetPatientDto>> GetAllPatientsByDoctorId(string doctorId);

        Task UploadProfileImage(IFormFile formFile, string userId, string path);
        Task DeleteProfileImage(string userId);

        Task ChangePassword(ChangePasswordDTO changePasswordDTO, string currentUserId);


    }
}
