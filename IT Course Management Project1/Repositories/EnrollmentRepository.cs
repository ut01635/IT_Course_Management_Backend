using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly string _connectionString;

        public EnrollmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO Enrollment (NIC, CourseId, EnrollmentDate, PaymentPlan, Status)
            VALUES (@studentNic, @courseId, @enrollmentDate, @paymentPlan, @status);
        ";

                // Fix the parameter name to match what is in the SQL command
                command.Parameters.AddWithValue("@studentNic", enrollment.StudentNIC);
                command.Parameters.AddWithValue("@courseId", enrollment.CourseId);
                command.Parameters.AddWithValue("@enrollmentDate", enrollment.EnrollmentDate);
                command.Parameters.AddWithValue("@paymentPlan", enrollment.PaymentPlan);
                command.Parameters.AddWithValue("@status", enrollment.Status);

                await command.ExecuteNonQueryAsync();
            }

            return enrollment;
        }


        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            var enrollments = new List<Enrollment>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Enrollment";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        enrollments.Add(new Enrollment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StudentNIC = reader.GetString(reader.GetOrdinal("NIC")),
                            CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                            PaymentPlan = reader.GetString(reader.GetOrdinal("PaymentPlan")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return enrollments;
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            Enrollment enrollment = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Enrollment WHERE Id = @id ";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        enrollment = new Enrollment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StudentNIC = reader.GetString(reader.GetOrdinal("NIC")),
                            CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                            PaymentPlan = reader.GetString(reader.GetOrdinal("PaymentPlan")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                    }
                }
            }

            return enrollment;
        }

        public async Task<int> UpdateEnrollmentAsync(int id, Enrollment enrollment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE Enrollment
                SET NIC = @studentNic,
                    CourseId = @courseId,
                    EnrollmentDate = @enrollmentDate,
                    PaymentPlan = @paymentPlan,
                    Status = @status
                WHERE Id = @id;
            ";

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@studentNic", enrollment.StudentNIC);
                command.Parameters.AddWithValue("@courseId", enrollment.CourseId);
                command.Parameters.AddWithValue("@enrollmentDate", enrollment.EnrollmentDate);
                command.Parameters.AddWithValue("@paymentPlan", enrollment.PaymentPlan);
                command.Parameters.AddWithValue("@status", enrollment.Status);

                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteEnrollmentAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Enrollment WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync();
            }
        }


        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByNicAsync(string nic)
        {
            var enrollments = new List<Enrollment>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Enrollment WHERE NIC = @nic";
                command.Parameters.AddWithValue("@nic", nic);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        enrollments.Add(new Enrollment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StudentNIC = reader.GetString(reader.GetOrdinal("NIC")),
                            CourseId = reader.GetInt32(reader.GetOrdinal("CourseId")),
                            EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate")),
                            PaymentPlan = reader.GetString(reader.GetOrdinal("PaymentPlan")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return enrollments;
        }



    }
}
