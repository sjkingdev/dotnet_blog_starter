using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using DotNetBlogApp.Data;
using DotNetBlogApp.Models;

namespace DotNetBlogApp.Pages.Blogs
{
    public class EditModel : PageModel
    {
        private readonly BlogContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(BlogContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public required Blog Blog { get; set; }

        [BindProperty]
        public required IFormFile Image { get; set; }  // New property to store the uploaded image

        public async Task<IActionResult> OnGetAsync(int id)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            Blog = await _context.Blogs.FindAsync(id);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (Blog == null)
            {
                return NotFound();
            }

            return Page();
        }

        // public async Task<IActionResult> OnPostAsync(int id)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }

        //     var blogToUpdate = await _context.Blogs.FindAsync(id);

        //     if (blogToUpdate == null)
        //     {
        //         return NotFound();
        //     }

        //     blogToUpdate.Title = Blog.Title;
        //     blogToUpdate.Content = Blog.Content;

        //     // Handle the image upload
        //     if (Image != null)
        //     {
        //         var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
        //         var extension = Path.GetExtension(Image.FileName);
        //         var filePath = Path.Combine(_env.WebRootPath, "images", fileName + extension);

        //         using (var stream = new FileStream(filePath, FileMode.Create))
        //         {
        //             await Image.CopyToAsync(stream);
        //         }

        //         blogToUpdate.ImagePath = "images/" + fileName + extension;
        //     }

        //     await _context.SaveChangesAsync();
        //     return RedirectToPage("/Blogs/Index");
        // }
    
  public async Task<IActionResult> OnPostAsync(int id)
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    // Make sure the id is valid
    if (id <= 0)
    {
        return BadRequest();
    }

    var blogToUpdate = await _context.Blogs.FindAsync(id);

    // Ensure the blog is found
    if (blogToUpdate == null)
    {
        return NotFound();
    }

    blogToUpdate.Title = Blog.Title;
    blogToUpdate.Content = Blog.Content;

    // Handle the image upload
    if (Image != null)
    {
        var fileName = Path.GetFileNameWithoutExtension(Image.FileName);
        var extension = Path.GetExtension(Image.FileName);
        var filePath = Path.Combine(_env.WebRootPath, "images", fileName + extension);

        // Save the image to the server
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Image.CopyToAsync(stream);
        }

        blogToUpdate.ImagePath = "images/" + fileName + extension;
    }

    await _context.SaveChangesAsync();
    return RedirectToPage("/Blogs/Index");
}
  
    
    
    }
}
