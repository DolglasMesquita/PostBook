using Microsoft.AspNetCore.Mvc;
using PostBook.Domain.Dtos;
using PostBook.Domain.Entities;
using PostBook.Domain.Interfaces.Service;

namespace PostBook.Controllers
{
    [Route("api/[controller]")]
   
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDTO user)
        {
            try
            {
                bool emailExist = await _userService.ValidateEmail(user.Email);

                if (emailExist) return BadRequest("Email already exists");

                var result = await _userService.CreateUser(user);
                return result
                    ? Created("User created successfully", "User created successfully")
                    : Conflict("User not created");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /*
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<User> result = await _userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                User result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        */

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser(LoginDTO login)
        {
            try
            {
                bool emailExist = await _userService.ValidateEmail(login.Email);

                if (!emailExist) return NotFound("Email not found");

                LoggedUserDTO result = await _userService.AuthenticateUser(login);

                if (result == null) return BadRequest("Incorrect password");
                

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
      
    }
}
