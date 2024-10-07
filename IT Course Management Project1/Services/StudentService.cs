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

        public async Task<StudentResponseDto> GetStudentByNIC(string NIC)
        {
            var student = await _studentRepository.GetStudentByNIC(NIC);
            return student == null ? null : new StudentResponseDto
            {
                Id = student.Id,
                NIC = student.NIC,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DOB = student.DOB,
                Age = student.Age,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email
            };
        }

        public async Task<List<StudentResponseDto>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudents();
            var studentDtos = new List<StudentResponseDto>();

            foreach (var student in students)
            {
                studentDtos.Add(new StudentResponseDto
                {
                    Id = student.Id,
                    NIC = student.NIC,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DOB = student.DOB,
                    Age = student.Age,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email
                });
            }

            return studentDtos;
        }

        public async Task<StudentResponseDto> AddStudent(StudentRequestDto studentRequest)
        {
            // Check if the NIC already exists
            var existingStudent = await _studentRepository.GetStudentByNIC(studentRequest.NIC);
            if (existingStudent != null)
            {
                throw new InvalidOperationException("A student with this NIC already exists.");
            }

            // Create a new student object
            var student = new Student
            {
                NIC = studentRequest.NIC,
                FirstName = studentRequest.FirstName,
                LastName = studentRequest.LastName,
                DOB = studentRequest.DOB,
                Age = CalculateAge(studentRequest.DOB),
                PhoneNumber = studentRequest.PhoneNumber,
                Email = studentRequest.Email,
                PassWord = studentRequest.PassWord // Hash the password before saving
            };

            // Add the student to the repository
            await _studentRepository.AddStudent(student);

            // Create and return the response DTO
            var response = new StudentResponseDto
            {
                Id = student.Id,
                NIC = student.NIC,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DOB = student.DOB,
                Age = student.Age,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
            };
            return response;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }


        public async Task UpdateStudent(string NIC, StudentRequestDto studentRequest)
        {
            var student = new Student
            {
                NIC = studentRequest.NIC,
                FirstName = studentRequest.FirstName,
                LastName = studentRequest.LastName,
                DOB = studentRequest.DOB,
                Age = CalculateAge(studentRequest.DOB),
                PhoneNumber = studentRequest.PhoneNumber,
                Email = studentRequest.Email,
                PassWord = studentRequest.PassWord // Hash before saving
            };

            await _studentRepository.UpdateStudent(student);
        }

        public async Task DeleteStudents(string NIC)
        {
            await _studentRepository.DeleteStudents(NIC);
        }
    }
}
