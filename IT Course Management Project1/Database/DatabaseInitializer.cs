using Microsoft.Data.SqlClient;

namespace IT_Course_Management_Project1.Database
{
    public class DatabaseInitializer
    {

        private readonly string _ConnectionString;
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;database=master;"; // Use master to create the new database
        private readonly string _database = "Server=(localdb)\\MSSQLLocalDB;database=InstituteManagement;";

        public DatabaseInitializer()
        {
        }

        public DatabaseInitializer(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void InitializeDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // SQL command to create the database
                string createDatabaseSql = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'InstituteManagement') " +
                                            "CREATE DATABASE InstituteManagement;";
                using (SqlCommand command = new SqlCommand(createDatabaseSql, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database 'InstituteManagement' created successfully or already exists.");
                }

                // Change the connection string to point to the new database
                //connection.ChangeDatabase("InstituteManagement");
                string useDatabase = "USE InstituteManagement;";
                using (SqlCommand cmd = new SqlCommand(useDatabase, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void CreateTable()
        {


            // SQL command to create the tables
            string createTablesQuery = @"
                    -- Create Students Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Students' AND xtype = 'U')
            BEGIN
                CREATE TABLE Students (
                    Nic NVARCHAR(15) PRIMARY KEY,
                    FullName NVARCHAR(25) NOT NULL,
                    Email NVARCHAR(25) NOT NULL,
                    Phone NVARCHAR(15) NOT NULL,
                    Password NVARCHAR(50) NOT NULL,
                    RegistrationFee INT NOT NULL,
                    CourseEnrollId INT NULL,
                    ImagePath NVARCHAR(100) NULL
                );
            END;

            -- Create Course Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Course' AND xtype = 'U')
            BEGIN
                CREATE TABLE Course (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    CourseName NVARCHAR(200) NOT NULL,
                    Level NVARCHAR(50) NOT NULL,
                    Duration NVARCHAR(100) NOT NULL,
                    Fees DECIMAL(18, 2) NOT NULL,
                    ImagePath NVARCHAR(200) NULL
                );
            END;

            -- Create Enrollment Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Enrollment' AND xtype = 'U')
            BEGIN
                CREATE TABLE Enrollment (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    NIC NVARCHAR(15) NOT NULL,
                    CourseId INT NOT NULL,
                    EnrollmentDate DATETIME NOT NULL,
                    PaymentPlan NVARCHAR(100) NOT NULL,
                    Status NVARCHAR(50) NOT NULL,
                    FOREIGN KEY (NIC) REFERENCES Students(Nic),
                    FOREIGN KEY (CourseId) REFERENCES Course(ID)
                );
            END;

            -- Create Payment Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Payment' AND xtype = 'U')
            BEGIN
                CREATE TABLE Payment (
                    ID INT PRIMARY KEY IDENTITY(1,1),
                    EnrollmentID INT NOT NULL,
                    PaymentDate DATETIME NOT NULL,
                    Amount DECIMAL(18, 2) NOT NULL,
                    FOREIGN KEY (EnrollmentID) REFERENCES Enrollment(ID)
                );
            END;

            -- Create Notification Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Notification' AND xtype = 'U')
            BEGIN
                CREATE TABLE Notification (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Message NVARCHAR(MAX) NOT NULL,
                    NIC NVARCHAR(15) NOT NULL,
                    Date DATETIME NOT NULL,
                    FOREIGN KEY (NIC) REFERENCES Students(Nic)
                );
            END;

            -- Create ContactUs Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'ContactUs' AND xtype = 'U')
            BEGIN
                CREATE TABLE ContactUs (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(200) NOT NULL,
                    Email NVARCHAR(100) NOT NULL,
                    Message NVARCHAR(MAX) NOT NULL,
                    Date DATETIME NOT NULL
                );
            END;

            -- Create Admin Table if it does not exist
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Admin' AND xtype = 'U')
            BEGIN
                CREATE TABLE Admin (
                    NIC NVARCHAR(15) PRIMARY KEY,
                    Password NVARCHAR(50) NOT NULL
                );
            END;

                   
                ";
            using (SqlConnection connection = new SqlConnection(_database))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(createTablesQuery, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Tables created (if not exist) successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }



        }


        public void InserSampleData()
        {
            string insertDataQuery = @"
            -- Insert sample data into Students table
            INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId, ImagePath)
            VALUES 
            ('1234567890123', 'John Doe', 'john.doe@example.com', '123-456-7890', 'password123', 500, 1, 'C:\\Images\\student1.jpg'),
            ('9876543210987', 'Jane Smith', 'jane.smith@example.com', '098-765-4321', 'password456', 300, NULL, 'C:\\Images\\student2.jpg');

            -- Insert sample data into Course table
            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            VALUES 
            ('Introduction to Programming', 'Beginner', '3 months', 250, 'C:\\Images\\course1.jpg'),
            ('Advanced Database Management', 'Advanced', '6 months', 400, 'C:\\Images\\course2.jpg'),
            ('Web Development with C#', 'Intermediate', '4 months', 350, 'C:\\Images\\course3.jpg');

            -- Insert sample data into Enrollment table
            INSERT INTO Enrollment (NIC, CourseId, EnrollmentDate, PaymentPlan, Status)
            VALUES 
            ('1234567890123', 1, '2024-10-14', 'Full Payment', 'Active'),
            ('9876543210987', 2, '2024-10-14', 'Installments', 'Active');

            -- Insert sample data into Payment table
            INSERT INTO Payment (EnrollmentID, PaymentDate, Amount)
            VALUES 
            (1, '2024-10-14', 250),
            (2, '2024-10-14', 200);

            -- Insert sample data into Notification table
            INSERT INTO Notification (Message, NIC, Date)
            VALUES 
            ('Your course enrollment has been confirmed.', '1234567890123', '2024-10-14'),
            ('Your payment has been processed successfully.', '9876543210987', '2024-10-14');

            -- Insert sample data into ContactUs table
            INSERT INTO ContactUs (Name, Email, Message, Date)
            VALUES 
            ('Alice Johnson', 'alice.johnson@example.com', 'I need help with my enrollment process.', '2024-10-14'),
            ('Bob Lee', 'bob.lee@example.com', 'How can I access my course materials?', '2024-10-14');

            -- Insert sample data into Admin table
            INSERT INTO Admin (NIC, Password)
            VALUES 
            ('1111111111111', 'adminpassword1'),
            ('2222222222222', 'adminpassword2');
        ";

            using (SqlConnection connection = new SqlConnection(_database))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertDataQuery, connection);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Sample data inserted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
