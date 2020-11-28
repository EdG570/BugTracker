using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Application.Facades;
using BugTracker.Application.Factories;
using BugTracker.Application.ViewModels.ProjectViewModels;
using BugTracker.Application.ViewModels.TicketViewModels;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Core.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BugTracker.Application.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectFacade _projectFacade;

        public ProjectController(IProjectFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }

        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel { User = await _projectFacade.GetUser(User), PartialImageName = _projectFacade.GetSeason() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndexViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Project");

            var user = await _projectFacade.GetUser(User);

            var project = new Project
            {
                Name = vm.NewProjectName,
                Description = vm.NewProjectDescription,
                CreatedBy = $"{ user.FirstName } { user.LastName }",
                OwnerId = user.Id,
                RepositoryUri = vm.NewProjectRepoUrl
            };

            var result = await _projectFacade.CreateProject(project);

            var userProject = new UserProject
            {
                AppUserId = user.Id,
                ProjectId = project.Id,
                AppUser = user,
                Project = project
            };

            var userProjectResult = await _projectFacade.CreateUserProject(userProject);

            return RedirectToAction("Index", "Project");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var project = await _projectFacade.FindProjectById(id);

            if (project == null) throw new KeyNotFoundException("Project was not found in the database with the provided id argument.");

            var vm = new DetailViewModel { 
                Project = project,
                Notifications = await _projectFacade.GetNotificationsByUserId(_projectFacade.GetUserId(User)),
                TicketCreateVm = new TicketCreateViewModel
                {
                    PriorityOptions = DropdownFactory.GetDropdown(DropdownFactory.DropdownType.Priority),
                    StatusOptions = DropdownFactory.GetDropdown(DropdownFactory.DropdownType.Status),
                    TypeOptions = DropdownFactory.GetDropdown(DropdownFactory.DropdownType.Type),
                    ProjectUsers = await _projectFacade.GetUserSelectListByProjectId(id),
                    ProjectId = id
                }
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(string name, string description, string link, string projectId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(projectId))
                throw new ArgumentException("Arguments cannot be null");

            var intId = Convert.ToInt32(projectId);

            var projectFromDb = await _projectFacade.FindProjectById(intId);

            if (projectFromDb == null)
                throw new KeyNotFoundException("Project not found in the database with the Id provided as an argument.");

            projectFromDb.Name = name;
            projectFromDb.Description = description;
            projectFromDb.RepositoryUri = link;

            var result = await _projectFacade.UpdateProject(projectFromDb);

            return Json(new { project = projectFromDb });
        }

        [HttpPost]
        public async Task<JsonResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id cannot be null or empty.");

            var projectId = Convert.ToInt32(id);

            var projectFromDb = await _projectFacade.FindProjectById(projectId);

            if (projectFromDb == null)
                throw new KeyNotFoundException("Ticket was not found in the database with the id provided as an argument.");

            return Json(new { project = projectFromDb });
        }

        [HttpGet]
        public async Task<JsonResult> Reports(int id)  
        {
            var user = await _projectFacade.GetUser(User);
            var vm = _projectFacade.GetReportsViewModel(user, id);
           
            return Json(new { vm });
        }
    }
}
