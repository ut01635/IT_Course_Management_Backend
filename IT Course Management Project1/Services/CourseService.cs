using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;
using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Repositories;

namespace IT_Course_Management_Project1.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }



        public async Task<CourseResponsDTO> AddCourse(CourseRequestDTO courseRequest)
        {
           

            // Create a new course object
            var course = new Course
            {
                CourseName = courseRequest.CourseName,
                Level = courseRequest.Level,
                TotalFee = courseRequest.TotalFee,
                Duration = courseRequest.Duration,
   
            };

            // Add the course to the repository
            await _courseRepository.AddCourse(course);

            // Create and return the response DTO
            var response = new CourseResponsDTO
            {
                Id = course.Id,
                Level = course.Level,
                TotalFee = course.TotalFee,
                Duration = course.Duration,
            };
            return response;
        }
    }
}
