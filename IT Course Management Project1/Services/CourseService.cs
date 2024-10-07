using IT_Course_Management_Project1.Repositories;

namespace IT_Course_Management_Project1.Services
{
    public class CourseService
    {
        private readonly CourseRepository _CourseRepository;

        public CourseService(CourseRepository courseRepository)
        {
            _CourseRepository = courseRepository;
        }
    }
}
