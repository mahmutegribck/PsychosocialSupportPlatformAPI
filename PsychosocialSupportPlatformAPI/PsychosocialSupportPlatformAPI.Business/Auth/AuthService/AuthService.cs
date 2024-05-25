using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.FacebookAuthDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<Patient> _patientManager;
        private readonly UserManager<Doctor> _doctorManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService,
            IMapper mapper,
            IConfiguration config,
            HttpClient httpClient,
            UserManager<Patient> patientManager,
            UserManager<Doctor> doctorManager,
            IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _config = config;
            _httpClient = httpClient;
            _patientManager = patientManager;
            _doctorManager = doctorManager;
            _userService = userService;
        }


        public async Task<RegisterResponse> RegisterForDoctor(RegisterDoctorDto model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "Model boş olamaz.");
                }

                if (await _userService.GetDoctorTitleById(model.DoctorTitleId) == null) throw new Exception("Ünvan Bulunamadı");

                if (model.Password != model.ConfirmPassword)
                    return new RegisterResponse
                    {
                        Message = "Parola ve onay parolası eşleşmiyor.",
                        IsSuccess = false,
                    };

                Doctor newUser = _mapper.Map<Doctor>(model);
                newUser.Id = Guid.NewGuid().ToString();

                newUser.UserName = newUser.Name.ToLower() + "-" + newUser.Surname.ToLower();

                char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
                char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

                for (int i = 0; i < turkishChars.Length; i++)
                    newUser.UserName = newUser.UserName.Replace(turkishChars[i], englishChars[i]);

                newUser.UserName = Regex.Replace(newUser.UserName, @"[^a-zA-Z0-9]", "-");
                newUser.UserName += "-" + new Random().Next(1000, 1000000);


                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    string role = _config["Roles:Doctor"] ?? throw new InvalidOperationException("Doktor rolü tanımlanmamış.");

                    bool roleExists = await _roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        ApplicationRole newRole = new();
                        newRole.Id = Guid.NewGuid().ToString();
                        newRole.Name = role;

                        await _roleManager.CreateAsync(newRole);

                    }
                    await _userManager.AddToRoleAsync(newUser, role);

                    return new RegisterResponse
                    {
                        Message = "Kullanıcı başarılı şekilde oluşturuldu.",
                        IsSuccess = true,
                    };
                }
                return new RegisterResponse
                {
                    Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault()?.Description),
                    IsSuccess = false,

                };
            }
            catch (ArgumentNullException ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
            catch (InvalidOperationException ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }


        public async Task<RegisterResponse> RegisterForPatient(RegisterPatientDto model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "Model boş olamaz.");
                }
                if (model.Password != model.ConfirmPassword)
                    return new RegisterResponse
                    {
                        Message = "Parola ve onay parolası eşleşmiyor.",
                        IsSuccess = false,
                    };

                Patient newUser = _mapper.Map<Patient>(model);
                newUser.Id = Guid.NewGuid().ToString();

                newUser.UserName = newUser.Name.ToLower() + "-" + newUser.Surname.ToLower();

                char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
                char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

                for (int i = 0; i < turkishChars.Length; i++)
                    newUser.UserName = newUser.UserName.Replace(turkishChars[i], englishChars[i]);

                newUser.UserName = Regex.Replace(newUser.UserName, @"[^a-zA-Z0-9]", "-");
                newUser.UserName += "-" + new Random().Next(1000, 1000000);

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    string role = _config["Roles:Patient"] ?? throw new InvalidOperationException("Hasta rolü tanımlanmamış.");

                    bool roleExists = await _roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        ApplicationRole newRole = new ApplicationRole();
                        newRole.Id = Guid.NewGuid().ToString();
                        newRole.Name = role;

                        await _roleManager.CreateAsync(newRole);
                    }
                    await _userManager.AddToRoleAsync(newUser, role);

                    return new RegisterResponse
                    {
                        Message = "Kullanıcı başarılı şekilde oluşturuldu.",
                        IsSuccess = true,
                    };
                }
                return new RegisterResponse
                {
                    Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault()?.Description),
                    IsSuccess = false,
                };
            }
            catch (ArgumentNullException ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
            catch (InvalidOperationException ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
        }


        public async Task<LoginResponse> LoginUserAsync(LoginDto model)
        {
            try
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Message = "Kullanıcı Bulunamadı",
                        IsSuccess = false,
                    };
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Contains(_config["Roles:Doctor"]))
                {
                    if (user is Doctor doctor)
                    {
                        if (!doctor.Confirmed)
                        {
                            return new LoginResponse
                            {
                                Message = "Danışman Henüz Yönetici Tarafından Onaylanmadı",
                                IsSuccess = false,
                            };
                        }
                    }
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                    return new LoginResponse
                    {
                        Message = "Kullanıcı Şifresi Hatalı",
                        IsSuccess = false,
                    };

                JwtTokenDTO? token = await _jwtService.CreateJwtToken(user);

                if (token == null)
                {
                    return new LoginResponse
                    {
                        Message = "Giriş Başarısız",
                        IsSuccess = false,
                    };
                }

                return new LoginResponse
                {
                    JwtTokenDTO = token,
                    Message = "Giriş Başarılı",
                    IsSuccess = true,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<JwtTokenDTO?> LoginWithRefreshToken(string refreshToken)
        {
            return await _jwtService.GenerateJwtTokenWithRefreshToken(refreshToken);
        }


        public async Task<LoginResponse> ResetPasswordAsync(ResetPasswordDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return new LoginResponse
                    {
                        IsSuccess = false,
                        Message = "",
                    };

                if (model.NewPassword != model.ConfirmPassword)
                    return new LoginResponse
                    {
                        IsSuccess = false,
                        Message = "",
                    };

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                    return new LoginResponse
                    {
                        Message = "",
                        IsSuccess = true,
                    };

                return new LoginResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<LoginResponse> LoginUserViaGoogle(string token)
        {
            ValidationSettings? settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _config["ExternalLogin:Google-Client-Id"]! }
            };

            Payload payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

            UserLoginInfo userLoginInfo = new("google", payload.Subject, "GOOGLE");

            Patient user = await _patientManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _patientManager.FindByEmailAsync(payload.Email);
                var doctor = await _doctorManager.FindByEmailAsync(payload.Email);
                if (doctor != null) throw new Exception("Lütfen Giriş Yap Ekranından Giriş Yapınız.");

                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        Name = payload.GivenName,
                        Surname = payload.FamilyName,
                        ProfileImageUrl = payload.Picture,
                        EmailConfirmed = payload.EmailVerified
                    };
                    user.UserName = user.Name.ToLower() + "-" + user.Surname.ToLower();

                    char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
                    char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

                    for (int i = 0; i < turkishChars.Length; i++)
                        user.UserName = user.UserName.Replace(turkishChars[i], englishChars[i]);

                    user.UserName = Regex.Replace(user.UserName, @"[^a-zA-Z0-9]", "-");
                    user.UserName += "-" + new Random().Next(1000, 1000000);

                    IdentityResult createResult = await _patientManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);
                string role = _config["Roles:Patient"] ?? throw new InvalidOperationException("Hasta rolü tanımlanmamış.");

                bool roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    ApplicationRole newRole = new ApplicationRole();
                    newRole.Id = Guid.NewGuid().ToString();
                    newRole.Name = role;

                    await _roleManager.CreateAsync(newRole);
                }
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception("Invalid external authentication.");
            }

            return new LoginResponse
            {
                JwtTokenDTO = await _jwtService.CreateJwtToken(user),
                Message = "",
                IsSuccess = true,

            };
        }

        public async Task<LoginResponse> LoginUserViaFacebook(string token)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("https://graph.facebook.com/debug_token?input_token=" + token + $"&access_token={_config["ExternalLogin:Facebook-AppId"]}|{_config["ExternalLogin:Facebook-Secret"]}");

            var stringData = await httpResponseMessage.Content.ReadAsStringAsync();
            var userFB = JsonConvert.DeserializeObject<FacebookUserDto>(stringData);

            if (userFB == null || userFB.Data.IsValid == false)
            {
                return new LoginResponse
                {
                    Message = "",
                    IsSuccess = false,
                };
            }
            HttpResponseMessage userResponse = await _httpClient.GetAsync($"https://graph.facebook.com/me?fields=first_name,last_name,email&access_token={token}");

            byte[] userContentBytes = await userResponse.Content.ReadAsByteArrayAsync();
            string userContent = Encoding.UTF8.GetString(userContentBytes);

            var userData = JsonConvert.DeserializeObject<FacebookUserDataDto>(userContent);

            UserLoginInfo userLoginInfo = new("facebook", userFB.Data.UserId, "FACEBOOK");

            Patient user = await _patientManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _patientManager.FindByEmailAsync(userData.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userData.Email,
                        Name = userData.FirstName,
                        Surname = userData.LastName,
                        UserName = userData.Email
                    };

                    IdentityResult createResult = await _patientManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);
                string role = _config["Roles:Patient"] ?? throw new InvalidOperationException("Hasta rolü tanımlanmamış.");

                bool roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    ApplicationRole newRole = new ApplicationRole();
                    newRole.Id = Guid.NewGuid().ToString();
                    newRole.Name = role;

                    await _roleManager.CreateAsync(newRole);
                }
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception("Invalid external authentication.");
            }

            return new LoginResponse
            {
                JwtTokenDTO = await _jwtService.CreateJwtToken(user),
                Message = "",
                IsSuccess = true,

            };

        }
    }
}
