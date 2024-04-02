using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;

namespace PsychosocialSupportPlatformAPI.Business.Users
{
    public interface IUserService
    {
        Task<object> GetUserByID(string userId);
        Task<IdentityResult> DeleteUser(string id);
        Task<IdentityResult> UpdateDoctor(string currentUserID, UpdateDoctorDTO updateDoctorDTO);
        Task<IdentityResult> UpdatePatient(string currentUserID, UpdatePatientDTO updatePatientDTO);


    }
}
