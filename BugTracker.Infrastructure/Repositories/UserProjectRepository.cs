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
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly IdentityAppContext _context;

        public UserProjectRepository(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<int> Create(UserProject entity)
        {
            await _context.UserProjects.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var userProject = await _context.UserProjects.FindAsync(id);
            _context.UserProjects.Remove(userProject);
            await SaveChanges();

            return 1;
        }

        public async Task<UserProject> FindOne(int id)
        {
            return await _context.UserProjects.FindAsync(id);
        }

        public async Task<IEnumerable<UserProject>> GetAll()
        {
            return await _context.UserProjects.Include(x => x.Project)
                                              .Include(x => x.AppUser)
                                              .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(UserProject entity)
        {
            _context.UserProjects.Update(entity);
            await SaveChanges();

            return 1;
        }
    }
}
