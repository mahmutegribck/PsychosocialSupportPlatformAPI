using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Auth.JwtToken
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<JwtTokenDTO> CreateJwtToken(ApplicationUser user)
        {
            var jwttoken = new JwtTokenDTO();
            var tokenhandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty));

            jwttoken.AccessTokenTime = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email)
            };

            //var userRoles = await _userManager.GetRolesAsync(user);
            //foreach (var role in userRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Expires = jwttoken.AccessTokenTime,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokendesc);
            var finaltoken = tokenhandler.WriteToken(token);


            return new JwtTokenDTO() { AccessToken = finaltoken, AccessTokenTime = jwttoken.AccessTokenTime, RefreshToken = await GenerateRefreshToken(user, jwttoken.AccessTokenTime) };
        }


        public async Task<string> GenerateRefreshToken(ApplicationUser user, DateTime accessTokenTime)
        {
            var randomnumber = new byte[32];
            using RandomNumberGenerator randomnumbergenerator = RandomNumberGenerator.Create();

            randomnumbergenerator.GetBytes(randomnumber);
            string refreshtoken = Convert.ToBase64String(randomnumber);

            user.RefreshToken = refreshtoken;
            user.RefreshTokenEndDate = accessTokenTime.AddHours(2);
            await _userManager.UpdateAsync(user);

            return refreshtoken;
        }


        public async Task<JwtTokenDTO?> GenerateJwtTokenWithRefreshToken(string refreshToken)
        {
            ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                return await CreateJwtToken(user);
            }
            else
                return null;
        }
    }
}
