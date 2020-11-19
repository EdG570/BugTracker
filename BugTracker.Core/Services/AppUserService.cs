using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
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
    }
}
