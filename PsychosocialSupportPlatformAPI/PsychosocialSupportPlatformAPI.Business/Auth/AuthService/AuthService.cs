using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.FacebookAuthDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
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
        private readonly IMailService _mailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService,
            IMapper mapper,
            IConfiguration config,
            HttpClient httpClient,
            UserManager<Patient> patientManager,
            UserManager<Doctor> doctorManager,
            IUserService userService,
            IMailService mailService)
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
            _mailService = mailService;
        }


        public async Task<RegisterResponse> RegisterForDoctor(RegisterDoctorDto model, CancellationToken cancellationToken)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model), "Model boş olamaz.");
                }

                if (await _userService.GetDoctorTitleById(model.DoctorTitleId, cancellationToken) == null) throw new Exception("Ünvan Bulunamadı");

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

                cancellationToken.ThrowIfCancellationRequested();

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

                    string? token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser) ?? throw new Exception("Token Oluşturulamadı");
                    Console.WriteLine(token);
                    await _mailService.SendEmailForConfirmEmail(newUser.Email, token, cancellationToken);

                    return new RegisterResponse
                    {
                        Message = "Kullanıcı başarılı şekilde oluşturuldu. Mail adresinizi onaylamak için mail adresinizi kontrol edin.",
                        IsSuccess = true,
                    };
                }
                return new RegisterResponse
                {
                    Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault()?.Description),
                    IsSuccess = false,

                };
            }
            catch (OperationCanceledException)
            {
                return new RegisterResponse
                {
                    Message = "İşlem İptal Edildi",
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


        public async Task<RegisterResponse> RegisterForPatient(RegisterPatientDto model, CancellationToken cancellationToken)
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

                cancellationToken.ThrowIfCancellationRequested();
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    string role = _config["Roles:Patient"] ?? throw new InvalidOperationException("Hasta rolü tanımlanmamış.");

                    bool roleExists = await _roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        ApplicationRole newRole = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = role
                        };

                        await _roleManager.CreateAsync(newRole);
                    }
                    await _userManager.AddToRoleAsync(newUser, role);

                    string? token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser) ?? throw new Exception("Token Oluşturulamadı");
                    await _mailService.SendEmailForConfirmEmail(newUser.Email, token, cancellationToken);

                    return new RegisterResponse
                    {
                        Message = "Kullanıcı başarılı şekilde oluşturuldu. Mail adresinizi onaylamak için mail adresinizi kontrol edin.",
                        IsSuccess = true,
                    };
                }
                return new RegisterResponse
                {
                    Message = string.Format("Kullanıcı oluşturulurken bir hata oluştu: {0}", result.Errors.FirstOrDefault()?.Description),
                    IsSuccess = false,
                };
            }
            catch (OperationCanceledException)
            {
                return new RegisterResponse
                {
                    Message = "İşlem İptal Edildi",
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


        public async Task<LoginResponse> LoginUserAsync(LoginDto model, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser? user = await _userManager.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync(cancellationToken);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Message = "Kullanıcı Bulunamadı",
                        IsSuccess = false,
                    };
                }

                if (!user.EmailConfirmed)
                {
                    return new LoginResponse
                    {
                        Message = "Kullanıcı Mail Adresi Onaylanmamış. Lütfen Mail Adresinizi Kontrol Edin.",
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
                                Message = "Hesabınız Henüz Yönetici Tarafından Onaylanmadı",
                                IsSuccess = false,
                            };
                        }
                    }
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                {
                    return new LoginResponse
                    {
                        Message = "Kullanıcı Şifresi Hatalı",
                        IsSuccess = false,
                    };
                }

                cancellationToken.ThrowIfCancellationRequested();
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
            catch (OperationCanceledException)
            {
                return new LoginResponse
                {
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
            catch (Exception)
            {
                return new LoginResponse
                {
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
        }


        public async Task<JwtTokenDTO?> LoginWithRefreshToken(string refreshToken)
        {
            return await _jwtService.GenerateJwtTokenWithRefreshToken(refreshToken);
        }


        public async Task ResetPassword(string token, ResetPasswordDto model, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser? user = await _userManager.Users.Where(u => u.Email == model.Email).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"{model.Email} Adresine Ait Hesap Bulunamadı");

                if (model.NewPassword != model.ConfirmPassword) throw new Exception("Şifreler Eşleşemedi");

                IdentityResult? result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (!result.Succeeded) throw new Exception("Şifre Değişirilemedi");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ForgotPassword(string email, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.Users.AsNoTracking().Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

            if (user != null && user.EmailConfirmed == true)
            {
                string? token = await _userManager.GeneratePasswordResetTokenAsync(user) ?? throw new Exception("Token Oluşturulamadı");
                await _mailService.SendEmailForForgotPassword(user, token, cancellationToken);
            }
            else
            {
                throw new Exception($"{email} Adresine Ait Kullanıcı Bulunamadı");
            }
        }

        public async Task ConfirmEmail(string email, string token, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

            if (user != null)
            {
                IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded) throw new Exception("Hesabınız Onaylanamadı");
            }
            else
            {
                throw new Exception($"{email} Adresine Ait Kullanıcı Bulunamadı");
            }
        }

        public async Task<LoginResponse> LoginUserViaGoogle(string token, CancellationToken cancellationToken)
        {
            try
            {
                ValidationSettings? settings = new()
                {
                    Audience = new List<string>() { _config["ExternalLogin:Google-Client-Id"]! }
                };
                Payload payload = await ValidateAsync(token, settings);
                UserLoginInfo userLoginInfo = new("google", payload.Subject, "GOOGLE");
                Patient? user = await _patientManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _patientManager.Users.Where(p => p.Email == payload.Email).FirstOrDefaultAsync(cancellationToken);
                    var doctor = await _doctorManager.Users.Where(d => d.Email == payload.Email).FirstOrDefaultAsync(cancellationToken);

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

                        cancellationToken.ThrowIfCancellationRequested();

                        IdentityResult createResult = await _patientManager.CreateAsync(user);
                        result = createResult.Succeeded;
                    }
                }
                if (!result)
                    throw new Exception("Invalid external authentication.");

                await _userManager.AddLoginAsync(user, userLoginInfo);
                string role = _config["Roles:Patient"] ?? throw new InvalidOperationException("Hasta rolü tanımlanmamış.");

                bool roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    ApplicationRole newRole = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role
                    };
                    await _roleManager.CreateAsync(newRole);
                }
                await _userManager.AddToRoleAsync(user, role);

                return new LoginResponse
                {
                    JwtTokenDTO = await _jwtService.CreateJwtToken(user),
                    Message = "",
                    IsSuccess = true,
                };
            }
            catch (OperationCanceledException)
            {
                return new LoginResponse
                {
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
            catch (Exception)
            {
                return new LoginResponse
                {
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
        }

        public async Task<LoginResponse> LoginUserViaFacebook(string token, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("https://graph.facebook.com/debug_token?input_token=" + token + $"&access_token={_config["ExternalLogin:Facebook-AppId"]}|{_config["ExternalLogin:Facebook-Secret"]}", cancellationToken);

                var stringData = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                var userFB = JsonConvert.DeserializeObject<FacebookUserDto>(stringData);

                if (userFB == null || userFB.Data.IsValid == false)
                {
                    return new LoginResponse
                    {
                        Message = "",
                        IsSuccess = false,
                    };
                }
                HttpResponseMessage userResponse = await _httpClient.GetAsync($"https://graph.facebook.com/me?fields=first_name,last_name,email&access_token={token}", cancellationToken);

                byte[] userContentBytes = await userResponse.Content.ReadAsByteArrayAsync(cancellationToken);
                string userContent = Encoding.UTF8.GetString(userContentBytes);

                var userData = JsonConvert.DeserializeObject<FacebookUserDataDto>(userContent) ?? throw new Exception();

                UserLoginInfo userLoginInfo = new("facebook", userFB.Data.UserId, "FACEBOOK");

                Patient user = await _patientManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _patientManager.Users.Where(p => p.Email == userData.Email).FirstOrDefaultAsync(cancellationToken);
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
                        cancellationToken.ThrowIfCancellationRequested();

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
                        ApplicationRole newRole = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = role
                        };

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
            catch (OperationCanceledException)
            {
                return new LoginResponse
                {
                    JwtTokenDTO = null,
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
            catch (Exception)
            {
                return new LoginResponse
                {
                    JwtTokenDTO = null,
                    Message = "İşlem İptal Edildi",
                    IsSuccess = false,
                };
            }
        }

        public async Task LogOutUser(string currentUserId, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await _userManager.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Kullanıcı Bulunamadı");

            user.RefreshToken = null;
            user.RefreshTokenEndDate = null;
            await _userManager.UpdateAsync(user);

        }
    }
}
