using System.ComponentModel.DataAnnotations;

namespace IT_Course_Management_Project1.DTOs.RequestDtos
{
    public class CourseRequestDTO
    {
        [Required]
        public string CourseName { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public decimal Fees { get; set; }

        public string ImagePath { get; set; }


    }
}
