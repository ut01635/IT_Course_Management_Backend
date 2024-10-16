using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpPost]
        public ActionResult<ContactUs> CreateContact([FromBody] ContactUs contactUs)
        {
            var createdContact = _contactUsService.CreateContact(contactUs);
            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);
        }

        [HttpGet]
        public ActionResult<List<ContactUs>> GetAllContacts()
        {
            var contacts = _contactUsService.RetrieveAllContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public ActionResult<ContactUs> GetContactById(int id)
        {
            var contact = _contactUsService.RetrieveAllContacts().FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactUs contactUs)
        {
            if (id != contactUs.Id) return BadRequest();
            _contactUsService.UpdateContact(contactUs);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            _contactUsService.RemoveContact(id);
            return NoContent();
        }

        [HttpGet("by-date/{date}")]
        public ActionResult<List<ContactUs>> GetContactsByDate(DateTime date)
        {
            var contacts = _contactUsService.RetrieveContactsByDate(date);
            return Ok(contacts);
        }
    }
}
