namespace IT_Course_Management_Project1.DTOs.ResponseDtos
{
    public class CourseEnrollResponsDTO
    {
        public int Id { get; set; }
        public string Nic { get; set; }
        public int CourseId { get; set; }
        public string Duration { get; set; }
        public int? InstallmentId { get; set; }
        public int? FullPaymentId { get; set; }
        public DateTime CourseEnrollDate { get; set; }
        public string Status { get; set; }
    }
}
