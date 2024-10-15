namespace IT_Course_Management_Project1.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Level { get; set; }
        public int Duration { get; set; }  // Duration in months
        public decimal Fees { get; set; }
        public string ImagePath { get; set; }
    }
}
