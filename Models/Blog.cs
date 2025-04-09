using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlogApp.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Subtitle { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? ImagePath { get; set; }  // New property to store image path

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
