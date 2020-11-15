using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUser> FindOne(int id);
    }
}
