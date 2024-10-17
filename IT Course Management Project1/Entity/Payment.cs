using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Course_Management_Project1.Entity
{
    public class Payment
    {
        public int ID { get; set; }

        [ForeignKey("EnrollmentID")]
        public int EnrollmentID { get; set; } // Foreign key to Enrollment

        public string Nic { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Navigation property to related Enrollment
        public Enrollment Enrollment { get; set; }
    }

}
