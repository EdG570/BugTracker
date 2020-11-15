using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BugTracker.Core.Interfaces
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsByUserId(int id);
        IEnumerable<SelectListItem> GetStatusSelectListItems();
        IEnumerable<SelectListItem> GetPrioritySelectListItems();
        IEnumerable<SelectListItem> GetTicketTypeSelectListItems();
    }
}
