using BugTracker.Application.ViewModels.ProjectViewModels;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Application.Facades
{
    public interface IProjectFacade
    {
        Task<AppUser> GetUser(ClaimsPrincipal principal);

        string GetSeason();

        Task<int> CreateUserProject(UserProject userProject);

        Task<IEnumerable<SelectListItem>> GetUserSelectListByProjectId(int id);

        Task<Project> FindProjectById(int id);

        Task<int> UpdateProject(Project project);
        Task<int> CreateProject(Project project);
        Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId);
        int GetUserId(ClaimsPrincipal principal);
        ReportsViewModel GetReportsViewModel(AppUser user, int projectId);
    }
}