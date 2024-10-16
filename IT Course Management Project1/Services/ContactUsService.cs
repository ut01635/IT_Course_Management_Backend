using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;

namespace IT_Course_Management_Project1.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public ContactUs CreateContact(ContactUs contactUs)
        {
            return _contactUsRepository.AddContactUsDetails(contactUs);
        }

        public List<ContactUs> RetrieveAllContacts()
        {
            return _contactUsRepository.GetAllContacts();
        }

        public void UpdateContact(ContactUs contactUs)
        {
            _contactUsRepository.EditContactUsDetails(contactUs);
        }

        public void RemoveContact(int id)
        {
            _contactUsRepository.DeleteContactUsDetails(id);
        }

        public List<ContactUs> RetrieveContactsByDate(DateTime date)
        {
            return _contactUsRepository.GetContactsByDate(date);
        }
    }

}
