using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BugTracker.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<int> Create(Project entity)
        {
            return await _projectRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _projectRepo.Delete(id);
        }

        public async Task<Project> FindOne(int id)
        {
            return await _projectRepo.FindOne(id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _projectRepo.GetAll();
        }

        public async Task<IEnumerable<SelectListItem>> GetUsersAsSelectListItemsByProjectId(int id)
        {
            var project = await _projectRepo.FindOne(id);
            var list = new List<SelectListItem>();

            foreach (var userProject in project.UserProjects)
            {
                list.Add(new SelectListItem
                {
                    Value = userProject.AppUserId.ToString(),
                    Text = userProject.AppUser.LastName.ToString() + ", " + userProject.AppUser.FirstName.ToString()
                });
            }

            list = list.OrderBy(x => x.Text).ToList();

            return list.Prepend(new SelectListItem { Value = "", Text = "Unassigned" });
        }

        public async Task<int> Update(Project entity)
        {
            return await _projectRepo.Update(entity);
        }
    }
}
