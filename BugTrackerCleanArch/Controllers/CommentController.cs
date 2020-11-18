using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Application.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IAppUserService _appUserService;

        public CommentController(ICommentService commentService, IAppUserService appUserService)
        {
            _commentService = commentService;
            _appUserService = appUserService;
        }

        [HttpPost]
        public async Task<JsonResult> Create(string ticketId, string description)
        {
            if (string.IsNullOrEmpty(ticketId) || string.IsNullOrEmpty(description))
                throw new ArgumentException("Ticket id and comment description cannot be null.");

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _appUserService.FindOne(userId);

            var comment = new Comment
            {
                CreatedAt = DateTime.Now,
                CreatedBy = user.FirstName + " " + user.LastName,
                Description = description,
                CreatorId = user.Id,
                TicketId = Convert.ToInt32(ticketId)
            };

            var result = await _commentService.Create(comment);

            return Json(new { comment });
        }
    }
}
