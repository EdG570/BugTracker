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
    public class NotificationRepository : INotificationRepository
    {
        private readonly IdentityAppContext _context;

        public NotificationRepository(IdentityAppContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Notification entity)
        {
            await _context.Notifications.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notification);
            await SaveChanges();

            return 1;
        }

        public async Task<Notification> FindOne(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications.Include(x => x.AppUser)
                                               .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Notification entity)
        {
            var notification = await _context.Notifications.FindAsync(entity.Id);
            _context.Entry(notification).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}
