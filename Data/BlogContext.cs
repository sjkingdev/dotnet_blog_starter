using Microsoft.EntityFrameworkCore;
using DotNetBlogApp.Models;

namespace DotNetBlogApp.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}
