using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet ("Get-all-enrollmetnt")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        [HttpGet("Get-enrollmetnt-By{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }

        [HttpPost ("Create-Enrollment")]
        public async Task<IActionResult> AddEnrollment([FromBody] Enrollment enrollment)
        {
            if (enrollment == null)
            {
                return BadRequest();
            }

            var addedEnrollment = await _enrollmentService.AddEnrollmentAsync(enrollment);
            return CreatedAtAction(nameof(GetEnrollmentById), new { id = addedEnrollment.Id }, addedEnrollment);
        }

        [HttpPut("Edit-Enrollment{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] Enrollment enrollment)
        {
            if (enrollment == null || id != enrollment.Id)
            {
                return BadRequest();
            }

            var result = await _enrollmentService.UpdateEnrollmentAsync(id, enrollment);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("Delete-Enrollment{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
               return Ok( await _enrollmentService.DeleteEnrollmentAsync(id));
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("by-nic/{nic}")]
        public async Task<IActionResult> GetEnrollmentsByNic(string nic)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByNicAsync(nic);
            if (enrollments == null || !enrollments.Any())
            {
                return NotFound($"No enrollments found for NIC: {nic}");
            }
            return Ok(enrollments);
        }


        [HttpGet("by-course-id/{courseId}")]
        public async Task<IActionResult> GetEnrollmentsByCourseId(int courseId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByCourseIdAsync(courseId);
            if (enrollments == null || !enrollments.Any())
            {
                return NotFound($"No enrollments found for Course ID: {courseId}");
            }
            return Ok(enrollments);
        }



    }
}
