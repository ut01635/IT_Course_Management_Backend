﻿using IT_Course_Management_Project1.Entity;
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


        public async Task<Course> AddCourseAsync(Course course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            OUTPUT INSERTED.ID
            VALUES (@courseName, @level, @duration, @fees, @imagePath);
        ";

                command.Parameters.AddWithValue("@courseName", course.CourseName);
                command.Parameters.AddWithValue("@level", course.Level);
                command.Parameters.AddWithValue("@duration", course.Duration);
                command.Parameters.AddWithValue("@fees", course.Fees);
                command.Parameters.AddWithValue("@imagePath", course.ImagePath ?? (object)DBNull.Value);

                try
                {
                    course.Id = (int)await command.ExecuteScalarAsync();
                }
                catch (SqlException sqlEx)
                {
                    throw new ApplicationException("Database operation failed.", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("An error occurred while adding the course.", ex);
                }
            }

            return course;
        }


        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var courses = new List<Course>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Course"; // Change to the correct table name

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        courses.Add(new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            Level = reader.GetString(reader.GetOrdinal("Level")),
                            Duration = reader.GetString(reader.GetOrdinal("Duration")),
                            Fees = reader.GetDecimal(reader.GetOrdinal("Fees")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                        });
                    }
                }
            }

            return courses;
        }



        public async Task<Course> GetCourseByIdAsync(int id)
        {
            Course course = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Course WHERE Id = @id"; // Corrected table name
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        course = new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            Level = reader.GetString(reader.GetOrdinal("Level")),
                            Duration = reader.GetString(reader.GetOrdinal("Duration")),
                            Fees = reader.GetDecimal(reader.GetOrdinal("Fees")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                        };
                    }
                }
            }

            return course;
        }



        public async Task<int> UpdateCourseAsync(int id, Course course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Course
                    SET CourseName = @courseName,
                        Level = @level,
                        Duration = @duration,
                        Fees = @fees,
                        ImagePath = @imagePath
                    WHERE Id = @id;
                ";

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@courseName", course.CourseName);
                command.Parameters.AddWithValue("@level", course.Level);
                command.Parameters.AddWithValue("@duration", course.Duration);
                command.Parameters.AddWithValue("@fees", course.Fees);
                command.Parameters.AddWithValue("@imagePath", course.ImagePath ?? (object)DBNull.Value);

                return await command.ExecuteNonQueryAsync(); // Returns the number of affected rows
            }
        }



        public async Task<int> DeleteCourseAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Course WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync();
            }
        }





    }
}
