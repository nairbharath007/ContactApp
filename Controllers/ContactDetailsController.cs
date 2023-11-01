using ContactApp.DTOs;
using ContactApp.Models;
using ContactApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        private readonly IContactDetailRepository _contactDetailRepository;
        public ContactDetailsController(IContactDetailRepository contactDetailRepository)
        {
            _contactDetailRepository = contactDetailRepository;
        }
        [HttpGet("getAllContactDetails")]
        public IActionResult GetAllContactDetails()
        {
            List<ContactDetailDto> contactDetailDtos = new List<ContactDetailDto>();
            var contactDetails = _contactDetailRepository.GetAll();
            if (contactDetails.Count == 0) 
                return BadRequest("No contact details added yet");
            else
            {
                foreach (var contactDetail in contactDetails)
                {
                    contactDetailDtos.Add(ConvertToDto(contactDetail));
                }
            }
            return Ok(contactDetailDtos);
        }
        [HttpGet("getContactDetailById/{id:int}")]
        public IActionResult GetContactDetailById(int id)
        {
            var contactDetail = _contactDetailRepository.GetById(id); 
            if (contactDetail != null)
            {
                var contactDetailDto = ConvertToDto(contactDetail);
                return Ok(contactDetailDto);
            }
            return NotFound("No such contact detail exists."); ;
        }

        private ContactDetail ConvertToModel(ContactDetailDto contactDetailDto)
        {
            return new ContactDetail()
            {
                Type = contactDetailDto.Type,
                NumberOrEMail = contactDetailDto.NumberOrEMail,
                ContactId = contactDetailDto.ContactId
            };
        }

        private ContactDetailDto ConvertToDto(ContactDetail contactDetail)
        {
            return new ContactDetailDto()
            {
                DetailId = contactDetail.DetailId,
                Type = contactDetail.Type,
                NumberOrEMail = contactDetail.NumberOrEMail,
                ContactId = contactDetail.ContactId
            };
        }

    }
}
