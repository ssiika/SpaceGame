using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SpaceGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUserList()
        {
            var response = await _userService.GetUserList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingle(int id)
        {
            var response = await _userService.GetSingle(id);


            if (response.Success is false)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var response = await _userService.AddUser(newUser);

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);


            if (response.Success is false)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }
    }
}
