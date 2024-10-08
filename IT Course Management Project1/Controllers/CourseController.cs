using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
    }
}
