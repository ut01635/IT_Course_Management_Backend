using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;


namespace IT_Course_Management_Project1.Repositories
{
    

    public class ContactUsRepository : IContactUsRepository
    {
        private readonly string _connectionString;

        public ContactUsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ContactUs AddContactUsDetails(ContactUs contactUs)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO ContactUs (Name, Email, Message, SubmitDate) VALUES (@Name, @Email, @Message, @SubmitDate); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", contactUs.Name);
                    command.Parameters.AddWithValue("@Email", contactUs.Email);
                    command.Parameters.AddWithValue("@Message", contactUs.Message);
                    command.Parameters.AddWithValue("@SubmitDate", DateTime.Now);

                    var result = command.ExecuteScalar();
                    contactUs.Id = Convert.ToInt32(result);
                }
            }
            return contactUs;
        }

        public List<ContactUs> GetAllContacts()
        {
            var contacts = new List<ContactUs>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ContactUs";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new ContactUs
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Message = reader["Message"].ToString(),
                            SubmitDate = (DateTime)reader["SubmitDate"]
                        };
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }

        public void EditContactUsDetails(ContactUs contactUs)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE ContactUs SET Name = @Name, Email = @Email, Message = @Message WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", contactUs.Id);
                    command.Parameters.AddWithValue("@Name", contactUs.Name);
                    command.Parameters.AddWithValue("@Email", contactUs.Email);
                    command.Parameters.AddWithValue("@Message", contactUs.Message);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteContactUsDetails(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM ContactUs WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<ContactUs> GetContactsByDate(DateTime date)
        {
            var contacts = new List<ContactUs>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ContactUs WHERE CAST(SubmitDate AS DATE) = @Date";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var contact = new ContactUs
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Message = reader["Message"].ToString(),
                                SubmitDate = (DateTime)reader["SubmitDate"]
                            };
                            contacts.Add(contact);
                        }
                    }
                }
            }

            return contacts;
        }


    }

}
