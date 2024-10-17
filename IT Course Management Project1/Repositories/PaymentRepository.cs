using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using Microsoft.Data.SqlClient;

public class PaymentRepository : IPaymentRepository
{
    private readonly string _connectionString;

    public PaymentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Add Payment
    public async Task<Payment> AddPaymentAsync(Payment payment)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Payment (EnrollmentID, Nic, PaymentDate, Amount)
                OUTPUT INSERTED.ID
                VALUES (@enrollmentId, @nic, @paymentDate, @amount);
            ";

            command.Parameters.AddWithValue("@enrollmentId", payment.EnrollmentID);
            command.Parameters.AddWithValue("@nic", payment.Nic);
            command.Parameters.AddWithValue("@paymentDate", payment.PaymentDate);
            command.Parameters.AddWithValue("@amount", payment.Amount);

            try
            {
                payment.ID = (int)await command.ExecuteScalarAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while adding the payment.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the payment.", ex);
            }
        }

        return payment;
    }

    // Get All Payments
    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        var payments = new List<Payment>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Payment";

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        payments.Add(new Payment
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            EnrollmentID = reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                        });
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while fetching payments.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching payments.", ex);
            }
        }

        return payments;
    }

    // Get Payment by ID
    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        Payment payment = null;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Payment WHERE ID = @id";
            command.Parameters.AddWithValue("@id", id);

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        payment = new Payment
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            EnrollmentID = reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                        };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while fetching the payment.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the payment.", ex);
            }
        }

        return payment;
    }

    // Get Payments by Enrollment ID
    public async Task<IEnumerable<Payment>> GetPaymentsByEnrollmentIdAsync(int enrollmentId)
    {
        var payments = new List<Payment>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Payment WHERE EnrollmentID = @enrollmentId";
            command.Parameters.AddWithValue("@enrollmentId", enrollmentId);

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        payments.Add(new Payment
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            EnrollmentID = reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                        });
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while fetching payments by Enrollment ID.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching payments by Enrollment ID.", ex);
            }
        }

        return payments;
    }

    // Get Payments by NIC
    public async Task<IEnumerable<Payment>> GetPaymentsByNicAsync(string nic)
    {
        var payments = new List<Payment>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Payment WHERE Nic = @nic";
            command.Parameters.AddWithValue("@nic", nic);

            try
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        payments.Add(new Payment
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            EnrollmentID = reader.GetInt32(reader.GetOrdinal("EnrollmentID")),
                            Nic = reader.GetString(reader.GetOrdinal("Nic")),
                            PaymentDate = reader.GetDateTime(reader.GetOrdinal("PaymentDate")),
                            Amount = reader.GetDecimal(reader.GetOrdinal("Amount"))
                        });
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while fetching payments by NIC.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching payments by NIC.", ex);
            }
        }

        return payments;
    }

    // Update Payment
    public async Task<Payment> UpdatePaymentAsync(Payment payment)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Payment
                SET EnrollmentID = @enrollmentId, Nic = @nic, PaymentDate = @paymentDate, Amount = @amount
                WHERE ID = @id;
            ";

            command.Parameters.AddWithValue("@id", payment.ID);
            command.Parameters.AddWithValue("@enrollmentId", payment.EnrollmentID);
            command.Parameters.AddWithValue("@nic", payment.Nic);
            command.Parameters.AddWithValue("@paymentDate", payment.PaymentDate);
            command.Parameters.AddWithValue("@amount", payment.Amount);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while updating the payment.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the payment.", ex);
            }
        }

        return payment;
    }

    // Delete Payment
    public async Task<bool> DeletePaymentAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Payment WHERE ID = @id";
            command.Parameters.AddWithValue("@id", id);

            try
            {
                var rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Database operation failed while deleting the payment.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the payment.", ex);
            }
        }
    }
}
