namespace IT_Course_Management_Project1.DTOs.ResponseDtos
{
    public class CourseResponseDto
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }

        public string CourseCategory { get; set; }

        public int Duration { get; set; }

        public string CourseFee { get; set; }

        public string Lecturer { get; set; }

        public string Description { get; set; }

        public string CoursePhoto { get; set; }
    }
}
