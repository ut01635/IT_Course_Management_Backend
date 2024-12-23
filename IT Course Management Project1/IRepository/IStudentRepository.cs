﻿using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByNicAsync(string nic);
        Task<Student> UpdateStudentAsync(string nic, Student student);
        Task<int> DeleteStudentAsync(string nic);

        Task PasswordUpdateAsync(string nic, string newPassword);
    }
}
