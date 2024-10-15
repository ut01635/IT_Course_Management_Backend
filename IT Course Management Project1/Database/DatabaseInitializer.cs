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
                    NIC NVARCHAR(15) PRIMARY KEY,
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
                    FOREIGN KEY (NIC) REFERENCES Students(NIC),
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
            BEGIN TRANSACTION;

            -- Insert sample data into Students table if it does not exist
            INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId, ImagePath)
            SELECT @Nic1, @FullName1, @Email1, @Phone1, @Password1, @RegistrationFee1, @CourseEnrollId1, @ImagePath1
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE Nic = @Nic1);

            INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId, ImagePath)
            SELECT @Nic2, @FullName2, @Email2, @Phone2, @Password2, @RegistrationFee2, @CourseEnrollId2, @ImagePath2
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE Nic = @Nic2);

            -- Insert sample data into Course table if it does not exist
            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName1, @Level1, @Duration1, @Fees1, @ImagePathCourse1
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName1);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName2, @Level2, @Duration2, @Fees2, @ImagePathCourse2
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName2);

            -- Additional inserts for other tables can be added here

            COMMIT;
            ";

            using (SqlConnection connection = new SqlConnection(_database))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertDataQuery, connection))
                    {
                        // Parameters for Students
                        command.Parameters.AddWithValue("@Nic1", "1234567890123");
                        command.Parameters.AddWithValue("@FullName1", "John Doe");
                        command.Parameters.AddWithValue("@Email1", "john.doe@example.com");
                        command.Parameters.AddWithValue("@Phone1", "123-456-7890");
                        command.Parameters.AddWithValue("@Password1", "hashed_password1"); // Replace with hashed password
                        command.Parameters.AddWithValue("@RegistrationFee1", 500);
                        command.Parameters.AddWithValue("@CourseEnrollId1", 1);
                        command.Parameters.AddWithValue("@ImagePath1", @"C:\Images\student1.jpg");

                        command.Parameters.AddWithValue("@Nic2", "9876543210987");
                        command.Parameters.AddWithValue("@FullName2", "Jane Smith");
                        command.Parameters.AddWithValue("@Email2", "jane.smith@example.com");
                        command.Parameters.AddWithValue("@Phone2", "098-765-4321");
                        command.Parameters.AddWithValue("@Password2", "hashed_password2"); // Replace with hashed password
                        command.Parameters.AddWithValue("@RegistrationFee2", 300);
                        command.Parameters.AddWithValue("@CourseEnrollId2", DBNull.Value); // NULL
                        command.Parameters.AddWithValue("@ImagePath2", @"C:\Images\student2.jpg");

                        // Parameters for Courses
                        command.Parameters.AddWithValue("@CourseName1", "Introduction to Programming");
                        command.Parameters.AddWithValue("@Level1", "Beginner");
                        command.Parameters.AddWithValue("@Duration1", "3 months");
                        command.Parameters.AddWithValue("@Fees1", 250);
                        command.Parameters.AddWithValue("@ImagePathCourse1", @"C:\Images\course1.jpg");

                        command.Parameters.AddWithValue("@CourseName2", "Advanced Database Management");
                        command.Parameters.AddWithValue("@Level2", "Advanced");
                        command.Parameters.AddWithValue("@Duration2", "6 months");
                        command.Parameters.AddWithValue("@Fees2", 400);
                        command.Parameters.AddWithValue("@ImagePathCourse2", @"C:\Images\course2.jpg");

                        // Execute the command
                        command.ExecuteNonQuery();
                    }
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
