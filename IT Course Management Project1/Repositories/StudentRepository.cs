using IT_Course_Management_Project1.DTOs.RequestDtos;
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
                                //Id = (Guid)reader["Id"],
                                NIC = (string)reader["NIC"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                PhoneNumber = reader["PhoneNumber"] as string,
                                Email = reader["Email"] as string,
                                PassWord = reader["PassWord"] as string,
                                RegistrationFee = (int)reader["RegistrationFee"],
                                ImagePath = (string)reader["ImagePath"]
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
                            //Id = (Guid)reader["Id"],
                            NIC = (string)reader["NIC"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            PhoneNumber = reader["PhoneNumber"] as string,
                            Email = reader["Email"] as string,
                            PassWord = reader["PassWord"] as string,
                            RegistrationFee = (int)reader["RegistrationFee"],
                            ImagePath = (string)reader["ImagePath"]
                        });
                    }
                }
            }

            return students;
        }

        public async Task<Student> AddStudent(Student student)
        {
            var query = "INSERT INTO Student (Nic , FullName , Email , Phone , Password , RegistrationFee , ImagePath ) VALUES (@nic,@firstName,@lastName,@email,@phone,@password,@registerFee,@imagePath)";

            using (var connection = new SqlConnection(_connectionstring))
            {

                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@nic", student.NIC);
                command.Parameters.AddWithValue("@firstName", student.FirstName);
                command.Parameters.AddWithValue("@lastName", student.LastName);
                command.Parameters.AddWithValue("@phoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@email", student.Email);
                command.Parameters.AddWithValue("@passWord", student.PassWord);
                command.Parameters.AddWithValue("@registerFee", student.RegistrationFee);
                command.Parameters.AddWithValue("@imagePath", student.ImagePath); ;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            return student;
        }

        public async Task<Student> UpdateStudent(string NIC, Student student)
        {
            string query = "UPDATE Students SET FullName = @name , Email = @email , Phone = @phone  WHERE Nic = @nic";

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", student.FirstName);
                    command.Parameters.AddWithValue("@lastName", student.LastName);
                    command.Parameters.AddWithValue("@email", student.Email);
                    command.Parameters.AddWithValue("@phone", student.PhoneNumber);
                    command.Parameters.AddWithValue("@nic", student.NIC);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return student;
        }



        public async Task AddCourseEnrollId(string Nic, int CourseEnrollId)
        {
            var student = await GetStudentByNIC(Nic);
            if (student != null)
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Students SET CourseEnrollId = @courseEnrollId WHERE Nic = @nic";

                    // Adding parameters for SQL Server
                    command.Parameters.AddWithValue("@courseEnrollId", CourseEnrollId);
                    command.Parameters.AddWithValue("@nic", Nic);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                throw new Exception("Student Not Found!");
            }
        }

        public async Task PasswordUpdate(string Nic, PasswordUpdateRequestDTO newPassword)
        {
            var student = await GetStudentByNIC(Nic);
            if (student != null)
            {
                using (var connection = new SqlConnection(_connectionstring))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Students SET Password = @newPassword  WHERE Nic = @nic";
                    command.Parameters.AddWithValue("@newPassword", newPassword.NewPassword);
                    command.Parameters.AddWithValue("@nic", Nic);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                throw new Exception("Student Not Found!");
            }

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
