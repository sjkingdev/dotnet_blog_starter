using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class ContactModel : PageModel
{
    [BindProperty, Required]
    public string? Name { get; set; }

    [BindProperty, Required, EmailAddress]
    public string? Email { get; set; }

    [BindProperty, Required]
    public string? Message { get; set; }

    public void OnGet() {}

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: Save message or send email
        TempData["Success"] = "Thanks for your message!";
        return RedirectToPage("Contact");
    }
}
