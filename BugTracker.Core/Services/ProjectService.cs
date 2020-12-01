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

        public string GetSeason()
        {
            var month = DateTime.Now.Month;

            switch (month)
            {
                case 1:
                    return "winter-";
                case 2:
                    return "winter-";
                case 3:
                    return "spring-";
                case 4:
                    return "spring-";
                case 5:
                    return "spring-";
                case 6:
                    return "summer-";
                case 7:
                    return "summer-";
                case 8:
                    return "summer-";
                case 9:
                    return "fall-";
                case 10:
                    return "fall-";
                case 11:
                    return "fall-";
                case 12:
                    return "winter-";
                default:
                    return "summer-";
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetUsersAsSelectListItemsByProjectId(int id)
        {
            var project = await _projectRepo.FindOne(id);
            var list = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Unassigned" } };

            if (project == null || project.UserProjects == null)
                return list;

            foreach (var userProject in project.UserProjects)
            {
                list.Add(new SelectListItem
                {
                    Value = userProject.AppUserId.ToString(),
                    Text = userProject.AppUser.LastName.ToString() + ", " + userProject.AppUser.FirstName.ToString()
                });
            }

            list = list.OrderBy(x => x.Text).ToList();

            return list;
        }

        public async Task<int> Update(Project entity)
        {
            return await _projectRepo.Update(entity);
        }
    }
}
