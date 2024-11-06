using Microsoft.AspNetCore.Mvc;

namespace PT_RazorPage.Pages
{
    public class ImageController : Controller
    {
        [Route("Image/Load")]
        public IActionResult Load(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                var image = System.IO.File.OpenRead(filePath);
                return File(image, "image/jpeg"); // Adjust MIME type based on your image
            }
            return NotFound();
        }
    }
}