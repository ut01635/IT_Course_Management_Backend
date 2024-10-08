using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IStudentService studentService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _studentService = studentService;
        }

        // POST: api/student
        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentRequestDto studentRequest)
        {
            if (studentRequest == null)
                return BadRequest("Invalid student data.");
            else
            {
                try
                {
                    var data = await _studentService.AddStudent(studentRequest);
                    return Ok(data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); 
                }
            }
            
        }

        // GET: api/student/{id}
        [HttpGet("Get Student by NIC")]
        public async Task<IActionResult> GetStudentByNIC(string NIC)
        {
            var student = await _studentService.GetStudentByNIC(NIC);
            return student != null ? Ok(student) : NotFound();
        }

        // GET: api/student
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }
        // PUT: api/student/{id}
        [HttpPut("edit student")]
        public async Task<IActionResult> UpdateStudent(string NIC, StudentUpdateRequestDTO studentRequest)
        {
            if (studentRequest == null || NIC == null)
                return BadRequest("Invalid student data.");

            await _studentService.UpdateStudent(NIC, studentRequest);
            return NoContent();
        }

        // DELETE: api/student/{id}
        [HttpDelete("Delete Student{id}")]
        public async Task<IActionResult> DeleteStudent(string NIC)
        {
            await _studentService.DeleteStudents(NIC);
            return NoContent();
        }
    }

    
}
