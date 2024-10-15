namespace IT_Course_Management_Project1.Entity
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Level { get; set; }
<<<<<<< Updated upstream
        
        public int TotalFee { get; set; }

        public string Duration { get; set; }

=======
        public string Duration { get; set; }  // Duration in months
        public decimal Fees { get; set; }
>>>>>>> Stashed changes
        public string ImagePath { get; set; }





    }
}
