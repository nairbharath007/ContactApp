using ContactApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet("getAllContacts")]
        public IActionResult GetAllContacts()
        {
            var contacts = _contactRepository.GetAll(); 
            if (contacts.Count == 0)
                return BadRequest("No contacts added yet"); 
            return Ok(contacts);
        }
        [HttpGet("getContactById/{id:int}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact != null) 
                return Ok(contact);
            return NotFound("No such contact exists");
        }
    }
}

