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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO ContactUS (Id, Name, Email, Message, SubmitDate) VALUES (@id, @name, @email, @message, @submitDate);";
                command.Parameters.AddWithValue("@id", contactUs.Id);
                command.Parameters.AddWithValue("@name", contactUs.Name);
                command.Parameters.AddWithValue("@email", contactUs.Email);
                command.Parameters.AddWithValue("@message", contactUs.Message);
                command.Parameters.AddWithValue("@submitDate", contactUs.SubmitDate);
                command.ExecuteNonQuery();
            }
            return contactUs;
        }

        public List<ContactUs> GetAllContacts()
        {
            var contacts = new List<ContactUs>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, Email, Message, SubmitDate FROM ContactUS;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new ContactUs
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Message = reader.GetString(3),
                            SubmitDate = reader.GetDateTime(4)
                        };
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }

        public void EditContactUsDetails(ContactUs contactUs)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE ContactUS SET Name = @name, Email = @email, Message = @message, SubmitDate = @submitDate WHERE Id = @id;";
                command.Parameters.AddWithValue("@id", contactUs.Id);
                command.Parameters.AddWithValue("@name", contactUs.Name);
                command.Parameters.AddWithValue("@email", contactUs.Email);
                command.Parameters.AddWithValue("@message", contactUs.Message);
                command.Parameters.AddWithValue("@submitDate", contactUs.SubmitDate);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteContactUsDetails(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM ContactUS WHERE Id = @id;";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<ContactUs> GetContactsByDate(DateTime date)
        {
            var contacts = new List<ContactUs>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, Email, Message, SubmitDate FROM ContactUS WHERE CAST(SubmitDate AS DATE) = @date;";
                command.Parameters.AddWithValue("@date", date.Date);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contact = new ContactUs
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Message = reader.GetString(3),
                            SubmitDate = reader.GetDateTime(4)
                        };
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }
    }

}
