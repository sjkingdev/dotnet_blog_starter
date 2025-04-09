using Microsoft.AspNetCore.Mvc.RazorPages;
using DotNetBlogApp.Data;
using DotNetBlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetBlogApp.Pages.Blogs
{
    public class DisplayBlogsModel : PageModel
    {
        private readonly BlogContext _context;

        public DisplayBlogsModel(BlogContext context)
        {
            _context = context;
            Blogs = new List<Blog>();
        }

        public IList<Blog> Blogs { get; set; }

        public async Task OnGetAsync()
        {
            Blogs = await _context.Blogs.ToListAsync();
        }
    }
}
