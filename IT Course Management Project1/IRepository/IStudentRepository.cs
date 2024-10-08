using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByNIC(string NIC);
        Task<List<Student>> GetAllStudents();
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(string NIC, Student student);
        Task DeleteStudents(string NIC);
    }
}
