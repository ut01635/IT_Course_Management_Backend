using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface IAdminRepository
    {
        Task<Admin> AddAdminAsync(Admin admin);
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin> GetAdminByNICAsync(string nic);
        Task UpdateAdminAsync(Admin admin);
        Task DeleteAdminAsync(string nic);
    }
}
