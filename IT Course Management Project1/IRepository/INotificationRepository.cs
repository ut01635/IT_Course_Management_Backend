using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<Notification> GetByIdAsync(int id);

        Task<IEnumerable<Notification>> GetAllAsync();
    }
}
