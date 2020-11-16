using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Application.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IProjectService _projectService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public TicketController(ITicketService ticketService, IProjectService projectService, IMapper mapper, IAppUserService appUserService)
        {
            _ticketService = ticketService;
            _projectService = projectService;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(string id, string status)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(status))
                throw new ArgumentException("id and status cannot be null or empty.");

            var ticketId = Convert.ToInt32(id);

            var ticketFromDb = await _ticketService.FindOne(ticketId);

            if (ticketFromDb == null)
                throw new KeyNotFoundException("Ticket was not found in the database with the id provided as an argument.");

            ticketFromDb.Status = (status == "Open") ? Status.Open
                                                     : (status == "Closed")
                                                        ? Status.Closed
                                                        : (status == "InProgress")
                                                            ? Status.InProgress
                                                            : ticketFromDb.Status;

            var result = await _ticketService.Update(ticketFromDb);

            return new JsonResult("success");
        }

        [HttpPost]
        public async Task<JsonResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id cannot be null or empty.");

            var ticketId = Convert.ToInt32(id);

            var ticketFromDb = await _ticketService.FindOne(ticketId);

            if (ticketFromDb == null)
                throw new KeyNotFoundException("Ticket was not found in the database with the id provided as an argument.");

            return Json(new { ticketFromDb });
        }
    }
}
