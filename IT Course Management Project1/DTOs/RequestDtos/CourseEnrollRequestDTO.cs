namespace IT_Course_Management_Project1.DTOs.RequestDtos
{
    public class CourseEnrollRequestDTO
    {
        public int Id { get; set; }
        public string Nic { get; set; }
        public int CourseId { get; set; }
        public DateTime CourseEnrollDate { get; set; }
        public string Status { get; set; }
    }
}
