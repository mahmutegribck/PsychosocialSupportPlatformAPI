using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _config = config;
        }


        public async Task<RegisterResponse> RegisterForDoctor(RegisterDoctorDto model)
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

                Doctor newUser = _mapper.Map<Doctor>(model);
                newUser.Id = Guid.NewGuid().ToString();
                newUser.UserName = newUser.Email;

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    string role = _config["Roles:Doctor"] ?? throw new InvalidOperationException("Doktor rolü tanımlanmamış.");

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
                newUser.UserName = newUser.Email;

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
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Message = "",
                        IsSuccess = false,
                    };
                }
                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                    return new LoginResponse
                    {
                        Message = "",
                        IsSuccess = false,
                    };

                JwtTokenDTO token = await _jwtService.CreateJwtToken(user);

                return new LoginResponse
                {
                    JwtTokenDTO = token,
                    Message = "",
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

            ApplicationUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        Name = payload.GivenName,
                        Surname = payload.FamilyName,
                        ProfileImageUrl = payload.Picture,
                        UserName = payload.Email,
                        EmailConfirmed = payload.EmailVerified
                    };

                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new Exception("Invalid external authentication.");


            return new LoginResponse
            {
                JwtTokenDTO = await _jwtService.CreateJwtToken(user),
                Message = "",
                IsSuccess = true,

            };
        }
    }
}
