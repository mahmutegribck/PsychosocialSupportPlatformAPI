using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PsychosocialSupportPlatformDBContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, PsychosocialSupportPlatformDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var userMessages = await _context.Messages.Where(m => m.SenderId == id || m.ReceiverId == id).ToListAsync();
            _context.Messages.RemoveRange(userMessages);

            var deletUser = await GetUser(id);
            IdentityResult result = await _userManager.DeleteAsync(deletUser);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            //var role = await _roleManager.FindByNameAsync("User");
            //return (List<ApplicationUser>)await _userManager.GetUsersInRoleAsync(role.Name);
            return null;
        }

        public async Task<ApplicationUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
