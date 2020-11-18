using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BugTracker.Core.Interfaces
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsByUserId(int id);
    }
}
