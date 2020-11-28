using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepo;

        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public async Task<AppUser> FindOne(int id)
        {
            return await _appUserRepo.FindOne(id);
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _appUserRepo.GetAll();
        }

        public async Task<AppUser> GetUserByClaim(ClaimsPrincipal principal)
        {
            var userId = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _appUserRepo.FindOne(userId);
        }
    }
}
