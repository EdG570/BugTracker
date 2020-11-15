using BugTracker.Core.Interfaces;
using BugTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;

        public NotificationService(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<int> Create(Notification entity)
        {
            return await _notificationRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _notificationRepo.Delete(id);
        }

        public async Task<Notification> FindOne(int id)
        {
            return await _notificationRepo.FindOne(id);
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _notificationRepo.GetAll();
        }

        public async Task<int> Update(Notification entity)
        {
            return await _notificationRepo.Update(entity);
        }
    }
}
