using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static BugTracker.Core.Interfaces.IGenericRepository;

namespace BugTracker.Core.Interfaces
{
    public interface IUserProjectRepository : IGenericRepository<UserProject>
    {
    }
}
