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

            foreach (var contactDetail in contactDetails)
            {
                contactDetailDtos.Add(ConvertToDto(contactDetail));
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

            return NotFound("No such contact detail exists");
        }

        [HttpPost("add")]
        public IActionResult AddContactDetail(ContactDetailDto contactDetailDto)
        {
            var contactDetail = ConvertToModel(contactDetailDto);
            int newDetailId = _contactDetailRepository.Add(contactDetail);

            if (newDetailId != 0)
            {
                return Ok(newDetailId);
            }

            return BadRequest("Some issue occurred while inserting the contact detail.");
        }


        [HttpPut("update")]
        public IActionResult UpdateContactDetail(ContactDetailDto contactDetailDto)
        {
            var contactDetail = _contactDetailRepository.GetById(contactDetailDto.DetailId);

            if (contactDetail != null)
            {
                var updatedDetail = ConvertToModel(contactDetailDto);
                var modifiedDetail = _contactDetailRepository.Update(updatedDetail,contactDetail);
                return Ok(modifiedDetail);
            }

            return BadRequest("Some issue occurred while updating the contact detail");
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult DeleteContactDetail(int id)
        {
            var contactDetail = _contactDetailRepository.GetById(id);

            if (contactDetail != null)
            {
                _contactDetailRepository.Delete(id);
                return Ok(id);
            }

            return BadRequest("No contact detail found to delete");

        }
        private ContactDetail ConvertToModel(ContactDetailDto contactDetailDto)
        {
            return new ContactDetail
            {
                DetailId = contactDetailDto.DetailId,
                Type = contactDetailDto.Type,
                NumberOrEMail = contactDetailDto.NumberOrEMail,
                ContactId = contactDetailDto.ContactId
                
            };
        }

        private ContactDetailDto ConvertToDto(ContactDetail contactDetail)
        {
            return new ContactDetailDto
            {
                DetailId = contactDetail.DetailId,
                Type = contactDetail.Type,
                NumberOrEMail = contactDetail.NumberOrEMail,
                ContactId = contactDetail.ContactId
            };
        }

    }
}
