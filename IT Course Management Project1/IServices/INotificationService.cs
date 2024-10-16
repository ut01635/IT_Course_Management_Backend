﻿using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface INotificationService
    {
        Task AddNotificationAsync(Notification notification);
        Task<Notification> GetNotificationByIdAsync(int id);

        Task<IEnumerable<Notification>> GetAllNotificationsAsync();

        Task DeleteNotificationAsync(int id);

        Task UpdateNotificationAsync(Notification notification);

        Task<IEnumerable<Notification>> GetNotificationsByNicAsync(string nic);
    }
}
