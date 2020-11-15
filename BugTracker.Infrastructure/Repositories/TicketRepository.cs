using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using BugTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IdentityAppContext _context;

        public TicketRepository(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Ticket entity)
        {
            await _context.Tickets.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await SaveChanges();

            return 1;
        }

        public async Task<Ticket> FindOne(int id)
        {
            return await _context.Tickets.Include(x => x.Project)
                                         .Include(x => x.Comments)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets.Include(x => x.Project)
                                         .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Ticket entity)
        {
            var ticket = await _context.Tickets.FindAsync(entity.Id);
            _context.Entry(ticket).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}
