using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByEnrollmentIdAsync(int enrollmentId);
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int id);
    }
}
