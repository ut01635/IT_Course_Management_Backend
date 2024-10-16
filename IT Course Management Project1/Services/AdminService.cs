using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;

namespace IT_Course_Management_Project1.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            try
            {
                return await _adminRepository.AddAdminAsync(admin);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error creating admin", ex);
            }
        }

        public async Task<List<Admin>> RetrieveAllAdminsAsync()
        {
            try
            {
                return await _adminRepository.GetAllAdminsAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving admins", ex);
            }
        }

        public async Task<Admin> RetrieveAdminByNICAsync(string nic)
        {
            try
            {
                return await _adminRepository.GetAdminByNICAsync(nic);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error retrieving admin", ex);
            }
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            try
            {
                await _adminRepository.UpdateAdminAsync(admin);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error updating admin", ex);
            }
        }

        public async Task RemoveAdminAsync(string nic)
        {
            try
            {
                await _adminRepository.DeleteAdminAsync(nic);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error deleting admin", ex);
            }
        }
    }
}
