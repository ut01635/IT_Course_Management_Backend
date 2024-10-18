namespace IT_Course_Management_Project1.DTOs.RequestDtos
{
    public class StudentRequestDto
    {
        public string Nic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RegistrationFee { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
