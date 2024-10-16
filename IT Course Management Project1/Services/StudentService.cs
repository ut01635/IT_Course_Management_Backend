using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;
using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;

namespace IT_Course_Management_Project1.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            return await _studentRepository.AddStudentAsync(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetStudentByNicAsync(string nic)
        {
            return await _studentRepository.GetStudentByNicAsync(nic);
        }

        public async Task<Student> UpdateStudentAsync(string nic, Student student)
        {
            return await _studentRepository.UpdateStudentAsync(nic, student);
        }

        public async Task<int> DeleteStudentAsync(string nic)
        {
            return await _studentRepository.DeleteStudentAsync(nic);
        }

        public async Task PasswordUpdateAsync(string nic, string newPassword)
        {
            await _studentRepository.PasswordUpdateAsync(nic, newPassword);
        }
    }
}
