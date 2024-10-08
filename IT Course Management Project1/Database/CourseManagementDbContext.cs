using Microsoft.Data.SqlClient;

namespace IT_Institute_API.DataBase
{
    public class DatabaseInitializer
    {
        private readonly string _ConnectionString;

        public DatabaseInitializer(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void Initialize()
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    USE CourseManagementDB                    

                    CREATE TABLE IF NOT EXISTS Student (
                        NIC NVARCHAR(15) PRIMARY KEY,
                        FirstName NVARCHAR(25) NOT NULL,
                        LastName NVARCHAR(25) NOT NULL,
                        Email NVARCHAR(25) NOT NULL,
                        PhoneNumber NVARCHAR(15) NOT NULL,
                        PassWord NVARCHAR(50) NOT NULL,
                        RegistrationFee INT NOT NULL,
                        CourseEnrollId INT NULL,
                        ImagePath NVARCHAR(100) NULL
                    );

                    CREATE TABLE IF NOT EXISTS Course (
                        Id INT PRIMARY KEY,
                        CourseName NVARCHAR(25) NOT NULL,
                        Level NVARCHAR(25) NOT NULL,
                        TotalFee INT NOT NULL,
                        Duration NVARCHAR(5) NOT NULL,
                        ImagePath NVARCHAR(100) NULL
                    );

                    CREATE TABLE IF NOT EXISTS CourseEnrollDetail (
                        Id INT PRIMARY KEY,
                        Nic NVARCHAR(15) NOT NULL,
                        CourseId INT NOT NULL,
                        InstallmentId INT NULL,
                        FullPaymentId INT NULL,
                        CourseEnrollDate DATE NOT NULL,
                        Status NVARCHAR(10) NOT NULL,
                        FOREIGN KEY (CourseId) REFERENCES Courses(Id) ON DELETE CASCADE,
                        FOREIGN KEY (Nic) REFERENCES Students(Nic) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS FullPayment (
                        Id INT PRIMARY KEY,
                        Nic NVARCHAR(15) NOT NULL,
                        FullPayment INT NOT NULL,
                        PaymentDate DATE NOT NULL,
                        FOREIGN KEY (Nic) REFERENCES Students(Nic) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS Installments (
                        Id INT PRIMARY KEY,
                        Nic NVARCHAR(15) NOT NULL,
                        TotalAmount DECIMAL(18, 2) NOT NULL,
                        InstallmentAmount DECIMAL(18, 2) NOT NULL,
                        Installments NVARCHAR(5) NOT NULL,
                        PaymentDue DECIMAL(18, 2) NOT NULL,
                        PaymentPaid DECIMAL(18, 2) NOT NULL,
                        PaymentDate DATE NOT NULL,
                        FOREIGN KEY (Nic) REFERENCES Students(Nic) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS ContactUS (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(50) NOT NULL,
                        Email NVARCHAR(50) NOT NULL,
                        Message NVARCHAR(500) NOT NULL,
                        SubmitDate DATE NOT NULL
                    );

                    IF NOT EXISTS (SELECT 1 FROM Students WHERE Nic = '200224204071')
                    BEGIN
                        INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId, ImagePath)
                        VALUES
                        ('200224204071', 'Hameem Imthath', 'ut01635tic@gmail.com.com', '0768210306', 'MTIzNDU2Nzg=', 2500, 452320, NULL),
                        ('200224204072', 'John Doe', 'john.doe@gmail.com', '0701234567', 'YWJjZGVmZw==', 3000, NULL, NULL),
                        ('200224204073', 'Jane Smith', 'jane.smith@gmail.com', '0709876543', 'c2Rmc2Rn', 3500, NULL, NULL),
                        ('200224204074', 'Alex Turner', 'alex.turner@yahoo.com', '0705555555', 'YXNkc2FzZA==', 2000, NULL, NULL),
                        ('200224204075', 'Mary Johnson', 'mary.johnson@gmail.com', '0706666666', 'bXJ5c2xkZGZzZA==', 4000, NULL, NULL),
                        ('200224204076', 'Michael Brown', 'michael.brown@gmail.com', '0707777777', 'bWljc2FsdGVzdA==', 2200, NULL, NULL),
                        ('200224204077', 'Sarah White', 'sarah.white@gmail.com', '0708888888', 'c2FyYWh3aXRl', 2700, NULL, NULL),
                        ('200224204078', 'David Green', 'david.green@gmail.com', '0709999999', 'ZGF2aWRncmVlbl9hY2NvdW50', 2600, NULL, NULL),
                        ('200224204079', 'Emma Wilson', 'emma.wilson@yahoo.com', '0704444444', 'ZW1tYXdpbG9uZXNnMzg=', 2800, NULL, NULL),
                        ('200224204080', 'James Lee', 'james.lee@gmail.com', '0703333333', 'amFtZXMuZm9vZGZhc3Q==', 2300, NULL, NULL);
                    END;

                    IF NOT EXISTS (SELECT 1 FROM Courses WHERE Id = 314917)
                    BEGIN
                        INSERT INTO Courses (Id, CourseName, Level, TotalFee, Duration, ImagePath )
                        VALUES
                        (314917, 'Python', 'Intermediate', 24000,'06-Months',NULL),
                        (314918, 'C#', 'Beginner', 15000,'08-Months',NULL),
                        (314919, 'JavaScript', 'Advanced', 18000,'07-Months',NULL),
                        (314920, 'SQL', 'Intermediate', 13000,'08-Months',NULL),
                        (314921, 'Java', 'Advanced', 22000,'05-Months',NULL);
                    END;

                    -- Sample Insert Data for CourseEnrollDetails
                    IF NOT EXISTS (SELECT 1 FROM CourseEnrollDetails WHERE Id = 452320)
                    BEGIN
                        INSERT INTO CourseEnrollDetails (Id, Nic, CourseId, InstallmentId, FullPaymentId, CourseEnrollDate, Status)
                        VALUES
                        (452320, '200224204071', 314917,  838045, NULL, '2024-09-16', 'Active'),
                        (452321, '200224204072', 314918,  838046, NULL, '2024-09-15', 'Active'),
                        (452322, '200224204073', 314919,  838047, NULL, '2024-09-14', 'Active'),
                        (452323, '200224204074', 314920,  838048, NULL, '2024-09-13', 'Inactive'),
                        (452324, '200224204075', 314921,  838049, NULL, '2024-09-12', 'Active'),
                        (452325, '200224204076', 314917,  838050, NULL, '2024-09-11', 'Active'),
                        (452326, '200224204077', 314919,  838051, NULL, '2024-09-10', 'Active'),
                        (452327, '200224204078', 314920,  838052, NULL, '2024-09-09', 'Inactive'),
                        (452328, '200224204079', 314921,  838053, NULL, '2024-09-08', 'Active'),
                        (452329, '200224204080', 314918,  838054, NULL, '2024-09-07', 'Active');
                    END;

                    -- Sample Insert Data for Installments
                    IF NOT EXISTS (SELECT 1 FROM Installments WHERE Id = 838045)
                    BEGIN
                        INSERT INTO Installments (Id, Nic, TotalAmount, InstallmentAmount, Installments, PaymentDue, PaymentPaid, PaymentDate)
                        VALUES
                        (838045, '200224204071', 24000, 8000, '3', 16000, 8000, '2024-09-16'),
                        (838046, '200224204072', 15000, 3750, '4', 11250, 3750, '2024-09-15'),
                        (838047, '200224204073', 18000, 9000, '2', 9000, 9000, '2024-09-14'),
                        (838048, '200224204074', 13000, 2600, '5', 10400, 2600, '2024-09-13'),
                        (838049, '200224204075', 22000, 3666.67, '6', 19833.33, 3666.67, '2024-09-12'),
                        (838050, '200224204076', 24000, 24000, '1', 0, 24000, '2024-09-11'),
                        (838051, '200224204077', 18000, 6000, '3', 12000, 6000, '2024-09-10'),
                        (838052, '200224204078', 13000, 13000, '2', 0, 13000, '2024-09-09'),
                        (838053, '200224204079', 22000, 5500, '4', 16500, 5500, '2024-09-08'),
                        (838054, '200224204080', 15000, 3000, '5', 12000, 3000, '2024-09-07');
                    END;
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}

