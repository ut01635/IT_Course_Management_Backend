using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.Entity;
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

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet ("Get-All-Students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("Get-StudentByNIC{nic}")]
        public async Task<ActionResult<Student>> GetStudentByNic(string nic)
        {
            var student = await _studentService.GetStudentByNicAsync(nic);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost("Add-Student")]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            var addedStudent = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentByNic), new { nic = addedStudent.Nic }, addedStudent);
        }

        [HttpPut("Update-Student{nic}")]
        public async Task<ActionResult> UpdateStudent(string nic, [FromBody] Student student)
        {
            var result = await _studentService.UpdateStudentAsync(nic, student);
            if (result == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("Delete-student{nic}")]
        public async Task<ActionResult> DeleteStudent(string nic)
        {
            var result = await _studentService.DeleteStudentAsync(nic);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }

    
}
