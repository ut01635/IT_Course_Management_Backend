using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface ICourseRepository
    {
        Task<Course> AddCourseAsync(Course course);
        Task<IEnumerable<Course>> GetAllCoursesAsync();


    }
}
