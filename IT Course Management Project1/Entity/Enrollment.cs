using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IT_Course_Management_Project1.Entity
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public string StudentNIC { get; set; }  // FK to Student

        [ForeignKey("Course")]
        public int CourseId { get; set; }  // FK to Course

        public DateTime EnrollmentDate { get; set; }
        public string PaymentPlan { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
