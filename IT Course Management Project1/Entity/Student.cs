namespace IT_Course_Management_Project1.Entity
{
    public class Student
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
    }
}
