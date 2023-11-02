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
                /*_context.Entry(contact).Collection(c => c.ContactDetails).Load();*/
                var contactDto = ConvertToDto(contact);
                return Ok(contactDto);
            }

            return NotFound("No such contact exists");
        }
        [HttpPost("add")]
        public IActionResult AddContact(ContactDto contactDto)
        {
            var contact = ConvertToModel(contactDto);
            int newContactId = _contactRepository.Add(contact);
            if (newContactId != null)
            {
                return Ok(newContactId);
            }
            return BadRequest("Some issue while inserting record");
        }
        [HttpPut("update")]
        public IActionResult UpdateContact(ContactDto contactDto)
        {
            var contactModel = ConvertToModel(contactDto);
            var contact = _contactRepository.GetById(contactDto.ContactId);
            if (contact != null)
            {
                var updatedContact = ConvertToModel(contactDto);
                var modifiedContact = _contactRepository.Update(updatedContact, contact);
                return Ok(modifiedContact);
            }
            return BadRequest("Some issue while updating record");
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactRepository.GetById(id);
            if (contact != null)
            {
                _contactRepository.Delete(contact);
                return Ok(id);
            }
            return BadRequest("No contact found to delete");
        }
        private Contact ConvertToModel(ContactDto contactDto)
        {
            return new Contact()
            {
                ContactId = contactDto.ContactId,
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
                UserId = contact.UserId,
                CountDetails = contact.ContactDetails != null ? contact.ContactDetails.Count() : 0
            };
        }
    }
}

