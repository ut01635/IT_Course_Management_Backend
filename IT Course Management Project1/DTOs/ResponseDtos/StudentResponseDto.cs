namespace IT_Course_Management_Project1.DTOs.ResponseDtos
{
    public class StudentResponseDto
    {
        //public Guid Id { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RegistrationFee { get; set; }
        //public int CourseEnrollId { get; set; }
        public string ImagePath { get; set; }
    }
}
