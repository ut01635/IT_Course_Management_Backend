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


        public async Task<Course> AddCourse(Course course)
        {
            var query = "INSERT INTO Course (CourseName,Level,TotalFee,Duration,ImagePath) VALUES (@courseName,@level,@totalFee,@duration,@imagePath)";
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                command.Parameters.AddWithValue("@courseName", course.CourseName);
                command.Parameters.AddWithValue("@level", course.Level);
                command.Parameters.AddWithValue("@totalFee", course.TotalFee);
                command.Parameters.AddWithValue("@duration", course.Duration);
                command.Parameters.AddWithValue("@imagePath", course.ImagePath);
               

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }


            return course;
        }
    }
}
