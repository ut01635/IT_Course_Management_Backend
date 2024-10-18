using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;

namespace IT_Course_Management_Project1.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }


        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            try
            {
                return await _paymentRepository.GetAllPaymentsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service layer: Unable to fetch payments.", ex);
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            try
            {
                return await _paymentRepository.GetPaymentByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service layer: Unable to fetch payment with ID {id}.", ex);
            }
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            try
            {
                return await _paymentRepository.AddPaymentAsync(payment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service layer: Unable to add payment.", ex);
            }
        }

        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            try
            {
                return await _paymentRepository.UpdatePaymentAsync(payment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service layer: Unable to update payment.", ex);
            }
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                return await _paymentRepository.DeletePaymentAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service layer: Unable to delete payment with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByEnrollmentIdAsync(int enrollmentId)
        {
            try
            {
                return await _paymentRepository.GetPaymentsByEnrollmentIdAsync(enrollmentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service layer: Unable to fetch payments for Enrollment ID {enrollmentId}.", ex);
            }
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByNicAsync(string nic)
        {
            try
            {
                return await _paymentRepository.GetPaymentsByNicAsync(nic);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in service layer: Unable to fetch payments for NIC {nic}.", ex);
            }
        }
    }
}
