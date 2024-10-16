using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using System.Data.SqlClient;

namespace IT_Course_Management_Project1.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Admin (NIC, Password) VALUES (@nic, @password);";
                command.Parameters.AddWithValue("@nic", admin.NIC);
                command.Parameters.AddWithValue("@password", admin.Password); // Ensure this is hashed

                await command.ExecuteNonQueryAsync();
                return admin;
            }
        }

        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            var admins = new List<Admin>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT NIC, Password FROM Admin;";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var admin = new Admin
                        {
                            NIC = reader.GetString(0),
                            Password = reader.GetString(1)
                        };
                        admins.Add(admin);
                    }
                }
            }

            return admins;
        }

        public async Task<Admin> GetAdminByNICAsync(string nic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT NIC, Password FROM Admin WHERE NIC = @nic;";
                command.Parameters.AddWithValue("@nic", nic);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Admin
                        {
                            NIC = reader.GetString(0),
                            Password = reader.GetString(1)
                        };
                    }
                }
            }
            return null; // Not found
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Admin SET Password = @password WHERE NIC = @nic;";
                command.Parameters.AddWithValue("@nic", admin.NIC);
                command.Parameters.AddWithValue("@password", admin.Password); // Ensure this is hashed

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAdminAsync(string nic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Admin WHERE NIC = @nic;";
                command.Parameters.AddWithValue("@nic", nic);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
