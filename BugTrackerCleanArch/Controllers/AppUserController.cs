using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Application.ViewModels.AppUserViewModels;
using BugTracker.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Application.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IUserProjectService _userProjectService;
        private readonly IAppUserService _userService;

        public AppUserController(IUserProjectService userProjectService, IAppUserService appUserService)
        {
            _userProjectService = userProjectService;
            _userService = appUserService;
        }

        public async Task<IActionResult> Collaborate(int id)
        {
            var collaborators = await _userProjectService.GetUserCollabsByProjectId(id);
            var nonCollaborators = await _userService.GetAll();
            nonCollaborators = nonCollaborators.Where(x => !collaborators.Contains(x));


            var vm = new CollaborateViewModel
            {
                Collaborators = collaborators,
                NonCollaborators = nonCollaborators.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.LastName + ", " + x.FirstName })
                                                   .OrderByDescending(x => x.Text),
                ProjectId = id
            };

            return View(vm);
        }

        
    }
}
