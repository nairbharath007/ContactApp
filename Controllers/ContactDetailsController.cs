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
            var contactDetails = _contactDetailRepository.GetAll();
            if (contactDetails.Count == 0) 
                return BadRequest("No contact details added yet");
            return Ok(contactDetails);
        }
        [HttpGet("getContactDetailById/{id:int}")]
        public IActionResult GetContactDetailById(int id)
        {
            var contactDetail = _contactDetailRepository.GetById(id); 
            if (contactDetail != null)
                return Ok(contactDetail); 
            return NotFound("No such contact detail exists");
        }
    }
}
