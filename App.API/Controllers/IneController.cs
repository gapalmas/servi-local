using App.Core.Interfaces.Core;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class INEController(IServiceFactory serviceFactory) : BaseController(serviceFactory)
    {
        

        [HttpPost("process")]
        public async Task<IActionResult> ProcessINEImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No valid image file provided.");
            }

            var tempFilePath = Path.GetTempFileName();
            try
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var result = await serviceFactory.INEProcessorService.ProcessINEImageAsync(tempFilePath);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing INE image: {ex.Message}");
            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }
        }
    }
}
