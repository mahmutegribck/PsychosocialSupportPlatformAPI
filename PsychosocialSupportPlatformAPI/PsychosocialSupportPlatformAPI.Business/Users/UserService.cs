using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.Admin;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
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
        private readonly IMailService _mailService;


        public UserService(
            IUserRepository userRepository, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            IConfiguration config,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _mailService = mailService;
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDTO, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(currentUserId) ?? throw new Exception("Kullanıcı Bulunamadı.");

            if (!await _userManager.CheckPasswordAsync(user, changePasswordDTO.OldPassword))
            {
                throw new Exception("Eski Şifre Yanlış.");
            }

            if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmPassword) throw new Exception("Yeni Şifre Doğrulanamdı.");

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception($"Şifre Değiştirme İşlemi Başarısız Oldu. {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
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

        public async Task<object> GetUserBySlug(string userSlug)
        {
            ApplicationUser? user = await _userRepository.GetUserBySlug(userSlug) ?? throw new Exception("Kullanıcı Bulunamadı");

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
            if (await _userRepository.GetDoctorTitleById(updateDoctorDTO.DoctorTitleId) == null) throw new Exception("Ünvan Bulunamadı");

            return await _userRepository.UpdateDoctor(currentUserID, _mapper.Map<Doctor>(updateDoctorDTO));
        }

        public async Task<IdentityResult> UpdatePatient(string currentUserID, UpdatePatientDTO updatePatientDTO)
        {
            return await _userRepository.UpdatePatient(currentUserID, _mapper.Map<Patient>(updatePatientDTO));
        }

        public async Task UploadProfileImage(IFormFile formFile, string userId, string rootPath)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("Kullanıcı Bulunamadı");
            if (user.ProfileImagePath != null)
            {
                File.Delete(user.ProfileImagePath);
            }

            string basePath = rootPath + "\\Images\\UserProfileImages\\";
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            string extension = Path.GetExtension(formFile.FileName);
            string newFileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            string imagePath = string.Concat($"{basePath}", newFileName);
            string imageUrl = $"{_config["Urls:DevBaseUrl"]}/Images/UserProfileImages/{newFileName}";

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await formFile.CopyToAsync(stream);

            user.ProfileImageUrl = imageUrl;
            user.ProfileImagePath = imagePath;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteProfileImage(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("Kullanıcı Bulunamadı");
            if (user.ProfileImagePath != null)
            {
                File.Delete(user.ProfileImagePath);
            }
            user.ProfileImagePath = null;
            user.ProfileImageUrl = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task AddDoctorTitle(AddDoctorTitleDTO addDoctorTitleDTO)
        {
            DoctorTitle doctorTitle = _mapper.Map<DoctorTitle>(addDoctorTitleDTO) ?? throw new Exception();
            if (await _userRepository.CheckDoctorTitle(doctorTitle.Title)) throw new Exception("Ünvan Kaydı Mevcut");
            await _userRepository.AddDoctorTitle(doctorTitle);
        }

        public async Task DeleteDoctorTitle(int doctorTitleId)
        {
            DoctorTitle? doctorTitle = await _userRepository.GetDoctorTitleById(doctorTitleId) ?? throw new Exception("Ünvan Bulunamadı");

            await _userRepository.DeleteDoctorTitle(doctorTitle);
        }

        public async Task<GetDoctorTitleDTO?> GetDoctorTitleById(int doctorTitleId)
        {
            return _mapper.Map<GetDoctorTitleDTO>(await _userRepository.GetDoctorTitleById(doctorTitleId));
        }

        public async Task<IEnumerable<GetDoctorTitleDTO>> GetAllDoctorTitles()
        {
            return _mapper.Map<IEnumerable<GetDoctorTitleDTO>>(await _userRepository.GetAllDoctorTitles());
        }

        public async Task<IEnumerable<GetDoctorDto>> GetAllUnConfirmedDoctor()
        {
            return _mapper.Map<IEnumerable<GetDoctorDto>>(await _userRepository.GetAllUnConfirmedDoctor());
        }

        public async Task ConfirmDoctor(string doctorUserName)
        {
            Doctor doctor = _mapper.Map<Doctor>(await _userRepository.GetUserBySlug(doctorUserName)) ?? throw new Exception("Doktor Bulunamadı");

            await _userRepository.ConfirmDoctor(doctor);

            await _mailService.SendEmailToDoctorForConfirmationAccount(doctor);
        }
    }
}
