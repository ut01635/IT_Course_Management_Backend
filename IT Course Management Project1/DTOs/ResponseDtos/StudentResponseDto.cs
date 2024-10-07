namespace IT_Course_Management_Project1.DTOs.ResponseDtos
{
    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string NIC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
