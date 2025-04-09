using Microsoft.AspNetCore.Mvc.RazorPages;
using DotNetBlogApp.Data;  // BlogContext namespace
using DotNetBlogApp.Models;  // Blog model namespace
using Microsoft.EntityFrameworkCore;  // For async database calls

namespace DotNetBlogApp.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly BlogContext _context;

        // Initialize Blogs to an empty list to avoid nullability issues
        public IndexModel(BlogContext context)
        {
            _context = context;
            Blogs = new List<Blog>(); // Ensures Blogs is never null
        }

        // Non-nullable property to hold the list of blogs
        public IList<Blog> Blogs { get; set; }

        // Async method to fetch the list of blogs from the database
        public async Task OnGetAsync()
        {
            // Fetching blogs from the database asynchronously
            Blogs = await _context.Blogs.ToListAsync();
        }
    }
}
