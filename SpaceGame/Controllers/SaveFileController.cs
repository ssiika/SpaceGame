using Microsoft.AspNetCore.Mvc;
using SpaceGame.Services.SaveFileService;

namespace SpaceGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveFileController : ControllerBase
    {
        private readonly ISaveFileService _saveFileService;

        public SaveFileController(ISaveFileService saveFileService)
        {
            _saveFileService = saveFileService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetSaveFileDto>>> GetSaveFile()
        {
            var response = await _saveFileService.GetSaveFile();

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetSaveFileDto>>> UpdateSaveFile()
        {
            var response = await _saveFileService.UpdateSaveFile();

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetSaveFileDto>>> DeleteSaveFile()
        {
            var response = await _saveFileService.DeleteSaveFile();

            return Ok(response);
        }

    }
}
