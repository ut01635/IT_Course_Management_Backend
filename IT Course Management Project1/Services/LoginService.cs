using IT_Course_Management_Project1.IRepository;

namespace IT_Course_Management_Project1.Services
{
    public class LoginService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IStudentRepository _studentRepository;

        public LoginService(IAdminRepository adminRepository, IStudentRepository studentRepository)
        {
            _adminRepository = adminRepository;
            _studentRepository = studentRepository;
        }



    }
}
