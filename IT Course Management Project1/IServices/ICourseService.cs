﻿using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;
using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface ICourseService
    {
        Task<Course> AddCourseAsync(Course course);
        Task<IEnumerable<Course>> GetAllCoursesAsync();

    }
}
