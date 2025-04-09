using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using DotNetBlogApp.Data;
using DotNetBlogApp.Models;

namespace DotNetBlogApp.Pages.Blogs
{
    public class CreateModel : PageModel
    {
        private readonly BlogContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(BlogContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public required Blog Blog { get; set; }

        [BindProperty]
        public required IFormFile Image { get; set; }  // New property to store the uploaded image

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if required fields are filled in before attempting to save
            if (!ModelState.IsValid || string.IsNullOrEmpty(Blog.Title) || string.IsNullOrEmpty(Blog.Content))
            {
                ModelState.AddModelError(string.Empty, "Title and Content are required.");
                return Page();
            }

            // Check if an image is uploaded, and if so, save it
            if (Image != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
                var extension = Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName + extension);

                // Save the image file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                // Save the file path in the database
                Blog.ImagePath = "images/" + fileName + extension;
            }
            else
            {
                // If no image is uploaded, use a default image or handle as required
                Blog.ImagePath = "images/default.jpg";  // Adjust this as needed
            }

            // Add the Blog to the database
            _context.Blogs.Add(Blog);
            await _context.SaveChangesAsync();

            // Redirect to the blog list after saving
            return RedirectToPage("/Blogs/Index");
        }
    }
}
