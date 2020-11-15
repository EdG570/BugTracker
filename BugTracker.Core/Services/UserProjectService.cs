using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Services
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IUserProjectRepository _userProjectRepo;

        public UserProjectService(IUserProjectRepository userProjectRepo)
        {
            _userProjectRepo = userProjectRepo;
        }

        public async Task<int> Create(UserProject entity)
        {
            return await _userProjectRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _userProjectRepo.Delete(id);
        }

        public async Task<UserProject> FindOne(int id)
        {
            return await _userProjectRepo.FindOne(id);
        }

        public async Task<IEnumerable<UserProject>> GetAll()
        {
            return await _userProjectRepo.GetAll();
        }

        public async Task<IEnumerable<UserProject>> GetAllByUserId(int id)
        {
            var userProjects = await _userProjectRepo.GetAll();
            userProjects = userProjects.Where(x => x.AppUserId == id);

            if (userProjects == null)
                return new List<UserProject>();

            return userProjects;
        }

        public async Task<int> Update(UserProject entity)
        {
            return await _userProjectRepo.Update(entity);
        }
    }
}
