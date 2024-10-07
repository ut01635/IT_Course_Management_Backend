namespace IT_Course_Management_Project1.Entity
{
    public class EnrollDetails
    {
        public int Id { get; set; }
        public string Nic { get; set; }
        public int CourseId { get; set; }
        public int? InstallmentId { get; set; }
        public int? FullPaymentId { get; set; }
        public DateTime CourseEnrollDate { get; set; }
        public string Status { get; set; }
    }
}
