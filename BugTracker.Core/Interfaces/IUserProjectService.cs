using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Interfaces
{
    public interface IUserProjectService : IGenericService<UserProject>
    {
        Task<IEnumerable<UserProject>> GetAllByUserId(int id);
        Task<IEnumerable<AppUser>> GetUserCollabsByProjectId(int id);
        Task<IEnumerable<AppUser>> GetNonCollabUsersByProjectId(int id);
    }
}
