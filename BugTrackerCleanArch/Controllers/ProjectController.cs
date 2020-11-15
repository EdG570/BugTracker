using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Application.ViewModels.Project;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace BugTracker.Application.Controllers
{
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
    }
}
