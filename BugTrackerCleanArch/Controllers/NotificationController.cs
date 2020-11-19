using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Application.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IProjectService _projectService;
        private readonly INotificationService _notificationService;

        public NotificationController(IAppUserService appUserService, IProjectService projectService, INotificationService notificationService)
        {
            _appUserService = appUserService;
            _projectService = projectService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var notifications = await _notificationService.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(List<string> selectedCollaborators, int projectId)
        {
            if (selectedCollaborators == null || selectedCollaborators.Count() == 0)
                return RedirectToAction("Collaborate", "AppUser", new { id = projectId });

            var project = await _projectService.FindOne(projectId);

            if (project == null)
                throw new ArgumentException("Project Id cannot be null");

            var inviterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var inviter = await _appUserService.FindOne(inviterId);

            if (inviter == null)
                throw new KeyNotFoundException("The current user was not found in the database");

            foreach (var userId in selectedCollaborators)
            {
                var notification = new Notification
                {
                    AppUserId = Convert.ToInt32(userId),
                    Message = "You've been invited to collaborate in the " + project.Name + " project by " + inviter.FirstName + " " + inviter.LastName
                };

                var result = await _notificationService.Create(notification);
            }
            

            return RedirectToAction("Collaborate", "AppUser", new { id = projectId });
        }
    }
}
