Create database CourseManagementDB

CREATE TABLE Student (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    NIC NVARCHAR(20) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    Age INT NOT NULL,
    PhoneNumber NVARCHAR(15),
    Email NVARCHAR(100),
    PassWord NVARCHAR(255) NOT NULL
);

select * from Student

