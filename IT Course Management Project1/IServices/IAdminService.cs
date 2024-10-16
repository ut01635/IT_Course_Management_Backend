using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface IAdminService
    {
        Task<Admin> CreateAdminAsync(Admin admin);
        Task<List<Admin>> RetrieveAllAdminsAsync();
        Task<Admin> RetrieveAdminByNICAsync(string nic);
        Task UpdateAdminAsync(Admin admin);
        Task RemoveAdminAsync(string nic);
    }
}
