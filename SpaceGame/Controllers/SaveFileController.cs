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

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetSaveFileDto>>> AddSaveFile(AddSaveFileDto newSaveFile)
        {
            var response = await _saveFileService.AddSaveFile(newSaveFile);

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetSaveFileDto>>> UpdateSaveFile(UpdateSaveFileDto newSaveFile)
        {
            var response = await _saveFileService.UpdateSaveFile(newSaveFile);

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteSaveFile()
        {
            var response = await _saveFileService.DeleteSaveFile();

            if (response.Success is false)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

    }
}
