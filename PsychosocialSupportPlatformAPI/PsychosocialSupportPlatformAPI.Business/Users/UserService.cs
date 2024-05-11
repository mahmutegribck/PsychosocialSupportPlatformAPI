using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.Admin;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.Business.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;


        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<GetPatientDto>> GetAllPatientsByDoctorId(string doctorId)
        {
            return _mapper.Map<IEnumerable<GetPatientDto>>(await _userRepository.GetAllPatientsByDoctorId(doctorId));
        }

        public async Task<object> GetUserByID(string userId)
        {
            var user = await _userRepository.GetUser(userId);

            if (await _userManager.IsInRoleAsync(user, _config["Roles:Patient"]))
            {
                return _mapper.Map<GetPatientDto>(user);

            }
            else if (await _userManager.IsInRoleAsync(user, _config["Roles:Doctor"]))
            {
                return _mapper.Map<GetDoctorDto>(user);

            }
            return _mapper.Map<GetAdminDto>(user);

        }

        public async Task<IdentityResult> UpdateDoctor(string currentUserID, UpdateDoctorDTO updateDoctorDTO)
        {
            return await _userRepository.UpdateDoctor(currentUserID, _mapper.Map<Doctor>(updateDoctorDTO));
        }

        public async Task<IdentityResult> UpdatePatient(string currentUserID, UpdatePatientDTO updatePatientDTO)
        {
            return await _userRepository.UpdatePatient(currentUserID, _mapper.Map<Patient>(updatePatientDTO));
        }
    }
}
