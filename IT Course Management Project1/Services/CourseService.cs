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



        public async Task<Course> AddCourseAsync(Course course)
        {
            return await _courseRepository.AddCourseAsync(course);
        }


    }
}
