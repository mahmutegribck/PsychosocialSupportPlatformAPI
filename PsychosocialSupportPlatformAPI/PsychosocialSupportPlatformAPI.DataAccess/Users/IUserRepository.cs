using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Users
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUser(string id);

        Task<IdentityResult> DeleteUser(string id);
        //Task<IdentityResult> UpdateUser(ApplicationUser user);
        //Task<IdentityResult> DeleteUser(string id);
    }
}
