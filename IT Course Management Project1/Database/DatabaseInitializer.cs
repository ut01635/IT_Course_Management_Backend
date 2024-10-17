

using System.Data.SqlClient;

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
                    CourseEnrollId INT NULL
                    
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
                    NIC NVARCHAR(15) NOT NULL,
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
                    SubmitDate DATETIME NOT NULL
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
            INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId)
            SELECT @Nic1, @FullName1, @Email1, @Phone1, @Password1, @RegistrationFee1, @CourseEnrollId1
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE Nic = @Nic1);

            INSERT INTO Students (Nic, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId)
            SELECT @Nic2, @FullName2, @Email2, @Phone2, @Password2, @RegistrationFee2, @CourseEnrollId2
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE Nic = @Nic2);

            -- Insert sample data into Course table if it does not exist
            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName1, @Level1, @Duration1, @Fees1, @ImagePathCourse1
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName1);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName2, @Level2, @Duration2, @Fees2, @ImagePathCourse2
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName2);
             
             -- Insert sample data into Notification table if it does not exist
             INSERT INTO Notification (Message, NIC, Date)
             SELECT @Message1, @NICNO1, @Date1
             WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message1);

            INSERT INTO Notification (Message, NIC, Date)
            SELECT @Message2, @NICNO2, @Date2
            WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message2);

             -- Insert sample data into Enrollment table if it does not exist
            INSERT INTO Enrollment (NIC, CourseId, EnrollmentDate, PaymentPlan, Status)
            SELECT @EnrollmentNIC1, @EnrollmentCourseId1, @EnrollmentDate1, @PaymentPlan1, @Status1
            WHERE NOT EXISTS (SELECT 1 FROM Enrollment WHERE NIC = @EnrollmentNIC1 AND CourseId = @EnrollmentCourseId1);

            INSERT INTO Enrollment (NIC, CourseId, EnrollmentDate, PaymentPlan, Status)
            SELECT @EnrollmentNIC2, @EnrollmentCourseId2, @EnrollmentDate2, @PaymentPlan2, @Status2
            WHERE NOT EXISTS (SELECT 1 FROM Enrollment WHERE NIC = @EnrollmentNIC2 AND CourseId = @EnrollmentCourseId2);

            -- Insert sample data into Payment table if it does not exist
            INSERT INTO Payment (EnrollmentID, NIC, PaymentDate, Amount)
            SELECT @PaymentEnrollmentID1, @PaymentNic1, @PaymentDate1, @PaymentAmount1
            WHERE NOT EXISTS (SELECT 1 FROM Payment WHERE EnrollmentID = @PaymentEnrollmentID1);

             INSERT INTO Payment (EnrollmentID, NIC, PaymentDate, Amount)
             SELECT @PaymentEnrollmentID2, @PaymentNic2, @PaymentDate2, @PaymentAmount2
             WHERE NOT EXISTS (SELECT 1 FROM Payment WHERE EnrollmentID = @PaymentEnrollmentID2);

            -- Insert sample data into Admin table if it does not exist
            INSERT INTO Admin (NIC, Password)
            SELECT @AdminNIC1, @AdminPassword1
            WHERE NOT EXISTS (SELECT 1 FROM Admin WHERE NIC = @AdminNIC1);

            INSERT INTO Admin (NIC, Password)
            SELECT @AdminNIC2, @AdminPassword2
            WHERE NOT EXISTS (SELECT 1 FROM Admin WHERE NIC = @AdminNIC2);

            -- Insert sample data into ContactUs table if it does not exist
            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName1, @ContactEmail1, @ContactMessage1, @ContactDate1
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail1);

            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName2, @ContactEmail2, @ContactMessage2, @ContactDate2
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail2);

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
                        command.Parameters.AddWithValue("@Nic1", "200206601718");
                        command.Parameters.AddWithValue("@FullName1", "Imthath");
                        command.Parameters.AddWithValue("@Email1", "ut01635tic@gmail.com");
                        command.Parameters.AddWithValue("@Phone1", "0768210306");
                        command.Parameters.AddWithValue("@Password1", "123456789"); // Replace with hashed password
                        command.Parameters.AddWithValue("@RegistrationFee1", 1500);
                        command.Parameters.AddWithValue("@CourseEnrollId1", 1);
                      

                        command.Parameters.AddWithValue("@Nic2", "200312345678");
                        command.Parameters.AddWithValue("@FullName2", "Piragash");
                        command.Parameters.AddWithValue("@Email2", "Piragash@gmail.com");
                        command.Parameters.AddWithValue("@Phone2", "0766931772");
                        command.Parameters.AddWithValue("@Password2", "123456789"); // Replace with hashed password
                        command.Parameters.AddWithValue("@RegistrationFee2", 1500);
                        command.Parameters.AddWithValue("@CourseEnrollId2", DBNull.Value); // NULL
                       

                        // Parameters for Courses
                        command.Parameters.AddWithValue("@CourseName1", "Java");
                        command.Parameters.AddWithValue("@Level1", "Beginner");
                        command.Parameters.AddWithValue("@Duration1", "2");
                        command.Parameters.AddWithValue("@Fees1", 10000);
                        command.Parameters.AddWithValue("@ImagePathCourse1", @"C:\Images\course1.jpg");

                        command.Parameters.AddWithValue("@CourseName2", "C#");
                        command.Parameters.AddWithValue("@Level2", "Advanced");
                        command.Parameters.AddWithValue("@Duration2", "6");
                        command.Parameters.AddWithValue("@Fees2", 42000);
                        command.Parameters.AddWithValue("@ImagePathCourse2", @"C:\Images\course2.jpg");

                        // Parameters for first notification
                        command.Parameters.AddWithValue("@Message1", "Welcome to the Institute Management System!");
                        command.Parameters.AddWithValue("@NICNO1", "200206601718");
                        command.Parameters.AddWithValue("@Date1", DateTime.Now);

                        // Parameters for second notification
                        command.Parameters.AddWithValue("@Message2", "Your registration is successful.");
                        command.Parameters.AddWithValue("@NICNO2", "200312345678");
                        command.Parameters.AddWithValue("@Date2", DateTime.Now);

                        // Parameters for Enrollment
                        command.Parameters.AddWithValue("@EnrollmentNIC1", "200206601718");
                        command.Parameters.AddWithValue("@EnrollmentCourseId1", 1);
                        command.Parameters.AddWithValue("@EnrollmentDate1", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentPlan1", "Full Payment");
                        command.Parameters.AddWithValue("@Status1", "Enrolled");

                        command.Parameters.AddWithValue("@EnrollmentNIC2", "200312345678");
                        command.Parameters.AddWithValue("@EnrollmentCourseId2", 2);
                        command.Parameters.AddWithValue("@EnrollmentDate2", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentPlan2", "Installment");
                        command.Parameters.AddWithValue("@Status2", "Pending");

                        // Parameters for Payment
                        command.Parameters.AddWithValue("@PaymentEnrollmentID1", 1);
                        command.Parameters.AddWithValue("@PaymentNic1", "200206601718"); // Sample NIC
                        command.Parameters.AddWithValue("@PaymentDate1", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentAmount1", 10000);

                        command.Parameters.AddWithValue("@PaymentEnrollmentID2", 2);
                        command.Parameters.AddWithValue("@PaymentNic2", "200312345678"); // Sample NIC
                        command.Parameters.AddWithValue("@PaymentDate2", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentAmount2", 7000);

                        // Parameters for Admin
                        command.Parameters.AddWithValue("@AdminNIC1", "200206601718");
                        command.Parameters.AddWithValue("@AdminPassword1", "admin123");

                        command.Parameters.AddWithValue("@AdminNIC2", "200312345678");
                        command.Parameters.AddWithValue("@AdminPassword2", "admin123");

                        // Parameters for ContactUs
                        command.Parameters.AddWithValue("@ContactName1", "Alice Johnson");
                        command.Parameters.AddWithValue("@ContactEmail1", "alice.johnson@example.com");
                        command.Parameters.AddWithValue("@ContactMessage1", "I would like more information about your courses.");
                        command.Parameters.AddWithValue("@ContactDate1", DateTime.Now);

                        command.Parameters.AddWithValue("@ContactName2", "Bob Brown");
                        command.Parameters.AddWithValue("@ContactEmail2", "bob.brown@example.com");
                        command.Parameters.AddWithValue("@ContactMessage2", "How can I enroll in a course?");
                        command.Parameters.AddWithValue("@ContactDate2", DateTime.Now);


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
