using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Users
{
    public interface IUserService
    {
        Task<object> GetUserByID(string userId);
        Task<IdentityResult> DeleteUser(string id);
    }
}
