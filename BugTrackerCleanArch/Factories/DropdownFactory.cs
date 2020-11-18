using BugTracker.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BugTracker.Application.Factories
{
    public static class DropdownFactory
    {
        public enum DropdownType
        {
            Priority,
            Status,
            Type,
            Collaborator
        }

        public static IEnumerable<SelectListItem> GetDropdown(DropdownType type)
        {
            switch (type)
            {
                case DropdownType.Priority:
                    return new List<SelectListItem>
                    {
                        new SelectListItem { Value = Priority.Low.ToString(), Text = "Low" },
                        new SelectListItem { Value = Priority.Medium.ToString(), Text = "Medium" },
                        new SelectListItem { Value = Priority.High.ToString(), Text = "High" },
                        new SelectListItem { Value = Priority.Urgent.ToString(), Text = "Urgent" }
                    };        
                case DropdownType.Status:
                    return new List<SelectListItem>
                    {
                        new SelectListItem { Value = Status.Open.ToString(), Text = "Open" },
                        new SelectListItem { Value = Status.InProgress.ToString(), Text = "In-Progress" },
                        new SelectListItem { Value = Status.Closed.ToString(), Text = "Closed" }
                    };
                case DropdownType.Type:
                    return new List<SelectListItem>
                    {
                        new SelectListItem { Value = TicketType.Task.ToString(), Text = "Task" },
                        new SelectListItem { Value = TicketType.Feature.ToString(), Text = "Feature" },
                        new SelectListItem { Value = TicketType.Bug.ToString(), Text = "Bug" }
                    };
                default:
                    return null;
            }
        }
    }
}
