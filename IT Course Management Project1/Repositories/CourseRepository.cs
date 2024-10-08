using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString;

        public CourseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<Course> AddCourse (Course course)
        {
            var query = "INSERT INTO Course (CourseId,CourseName,CourseCategory,Duration,CourseFee,Lecturer,Description,CoursePhoto) VALUES (@courseId,@courseName,@courseCategory,@duration,@courseFee,@lecturer,@description,@coursePhoto)";
            using (var connection =  new SqlConnection (_connectionString))
            {
                var command = new SqlCommand (query, connection);

                command.Parameters.AddWithValue("@courseId", Guid.NewGuid());
                command.Parameters.AddWithValue("@courseName", course.CourseName);
                command.Parameters.AddWithValue("@courseCategory", course.CourseCategory);
                command.Parameters.AddWithValue("@duration", course.Duration);
                command.Parameters.AddWithValue("@courseFee", course.CourseFee);
                command.Parameters.AddWithValue("@lecturer", course.Lecturer);
                command.Parameters.AddWithValue("@description", course.Description);
                command.Parameters.AddWithValue("@coursePhoto", course.CoursePhoto);


                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }


            return course;
        }
    }
}
