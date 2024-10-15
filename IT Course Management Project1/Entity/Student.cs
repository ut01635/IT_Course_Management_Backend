﻿namespace IT_Course_Management_Project1.Entity
{
    public class Student
    {
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RegistrationFee { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int? CourseEnrollId { get; set; }
        public string ImagePath { get; set; }
    }
}
