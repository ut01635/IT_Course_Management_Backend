using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionstring;

        public StudentRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }


        //Add Students
        //public async Task<Student> AddBike(Student student)
        //{
        //    using (var connection = new SqlConnection(_connectionstring))
        //    {
        //        var command = new SqlCommand(
        //            "INSERT INTO Students (Id, NIC, FirstName, LastName, DOB, Age, PhoneNumber, Email, PassWord) VALUES (@id, @nic, @firstName, @lastName, @dob, @age, @phoneNumber, @email, @passWord)", connection);

        //        command.Parameters.AddWithValue("@id", Guid.NewGuid()); // Assuming you're generating a new GUID
        //        command.Parameters.AddWithValue("@nic", student.NIC);
        //        command.Parameters.AddWithValue("@firstName", student.FirstName);
        //        command.Parameters.AddWithValue("@lastName", student.LastName);
        //        command.Parameters.AddWithValue("@dob", student.DOB);
        //        command.Parameters.AddWithValue("@age", student.Age);
        //        command.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
        //        command.Parameters.AddWithValue("@email", student.Email);
        //        command.Parameters.AddWithValue("@passWord", student.PassWord);


        //        await connection.OpenAsync();
        //        await command.ExecuteNonQueryAsync();
        //    }

        //    return student;
        //}

        public async Task<Student> GetStudentByNIC(string NIC)
        {
            Student student = null;
            string query = "SELECT * FROM Student WHERE NIC = @NIC";

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NIC", NIC);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            student = new Student
                            {
                                Id = (Guid)reader["Id"],
                                NIC = (string)reader["NIC"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DOB = (DateTime)reader["DOB"],
                                Age = (int)reader["Age"],
                                PhoneNumber = reader["PhoneNumber"] as string,
                                Email = reader["Email"] as string,
                                PassWord = reader["PassWord"] as string
                            };
                        }
                    }
                }
            }

            return student;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var students = new List<Student>();
            string query = "SELECT * FROM Student";

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        students.Add(new Student
                        {
                            Id = (Guid)reader["Id"],
                            NIC = (string)reader["NIC"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            DOB = (DateTime)reader["DOB"],
                            Age = (int)reader["Age"],
                            PhoneNumber = reader["PhoneNumber"] as string,
                            Email = reader["Email"] as string,
                            PassWord = reader["PassWord"] as string
                        });
                    }
                }
            }

            return students;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var query = "INSERT INTO Student (Id, NIC, FirstName, LastName, DOB, Age, PhoneNumber, Email, PassWord) VALUES (@id, @nic, @firstName, @lastName, @dob, @age, @phoneNumber, @email, @passWord)";

            using (var connection = new SqlConnection(_connectionstring))
            {

                var command = new SqlCommand(query, connection);
       
                    command.Parameters.AddWithValue("@id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@nic", student.NIC);
                    command.Parameters.AddWithValue("@firstName", student.FirstName);
                    command.Parameters.AddWithValue("@lastName", student.LastName);
                    command.Parameters.AddWithValue("@dob", student.DOB);
                    command.Parameters.AddWithValue("@age", student.Age);
                    command.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("@passWord", student.PassWord);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                
            }
            return student;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            string query = "UPDATE Student SET NIC = @nic, FirstName = @firstName, LastName = @lastName, DOB = @dob, Age = @age, PhoneNumber = @phoneNumber, Email = @email, PassWord = @passWord WHERE Id = @id";

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", student.Id);
                    command.Parameters.AddWithValue("@nic", student.NIC);
                    command.Parameters.AddWithValue("@firstName", student.FirstName);
                    command.Parameters.AddWithValue("@lastName", student.LastName);
                    command.Parameters.AddWithValue("@dob", student.DOB);
                    command.Parameters.AddWithValue("@age", student.Age);
                    command.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("@passWord", student.PassWord); // Ensure this is hashed before storage

                    await command.ExecuteNonQueryAsync();
                }
            }

            return student;
        }

        public async Task DeleteStudents(string NIC)
        {
            string query = "DELETE FROM Student WHERE NIC = @NIC";

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NIC", NIC);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
