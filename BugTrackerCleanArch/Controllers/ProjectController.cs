using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Application.ViewModels.Project;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BugTracker.Application.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IProjectService _projectService;
        private readonly IUserProjectService _userProjectService;
        private readonly IMapper _mapper;

        public ProjectController(IAppUserService appUserService, IProjectService projectService, IUserProjectService userProjectService, IMapper mapper)
        {
            _appUserService = appUserService;
            _projectService = projectService;
            _userProjectService = userProjectService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _appUserService.FindOne(userId);

            return View(new IndexViewModel { User = user });
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndexViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Project");

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _appUserService.FindOne(userId);

            var project = new Project
            {
                Name = vm.NewProjectName,
                Description = vm.NewProjectDescription,
                CreatedBy = $"{ user.FirstName } { user.LastName }",
                OwnerId = user.Id,
                RepositoryUri = vm.NewProjectRepoUrl
            };

            var result = await _projectService.Create(project);

            var userProject = new UserProject
            {
                AppUserId = user.Id,
                ProjectId = project.Id,
                AppUser = user,
                Project = project
            };

            var userProjectResult = await _userProjectService.Create(userProject);

            return RedirectToAction("Index", "Project");
        }

        [HttpPost]
        public async Task<JsonResult> Edit(string name, string description, string link, string projectId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(projectId))
                throw new ArgumentException("Arguments cannot be null");

            var intId = Convert.ToInt32(projectId);

            var projectFromDb = await _projectService.FindOne(intId);

            if (projectFromDb == null)
                throw new KeyNotFoundException("Project not found in the database with the Id provided as an argument.");

            projectFromDb.Name = name;
            projectFromDb.Description = description;
            projectFromDb.RepositoryUri = link;

            var result = await _projectService.Update(projectFromDb);

            return Json(new { project = projectFromDb });
        }

        [HttpPost]
        public async Task<JsonResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id cannot be null or empty.");

            var projectId = Convert.ToInt32(id);

            var projectFromDb = await _projectService.FindOne(projectId);

            if (projectFromDb == null)
                throw new KeyNotFoundException("Ticket was not found in the database with the id provided as an argument.");

            return Json(new { project = projectFromDb });
        }
    }
}
