using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(ICourseService courseService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _courseService = courseService;
        }

      
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseRequestDTO courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                Level = courseDto.Level,
                Duration = courseDto.Duration,
                Fees = courseDto.Fees,
                ImagePath = courseDto.ImagePath
            };

            try
            {
                var addedCourse = await _courseService.AddCourseAsync(course);
                return CreatedAtAction(nameof(AddCourse), new { id = addedCourse.Id }, addedCourse);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while adding the course: {ex.Message}");
            }
        }


<<<<<<< Updated upstream
=======
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseRequestDTO courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                Level = courseDto.Level,
                Duration = courseDto.Duration,
                Fees = courseDto.Fees,
                ImagePath = courseDto.ImagePath
            };

            try
            {
                var addedCourse = await _courseService.AddCourseAsync(course);
                return CreatedAtAction(nameof(AddCourse), new { id = addedCourse.Id }, addedCourse);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while adding the course: {ex.Message}");
            }
        }


>>>>>>> Stashed changes

    }
}
