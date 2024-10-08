

using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;

namespace IT_Course_Management_Project1.IServices
{
    public interface IStudentService
    {
        Task<StudentResponseDto> GetStudentByNIC(string NIC);
        Task<List<StudentResponseDto>> GetAllStudents();
        Task<StudentResponseDto> AddStudent(StudentRequestDto studentRequest);
        Task<StudentResponseDto> UpdateStudent(string NIC, StudentUpdateRequestDTO studentRequest);
        Task DeleteStudents(string NIC);
    }
}
