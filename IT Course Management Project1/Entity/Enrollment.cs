namespace IT_Course_Management_Project1.Entity
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentNic { get; set; }  // FK to Student
        public int CourseId { get; set; }  // FK to Course
        public DateTime EnrollmentDate { get; set; }
        public string PaymentPlan { get; set; }
        public string Status { get; set; }
    }
}
