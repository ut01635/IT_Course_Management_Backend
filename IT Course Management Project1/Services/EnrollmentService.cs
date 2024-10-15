using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;

namespace IT_Course_Management_Project1.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
        {
            return await _enrollmentRepository.AddEnrollmentAsync(enrollment);
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _enrollmentRepository.GetAllEnrollmentsAsync();
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            return await _enrollmentRepository.GetEnrollmentByIdAsync(id);
        }

        public async Task<int> UpdateEnrollmentAsync(int id, Enrollment enrollment)
        {
            return await _enrollmentRepository.UpdateEnrollmentAsync(id, enrollment);
        }

        public async Task<int> DeleteEnrollmentAsync(int id)
        {
            if (id != 0)
            {
                var data = _enrollmentRepository.GetEnrollmentByIdAsync(id);
                if(data == null)
                {
                    throw new Exception("Enrollment Not Found");
                }
                return await _enrollmentRepository.DeleteEnrollmentAsync(id);
            }
            else
            {
                throw  new Exception("Please enter valid Id");
            }
           
        }
    }
}
