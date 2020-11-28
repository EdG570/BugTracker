using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Application.ViewModels.ProjectViewModels;
using BugTracker.Application.ViewModels.TicketViewModels;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
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

            ticketFromDb.Status = (status == "Open") ? Status.Open
                                                     : (status == "Closed")
                                                        ? Status.Closed
                                                        : (status == "InProgress")
                                                            ? Status.InProgress
                                                            : ticketFromDb.Status;

            var result = await _ticketService.Update(ticketFromDb);

            return new JsonResult(new { status });
        }

        [HttpPost]
        public async Task<JsonResult> FindById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id cannot be null or empty.");

            var ticketId = Convert.ToInt32(id);
            var ticketFromDb = await _ticketService.FindOne(ticketId);

            return Json(new { ticketFromDb });
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.TicketCreateVm.Title) || string.IsNullOrEmpty(vm.TicketCreateVm.Description))
                return RedirectToAction("Detail", "Project", new { id = vm.Project.Id });

            var user = await _appUserService.GetUserByClaim(User);
            var ticket = _mapper.Map(vm.TicketCreateVm, new Ticket());

            ticket.CreatedBy = user.FirstName + user.LastName;
            ticket.RequestorId = user.Id;
            ticket.ProjectId = vm.Project.Id;
            ticket.Status = Status.Open; 

            var result = await _ticketService.Create(ticket);

            return RedirectToAction("Detail", "Project", new { id = ticket.ProjectId });
        }
    }
}
