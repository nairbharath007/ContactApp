using ContactApp.Models;
using ContactApp.Repository;
using ContactApp1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    [Route("api/[controller]")]
    //default, 'controller' word is not considered.
    //just upto 'Users'
    [ApiController] //annotation, similar to decorator in angular
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        //shortcut: ctor
        public UsersController(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        [HttpGet("GetAll")]
        //http:/localhost/api/Users/GetAll
        //'GetAll' will be attached to the end of url

        //IActionResult is used for returning various Http Responses
        //which can include status codes as well.

        //Ok,BadRequest,NotFound are action results with different status
        //Ok- data found, BadRequest - no data there, NotFound - cant find the data

        public IActionResult Get()
        {
            var users = _userRepository.GetAll();
            if (users.Count == 0)
                return BadRequest("No User Added Yet.");
            return Ok(users);
        }

/*        [HttpGet("GetById")]
*/        [HttpGet("getById/{id:int}")]

        public IActionResult Get(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
                return Ok(user);
            return NotFound("No such user exists.");
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            int newUserId = _userRepository.Add(user);
            if (newUserId!=null)
                return Ok(newUserId);
            return BadRequest("Some issue while recording record.");
        }

        [HttpPut("update")]
        public IActionResult Update(User user)
        {
            var modifiedUser = _userRepository.Update(user);
            if(modifiedUser!=null)
                return Ok(modifiedUser);
            return BadRequest("Some issue while updating record.");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            bool isRemoved = _userRepository.Delete(id);
            if(isRemoved)
                return Ok(id);
            return BadRequest("No user found to delete.");
        }
    }
}
