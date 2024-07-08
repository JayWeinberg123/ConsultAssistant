using Microsoft.AspNetCore.Mvc;
using ConsultAssistant.Data;
using ConsultAssistant.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly AppDbContext _context;

    public UploadController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var filePath = Path.Combine("Uploads", file.FileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileModel = new UploadedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            Path = filePath,
            UploadedAt = DateTime.Now
        };

        _context.UploadedFiles.Add(fileModel);
        await _context.SaveChangesAsync();

        return Ok(new { fileModel.Id });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UploadedFile>>> GetFiles()
    {
        return await _context.UploadedFiles.ToListAsync();
    }
}
