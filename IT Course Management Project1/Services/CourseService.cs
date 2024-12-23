﻿using IT_Course_Management_Project1.DTOs.RequestDtos;
using IT_Course_Management_Project1.DTOs.ResponseDtos;
using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Repositories;

namespace IT_Course_Management_Project1.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> AddCourseAsync(Course course)
        {

            return await _courseRepository.AddCourseAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _courseRepository.GetCourseByIdAsync(id);
        }

        public async Task<int> UpdateCourseAsync(int id, Course course)
        {
            return await _courseRepository.UpdateCourseAsync(id, course);
        }


        public async Task<int> DeleteCourseAsync(int id)
        {
            return await _courseRepository.DeleteCourseAsync(id);
        }

    }
}
