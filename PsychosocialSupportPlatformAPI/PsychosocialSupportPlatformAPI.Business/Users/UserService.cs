using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<Doctor> _doctorManager;
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;


        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            UserManager<Doctor> doctorManager,
            IConfiguration config,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _doctorManager = doctorManager;
            _config = config;
            _mailService = mailService;
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDTO, string currentUserId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı.");

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

        public async Task<IdentityResult> DeleteUser(string id, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUser(id, cancellationToken);
        }

        public async Task<IEnumerable<GetPatientDto>> GetAllPatientsByDoctorId(string doctorId, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetPatientDto>>(await _userRepository.GetAllPatientsByDoctorId(doctorId, cancellationToken));
        }

        public async Task<object> GetUserByID(string userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(userId, cancellationToken);

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

        public async Task<object> GetUserBySlug(string userSlug, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userRepository.GetUserBySlug(userSlug, cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı");

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

        public async Task<IdentityResult> UpdateDoctor(string doctorId, UpdateDoctorDTO updateDoctorDTO, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdateDoctor(doctorId, _mapper.Map<Doctor>(updateDoctorDTO), cancellationToken);
        }

        public async Task<IdentityResult> UpdateDoctorTitle(string doctorId, int doctorTitleId, CancellationToken cancellationToken)
        {
            DoctorTitle? doctorTitle = await _userRepository.GetDoctorTitleById(doctorTitleId, cancellationToken) ?? throw new Exception("Ünvan Bulunamadı");
            Doctor? doctor = await _doctorManager.Users.Where(d => d.Id == doctorId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Doktor Bulunamadı");

            return await _userRepository.UpdateDoctorTitle(doctor, doctorTitle, cancellationToken);
        }

        public async Task<IdentityResult> UpdatePatient(string patientId, UpdatePatientDTO updatePatientDTO, CancellationToken cancellationToken)
        {
            return await _userRepository.UpdatePatient(patientId, _mapper.Map<Patient>(updatePatientDTO), cancellationToken);
        }

        public async Task UploadProfileImage(IFormFile formFile, string userId, CancellationToken cancellationToken)
        {
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            ApplicationUser user = await _userManager.Users
                .Where(u => u.Id == userId)
                .FirstAsync(cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı");

            if (user.ProfileImagePath != null)
            {
                File.Delete(user.ProfileImagePath);
            }

            string basePath = rootPath + "\\Images\\UserProfileImages\\";
            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            string extension = Path.GetExtension(formFile.FileName).ToLower();
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            {
                throw new ArgumentException("Yalnızca JPG, JPEG ve PNG Dosyaları Desteklenmektedir");
            }
            string newFileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            string imagePath = string.Concat($"{basePath}", newFileName);
            string imageUrl = $"{_config["Urls:BaseUrl"]}/Images/UserProfileImages/{newFileName}";

            using (var stream = new FileStream(imagePath, FileMode.Create))
                await formFile.CopyToAsync(stream, cancellationToken);

            user.ProfileImageUrl = imageUrl;
            user.ProfileImagePath = imagePath;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteProfileImage(string userId, CancellationToken cancellationToken)
        {
            ApplicationUser user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı");
            if (user.ProfileImagePath != null)
            {
                File.Delete(user.ProfileImagePath);
            }
            user.ProfileImagePath = null;
            user.ProfileImageUrl = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task AddDoctorTitle(AddDoctorTitleDTO addDoctorTitleDTO, CancellationToken cancellationToken)
        {
            DoctorTitle doctorTitle = _mapper.Map<DoctorTitle>(addDoctorTitleDTO) ?? throw new Exception();
            if (await _userRepository.CheckDoctorTitle(doctorTitle.Title, cancellationToken)) throw new Exception("Ünvan Kaydı Mevcut");
            await _userRepository.AddDoctorTitle(doctorTitle, cancellationToken);
        }

        public async Task DeleteDoctorTitle(int doctorTitleId, CancellationToken cancellationToken)
        {
            DoctorTitle? doctorTitle = await _userRepository.GetDoctorTitleById(doctorTitleId, cancellationToken) ?? throw new Exception("Ünvan Bulunamadı");

            await _userRepository.DeleteDoctorTitle(doctorTitle, cancellationToken);
        }

        public async Task<GetDoctorTitleDTO?> GetDoctorTitleById(int doctorTitleId, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetDoctorTitleDTO>(await _userRepository.GetDoctorTitleById(doctorTitleId, cancellationToken));
        }

        public async Task<IEnumerable<GetDoctorTitleDTO>> GetAllDoctorTitles(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetDoctorTitleDTO>>(await _userRepository.GetAllDoctorTitles(cancellationToken));
        }

        public async Task<IEnumerable<GetDoctorDto>> GetAllUnConfirmedDoctor(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetDoctorDto>>(await _userRepository.GetAllUnConfirmedDoctor(cancellationToken));
        }

        public async Task ConfirmDoctor(string doctorUserName, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(await _userRepository.GetUserBySlug(doctorUserName, cancellationToken)) ?? throw new Exception("Doktor Bulunamadı");

            await _userRepository.ConfirmDoctor(doctor, cancellationToken);

            await _mailService.SendEmailToDoctorForConfirmationAccount(doctor, cancellationToken);
        }

        public async Task<IEnumerable<GetPatientDto>> GetAllPatients(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetPatientDto>>(await _userRepository.GetAllPatients(cancellationToken));
        }
        public async Task<IEnumerable<GetDoctorDto>> GetAllDoctors(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetDoctorDto>>(await _userRepository.GetAllDoctors(cancellationToken));
        }
    }
}
