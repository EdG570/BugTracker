using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTracker.Core.Interfaces
{
    public interface IProjectService : IGenericService<Project>
    {
        Task<IEnumerable<SelectListItem>> GetUsersAsSelectListItemsByProjectId(int id);
    }
}
