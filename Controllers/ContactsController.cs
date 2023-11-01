using ContactApp.DTOs;
using ContactApp.Models;
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
            List<ContactDto> contactDtos = new List<ContactDto>();
            var contacts = _contactRepository.GetAll(); 
            if (contacts.Count == 0)
                return BadRequest("No contacts added yet");
            else
            {
                foreach (var contact in contacts)
                {
                    contactDtos.Add(ConvertToDto(contact));
                }
            }
            return Ok(contactDtos);
        }
        [HttpGet("getContactById/{id:int}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact != null)
            {
                var contactDto = ConvertToDto(contact);
                return Ok(contactDto);
            }
            return NotFound("No such contact exists.");
        }

        private Contact ConvertToModel(ContactDto contactDto)
        {
            return new Contact()
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                IsActive = contactDto.IsActive,
                UserId = contactDto.UserId
            };
        }

        private ContactDto ConvertToDto(Contact contact)
        {
            return new ContactDto()
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                IsActive = contact.IsActive,
/*              ContactDetails = contact.ContactDetails != null ? contact.ContactDetails : 0,
*/              UserId = contact.UserId
            };
        }
    }
}

