using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Course_Management_Project1.Entity
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey("Student")]
        public string StudentNIC { get; set; }
        public DateTime Date { get; set; }

        
    }
}
