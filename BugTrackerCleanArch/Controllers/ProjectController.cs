using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Application.ViewModels.Project;
using BugTracker.Core.Interfaces;
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
    }
}
