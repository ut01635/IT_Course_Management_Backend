

using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;

namespace IT_Course_Management_Project1.IServices
{
    public interface IStudentService
    {
<<<<<<< Updated upstream
        Task<StudentResponseDto> GetStudentByNIC(string NIC);
        Task<List<StudentResponseDto>> GetAllStudents();
        Task<StudentResponseDto> AddStudent(StudentRequestDto studentRequest);
        Task<StudentResponseDto> UpdateStudent(string NIC, StudentUpdateRequestDTO studentRequest);
        Task DeleteStudents(string NIC);
=======
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByNicAsync(string nic);
        Task<Student> UpdateStudentAsync(string nic, Student student);
        Task<int> DeleteStudentAsync(string nic);
>>>>>>> Stashed changes
    }
}
