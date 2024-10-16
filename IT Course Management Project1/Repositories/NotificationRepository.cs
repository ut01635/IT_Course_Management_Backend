using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task AddAsync(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Notification (Message, NIC, Date) VALUES (@Message, @NIC, @Date)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Message", notification.Message);
                    command.Parameters.AddWithValue("@NIC", notification.StudentNIC);
                    command.Parameters.AddWithValue("@Date", notification.Date);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<Notification> GetByIdAsync(int id)
        {
            Notification notification = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Notification WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            notification = new Notification
                            {
                                Id = (int)reader["Id"],
                                Message = (string)reader["Message"],
                                StudentNIC = (string)reader["NIC"],
                                Date = (DateTime)reader["Date"]
                            };
                        }
                    }
                }
            }
            return notification;
        }


        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            var notifications = new List<Notification>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Notification";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        notifications.Add(new Notification
                        {
                            Id = (int)reader["Id"],
                            Message = (string)reader["Message"],
                            StudentNIC = (string)reader["NIC"],
                            Date = (DateTime)reader["Date"]
                        });
                    }
                }
            }
            return notifications;
        }


        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Notification WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
