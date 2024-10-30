using System;
using System.Data.SqlClient;

namespace IT_Course_Management_Project1.Database
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;database=master;"; // Use master to create the new database
        private readonly string _database = "Server=(localdb)\\MSSQLLocalDB;database=InstituteManagement;";

        public DatabaseInitializer()
        {
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
                connection.ChangeDatabase("InstituteManagement");
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
                        NIC NVARCHAR(50) PRIMARY KEY,
                        FullName NVARCHAR(200) NOT NULL,
                        Email NVARCHAR(100) NOT NULL,
                        Phone NVARCHAR(50) NOT NULL,
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
                        NIC NVARCHAR(50) NOT NULL,
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
                        NIC NVARCHAR(50) NOT NULL,
                        FOREIGN KEY (EnrollmentID) REFERENCES Enrollment(ID)
                    );
                END;

                -- Create Notification Table if it does not exist
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Notification' AND xtype = 'U')
                BEGIN
                    CREATE TABLE Notification (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Message NVARCHAR(MAX) NOT NULL,
                        NIC NVARCHAR(50) NOT NULL,
                        Date DATETIME NOT NULL,
                        FOREIGN KEY (NIC) REFERENCES Students(NIC)
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
                        NIC NVARCHAR(50) PRIMARY KEY,
                        Password NVARCHAR(50) NOT NULL
                    );
                END;
            ";

            using (SqlConnection connection = new SqlConnection(_database))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(createTablesQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Tables created (if not exist) successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }

        public void InsertSampleData()
        {
            string insertDataQuery = @"
            BEGIN TRANSACTION;

            -- Insert sample data into Students table if it does not exist
            INSERT INTO Students (NIC, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId)
            SELECT @Nic1, @FullName1, @Email1, @Phone1, @Password1, @RegistrationFee1, @CourseEnrollId1
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE NIC = @Nic1);

            INSERT INTO Students (NIC, FullName, Email, Phone, Password, RegistrationFee, CourseEnrollId)
            SELECT @Nic2, @FullName2, @Email2, @Phone2, @Password2, @RegistrationFee2, @CourseEnrollId2
            WHERE NOT EXISTS (SELECT 1 FROM Students WHERE NIC = @Nic2);

            

          

             -- Insert sample data into Course table if it does not exist
            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName1, @Level1, @Duration1, @Fees1, @ImagePathCourse1
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName1);

             INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
             SELECT @CourseName2, @Level2, @Duration2, @Fees2, @ImagePathCourse2
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName2);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName3, @Level3, @Duration3, @Fees3, @ImagePathCourse3
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName3);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName4, @Level4, @Duration4, @Fees4, @ImagePathCourse4
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName4);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName5, @Level5, @Duration5, @Fees5, @ImagePathCourse5
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName5);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName6, @Level6, @Duration6, @Fees6, @ImagePathCourse6
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName6);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName7, @Level7, @Duration7, @Fees7, @ImagePathCourse7
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName7);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName8, @Level8, @Duration8, @Fees8, @ImagePathCourse8
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName8);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName9, @Level9, @Duration9, @Fees9, @ImagePathCourse9
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName9);

            INSERT INTO Course (CourseName, Level, Duration, Fees, ImagePath)
            SELECT @CourseName10, @Level10, @Duration10, @Fees10, @ImagePathCourse10
            WHERE NOT EXISTS (SELECT 1 FROM Course WHERE CourseName = @CourseName10);




            -- Insert sample data into Notification table if it does not exist
            INSERT INTO Notification (Message, NIC, Date)
            SELECT @Message1, @NICNO1, @Date1
            WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message1);

            INSERT INTO Notification (Message, NIC, Date)
            SELECT @Message2, @NICNO2, @Date2
            WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message2);

            INSERT INTO Notification (Message, NIC, Date)
            SELECT @Message3, @NICNO3, @Date3
            WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message3);

            INSERT INTO Notification (Message, NIC, Date)
            SELECT @Message4, @NICNO4, @Date4
            WHERE NOT EXISTS (SELECT 1 FROM Notification WHERE Message = @Message4);

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

            INSERT INTO Admin (NIC, Password)
            SELECT @AdminNIC3, @AdminPassword3
            WHERE NOT EXISTS (SELECT 1 FROM Admin WHERE NIC = @AdminNIC3);

            -- Insert sample data into ContactUs table if it does not exist
            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName1, @ContactEmail1, @ContactMessage1, @ContactDate1
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail1);

            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName2, @ContactEmail2, @ContactMessage2, @ContactDate2
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail2);

            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName3, @ContactEmail3, @ContactMessage3, @ContactDate3
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail3);

            INSERT INTO ContactUs (Name, Email, Message, SubmitDate)
            SELECT @ContactName4, @ContactEmail4, @ContactMessage4, @ContactDate4
            WHERE NOT EXISTS (SELECT 1 FROM ContactUs WHERE Email = @ContactEmail4);

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
                        command.Parameters.AddWithValue("@Password1", "123456789");
                        command.Parameters.AddWithValue("@RegistrationFee1", 1500);
                        command.Parameters.AddWithValue("@CourseEnrollId1", 1);

                        command.Parameters.AddWithValue("@Nic2", "200431400979");
                        command.Parameters.AddWithValue("@FullName2", "Piragash");
                        command.Parameters.AddWithValue("@Email2", "pppiragash@gmail.com");
                        command.Parameters.AddWithValue("@Phone2", "0766931772");
                        command.Parameters.AddWithValue("@Password2", "piragash");
                        command.Parameters.AddWithValue("@RegistrationFee2", 1500);
                        command.Parameters.AddWithValue("@CourseEnrollId2", 2);



                        // Parameters for Courses
                        command.Parameters.AddWithValue("@CourseName1", "Java");
                        command.Parameters.AddWithValue("@Level1", "Beginner");
                        command.Parameters.AddWithValue("@Duration1", "2 months");
                        command.Parameters.AddWithValue("@Fees1", 10000);
                        command.Parameters.AddWithValue("@ImagePathCourse1", @"C:\Images\course1.jpg");

                        command.Parameters.AddWithValue("@CourseName2", "C#");
                        command.Parameters.AddWithValue("@Level2", "Intermediate");
                        command.Parameters.AddWithValue("@Duration2", "6 months");
                        command.Parameters.AddWithValue("@Fees2", 42000);
                        command.Parameters.AddWithValue("@ImagePathCourse2", @"C:\Images\course2.jpg");

                        command.Parameters.AddWithValue("@CourseName3", "Python");
                        command.Parameters.AddWithValue("@Level3", "Intermediate");
                        command.Parameters.AddWithValue("@Duration3", "6 months");
                        command.Parameters.AddWithValue("@Fees3", 25000);
                        command.Parameters.AddWithValue("@ImagePathCourse3", @"C:\Images\course3.jpg");

                        command.Parameters.AddWithValue("@CourseName4", "JavaScript");
                        command.Parameters.AddWithValue("@Level4", "Beginner");
                        command.Parameters.AddWithValue("@Duration4", "2 months");
                        command.Parameters.AddWithValue("@Fees4", 20000);
                        command.Parameters.AddWithValue("@ImagePathCourse4", @"C:\Images\course4.jpg");

                        command.Parameters.AddWithValue("@CourseName5", "Ruby");
                        command.Parameters.AddWithValue("@Level5", "Beginner");
                        command.Parameters.AddWithValue("@Duration5", "2 months");
                        command.Parameters.AddWithValue("@Fees5", 15000);
                        command.Parameters.AddWithValue("@ImagePathCourse5", @"C:\Images\course5.jpg");

                        command.Parameters.AddWithValue("@CourseName6", "Swift");
                        command.Parameters.AddWithValue("@Level6", "Intermediate");
                        command.Parameters.AddWithValue("@Duration6", "6 months");
                        command.Parameters.AddWithValue("@Fees6", 30000);
                        command.Parameters.AddWithValue("@ImagePathCourse6", @"C:\Images\course6.jpg");

                        command.Parameters.AddWithValue("@CourseName7", "PHP");
                        command.Parameters.AddWithValue("@Level7", "Intermediate");
                        command.Parameters.AddWithValue("@Duration7", "6 months");
                        command.Parameters.AddWithValue("@Fees7", 35000);
                        command.Parameters.AddWithValue("@ImagePathCourse7", @"C:\Images\course7.jpg");

                        command.Parameters.AddWithValue("@CourseName8", "Go");
                        command.Parameters.AddWithValue("@Level8", "Beginner");
                        command.Parameters.AddWithValue("@Duration8", "2 months");
                        command.Parameters.AddWithValue("@Fees8", 20000);
                        command.Parameters.AddWithValue("@ImagePathCourse8", @"C:\Images\course8.jpg");

                        command.Parameters.AddWithValue("@CourseName9", "Kotlin");
                        command.Parameters.AddWithValue("@Level9", "Intermediate");
                        command.Parameters.AddWithValue("@Duration9", "6 months");
                        command.Parameters.AddWithValue("@Fees9", 28000);
                        command.Parameters.AddWithValue("@ImagePathCourse9", @"C:\Images\course9.jpg");

                        command.Parameters.AddWithValue("@CourseName10", "TypeScript");
                        command.Parameters.AddWithValue("@Level10", "Intermediate");
                        command.Parameters.AddWithValue("@Duration10", "6 months");
                        command.Parameters.AddWithValue("@Fees10", 32000);
                        command.Parameters.AddWithValue("@ImagePathCourse10", @"C:\Images\course10.jpg");


                        // Parameters for Notifications
                        command.Parameters.AddWithValue("@Message1", "Welcome to the Institute Management System!");
                        command.Parameters.AddWithValue("@NICNO1", "200206601718");
                        command.Parameters.AddWithValue("@Date1", DateTime.Now);

                        command.Parameters.AddWithValue("@Message2", "Your registration is successful.");
                        command.Parameters.AddWithValue("@NICNO2", "200431400979");
                        command.Parameters.AddWithValue("@Date2", DateTime.Now);

                       
                        command.Parameters.AddWithValue("@Message3", "Course schedule will be sent shortly.");
                        command.Parameters.AddWithValue("@NICNO3", "200206601718");
                        command.Parameters.AddWithValue("@Date3", DateTime.Now);

                        command.Parameters.AddWithValue("@Message4", "Please complete your payment to secure your enrollment.");
                        command.Parameters.AddWithValue("@NICNO4", "200431400979");
                        command.Parameters.AddWithValue("@Date4", DateTime.Now);


                        // Parameters for Enrollment

                        command.Parameters.AddWithValue("@EnrollmentNIC1", "200206601718");
                        command.Parameters.AddWithValue("@EnrollmentCourseId1", 1);
                        command.Parameters.AddWithValue("@EnrollmentDate1", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentPlan1", "Full Payment");
                        command.Parameters.AddWithValue("@Status1", "Enrolled");

                        command.Parameters.AddWithValue("@EnrollmentNIC2", "200431400979");
                        command.Parameters.AddWithValue("@EnrollmentCourseId2", 2);
                        command.Parameters.AddWithValue("@EnrollmentDate2", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentPlan2", "Installment");
                        command.Parameters.AddWithValue("@Status2", "Pending");

                        // Parameters for Payment
                        command.Parameters.AddWithValue("@PaymentEnrollmentID1", 1);
                        command.Parameters.AddWithValue("@PaymentNic1", "200206601718");
                        command.Parameters.AddWithValue("@PaymentDate1", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentAmount1", 10000);

                        command.Parameters.AddWithValue("@PaymentEnrollmentID2", 2);
                        command.Parameters.AddWithValue("@PaymentNic2", "200431400979");
                        command.Parameters.AddWithValue("@PaymentDate2", DateTime.Now);
                        command.Parameters.AddWithValue("@PaymentAmount2", 7000);

                        // Parameters for Admin
                        command.Parameters.AddWithValue("@AdminNIC1", "200206601718");
                        command.Parameters.AddWithValue("@AdminPassword1", "admin123");

                        command.Parameters.AddWithValue("@AdminNIC2", "200431400979");
                        command.Parameters.AddWithValue("@AdminPassword2", "admin123");

                        command.Parameters.AddWithValue("@AdminNIC3", "200417002813");
                        command.Parameters.AddWithValue("@AdminPassword3", "admin123");


                        // Parameters for ContactUs
                        command.Parameters.AddWithValue("@ContactName1", "Alice Johnson");
                        command.Parameters.AddWithValue("@ContactEmail1", "alice.johnson@example.com");
                        command.Parameters.AddWithValue("@ContactMessage1", "I would like more information about your courses.");
                        command.Parameters.AddWithValue("@ContactDate1", DateTime.Now);

                        command.Parameters.AddWithValue("@ContactName2", "Bob Brown");
                        command.Parameters.AddWithValue("@ContactEmail2", "bob.brown@example.com");
                        command.Parameters.AddWithValue("@ContactMessage2", "How can I enroll in a course?");
                        command.Parameters.AddWithValue("@ContactDate2", DateTime.Now);

                        
                        command.Parameters.AddWithValue("@ContactName3", "Charlie Green");
                        command.Parameters.AddWithValue("@ContactEmail3", "charlie.green@example.com");
                        command.Parameters.AddWithValue("@ContactMessage3", "What are your opening hours?");
                        command.Parameters.AddWithValue("@ContactDate3", DateTime.Now);

                        command.Parameters.AddWithValue("@ContactName4", "Diana White");
                        command.Parameters.AddWithValue("@ContactEmail4", "diana.white@example.com");
                        command.Parameters.AddWithValue("@ContactMessage4", "Can I get a brochure?");
                        command.Parameters.AddWithValue("@ContactDate4", DateTime.Now);

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
