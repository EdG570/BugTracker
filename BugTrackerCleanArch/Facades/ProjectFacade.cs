using AutoMapper;
using BugTracker.Application.ViewModels.ProjectViewModels;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Application.Facades
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IAppUserService _appUserService;
        private readonly IProjectService _projectService;
        private readonly IUserProjectService _userProjectService;
        private readonly ITicketService _ticketService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public ProjectFacade(IAppUserService appUserService, IProjectService projectService,
            IUserProjectService userProjectService, IMapper mapper, ITicketService ticketService,
            INotificationService notificationService)
        {
            _appUserService = appUserService;
            _projectService = projectService;
            _userProjectService = userProjectService;
            _mapper = mapper;
            _ticketService = ticketService;
            _notificationService = notificationService;
        }

        public async Task<AppUser> GetUser(ClaimsPrincipal principal)
        {
            var userId = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _appUserService.FindOne(userId);
        }

        public string GetSeason()
        {
            return _projectService.GetSeason();
        }

        public async Task<int> CreateUserProject(UserProject userProject)
        {
            return await _userProjectService.Create(userProject);
        }

        public async Task<IEnumerable<SelectListItem>> GetUserSelectListByProjectId(int id)
        {
            return await _projectService.GetUsersAsSelectListItemsByProjectId(id);
        }

        public async Task<Project> FindProjectById(int id)
        {
            return await _projectService.FindOne(id);
        }

        public async Task<int> UpdateProject(Project project)
        {
            return await _projectService.Update(project);
        }

        public async Task<int> CreateProject(Project project)
        {
            return await _projectService.Create(project);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
        {
            return await _notificationService.GetAllByUserId(userId);
        }

        public int GetUserId(ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public ReportsViewModel GetReportsViewModel(AppUser user, int projectId)
        {
            var project = user.UserProjects.Select(x => x.Project).FirstOrDefault(x => x.Id == projectId);

            return new ReportsViewModel
            {
                ProjectId = projectId,
                TotalTicketPriorityHigh = project.Tickets.Where(x => x.Priority == Priority.High).Count(),
                TotalTicketsPriorityLow = project.Tickets.Where(x => x.Priority == Priority.Low).Count(),
                TotalTicketsPriorityMed = project.Tickets.Where(x => x.Priority == Priority.Medium).Count(),
                TotalTicketsPriorityUrgent = project.Tickets.Where(x => x.Priority == Priority.Urgent).Count(),
                TotalTicketsStatusClosed = project.Tickets.Where(x => x.Status == Status.Closed).Count(),
                TotalTicketsStatusOpen = project.Tickets.Where(x => x.Status == Status.Open).Count(),
                TotalTicketsStatusInProgress = project.Tickets.Where(x => x.Status == Status.InProgress).Count(),
                TotalTicketsTypeTask = project.Tickets.Where(x => x.Type == TicketType.Task).Count(),
                TotalTicketsTypeBug = project.Tickets.Where(x => x.Type == TicketType.Bug).Count(),
                TotalTicketsTypeFeature = project.Tickets.Where(x => x.Type == TicketType.Feature).Count()
            };
        }
    }
}
