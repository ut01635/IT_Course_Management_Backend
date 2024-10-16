using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IRepository
{
    public interface IContactUsRepository
    {
        ContactUs AddContactUsDetails(ContactUs contactUs);
        List<ContactUs> GetAllContacts();
        void EditContactUsDetails(ContactUs contactUs);
        void DeleteContactUsDetails(int id);
        List<ContactUs> GetContactsByDate(DateTime date);
    }

}
