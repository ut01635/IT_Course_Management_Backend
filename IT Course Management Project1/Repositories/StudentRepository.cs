using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Add a new student
        public async Task<Student> AddStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, ImagePath)
                VALUES (@nic, @fullName, @email, @phone, @password, @registrationFee, @imagePath);
            ";

                command.Parameters.AddWithValue("@nic", student.Nic);
                command.Parameters.AddWithValue("@fullName", student.FullName);
                command.Parameters.AddWithValue("@email", student.Email);
                command.Parameters.AddWithValue("@phone", student.Phone);
                command.Parameters.AddWithValue("@password", student.Password);
                command.Parameters.AddWithValue("@registrationFee", student.RegistrationFee);
                command.Parameters.AddWithValue("@imagePath", student.ImagePath ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }

            return student;
        }

        // Get all students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Students";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        students.Add(new Student
                        {
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            RegistrationFee = reader.GetInt32(reader.GetOrdinal("RegistrationFee")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                        });
                    }
                }
            }

            return students;
        }

        // Get student by NIC
        public async Task<Student> GetStudentByNicAsync(string nic)
        {
            Student student = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Students WHERE Nic = @nic";
                command.Parameters.AddWithValue("@nic", nic);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        student = new Student
                        {
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            RegistrationFee = reader.GetInt32(reader.GetOrdinal("RegistrationFee")),
                            ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                        };
                    }
                }
            }

            return student;
        }

        // Update student
        public async Task<Student> UpdateStudentAsync(string nic, Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE Students 
                SET FullName = @fullName, 
                    Email = @email, 
                    Phone = @phone, 
                    Password = @password, 
                    RegistrationFee = @registrationFee, 
                    ImagePath = @imagePath
                WHERE Nic = @nic
            ";

                command.Parameters.AddWithValue("@nic", nic);
                command.Parameters.AddWithValue("@fullName", student.FullName);
                command.Parameters.AddWithValue("@email", student.Email);
                command.Parameters.AddWithValue("@phone", student.Phone);
                command.Parameters.AddWithValue("@password", student.Password);
                command.Parameters.AddWithValue("@registrationFee", student.RegistrationFee);
                command.Parameters.AddWithValue("@imagePath", student.ImagePath ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
                return student;
            }
        }

        // Delete student by NIC
        public async Task<int> DeleteStudentAsync(string nic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Students WHERE Nic = @nic";
                command.Parameters.AddWithValue("@nic", nic);

                return await command.ExecuteNonQueryAsync();
            }
        }


        public async Task PasswordUpdateAsync(string nic, string newPassword)
        {
            var student = await GetStudentByNicAsync(nic);
            if (student == null)
            {
                throw new Exception("Student Not Found!");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Students SET Password = @newPassword WHERE Nic = @nic";
                command.Parameters.AddWithValue("@newPassword", newPassword);
                command.Parameters.AddWithValue("@nic", nic);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to update password. No rows affected.");
                }
            }
        }
        

    }
}
