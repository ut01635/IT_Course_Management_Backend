using IT_Course_Management_Project1.Entity;

namespace IT_Course_Management_Project1.IServices
{
    public interface IContactUsService
    {
        ContactUs CreateContact(ContactUs contactUs);
        List<ContactUs> RetrieveAllContacts();
        void UpdateContact(ContactUs contactUs);
        void RemoveContact(int id);
        List<ContactUs> RetrieveContactsByDate(DateTime date);
    }

}
