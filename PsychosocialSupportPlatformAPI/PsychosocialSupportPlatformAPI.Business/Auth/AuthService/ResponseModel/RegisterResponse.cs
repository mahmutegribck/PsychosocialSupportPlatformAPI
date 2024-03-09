using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
