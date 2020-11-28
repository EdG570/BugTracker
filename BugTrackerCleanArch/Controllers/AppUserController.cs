using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly INotificationService _notificationService;

        public AppUserController(IUserProjectService userProjectService, IAppUserService appUserService, INotificationService notificationService)
        {
            _userProjectService = userProjectService;
            _userService = appUserService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Collaborate(int id)
        {
            var collaborators = await _userProjectService.GetUserCollabsByProjectId(id);
            var nonCollaborators = await _userService.GetAll();
            nonCollaborators = nonCollaborators.Where(x => !collaborators.Contains(x));
            var user = await _userService.GetUserByClaim(User);

            var vm = new CollaborateViewModel
            {
                Collaborators = collaborators,
                NonCollaborators = nonCollaborators.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.LastName + ", " + x.FirstName })
                                                   .OrderByDescending(x => x.Text),
                ProjectId = id,
                Notifications = await _notificationService.GetAllByUserId(user.Id)
            };

            return View(vm);
        }

        
    }
}
