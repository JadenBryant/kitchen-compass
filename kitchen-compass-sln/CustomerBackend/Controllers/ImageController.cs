using Microsoft.AspNetCore.Mvc;

namespace CustomerBackend.Controllers;

using Services;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase {
    private readonly GoogleCloudStorageService _storageService;

    public ImageController(GoogleCloudStorageService storageService) {
        _storageService = storageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file) {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var objectName = await _storageService.UploadImageAsync(file);
        var signedUrl = _storageService.GetSignedUrl(objectName);

        return Ok(new { objectName, signedUrl });
    }

    [HttpGet("url/{objectName}")]
    public IActionResult GetSignedUrl(string objectName) {
        var signedUrl = _storageService.GetSignedUrl(objectName);
        return Ok(new { signedUrl });
    }
}