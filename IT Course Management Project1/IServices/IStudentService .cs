

using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;
using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByNicAsync(string nic);
        Task<Student> UpdateStudentAsync(string nic, StudentUpdateRequestDTO studentDto);
        Task<int> DeleteStudentAsync(string nic);

        Task PasswordUpdateAsync(string nic, string newPassword);
    }
}
