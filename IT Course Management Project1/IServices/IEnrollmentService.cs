using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface IEnrollmentService
    {
        Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment> GetEnrollmentByIdAsync(int id);
        Task<int> UpdateEnrollmentAsync(int id, Enrollment enrollment);
        Task<int> DeleteEnrollmentAsync(int id);
    }
}
