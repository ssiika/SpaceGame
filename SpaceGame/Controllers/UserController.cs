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
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUserList()
        {
            var response = await _userService.GetUserList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetSingle(int id)
        {
            var response = await _userService.GetSingle(id);

            if (response.Data is null)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(User newUser)
        {
            var response = await _userService.AddUser(newUser);
            return Ok(response);
        }
    }
}
