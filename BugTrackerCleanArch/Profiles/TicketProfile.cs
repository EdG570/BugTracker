using AutoMapper;
using BugTracker.Application.ViewModels.TicketViewModels;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Application.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketCreateViewModel, Ticket>();
        }
    }
}
