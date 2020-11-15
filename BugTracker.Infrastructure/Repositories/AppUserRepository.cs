using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IdentityAppContext _context;

        public AppUserRepository(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<AppUser> FindOne(int id)
        {
            return await _context.AppUsers.Include(x => x.UserProjects)
                                          .ThenInclude(x => x.Project)
                                          .ThenInclude(x => x.Tickets)
                                          .ThenInclude(x => x.Comments)
                                          .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
