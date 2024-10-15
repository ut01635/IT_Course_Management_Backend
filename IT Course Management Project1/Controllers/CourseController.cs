using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        //// POST: api/course
        //[HttpPost]
        //public async Task<IActionResult> CreateCourse(CourseRequestDTO courseRequest)
        //{
        //    if (courseRequest == null)
        //        return BadRequest("Invalid course data.");
        //    else
        //    {
        //        try
        //        {
        //            var data = await _courseService.AddCourse(courseRequest);
        //            return Ok(data);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }

        //}
    }
}
