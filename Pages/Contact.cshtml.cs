using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Farmacie.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Handle form submission here
            // You can send an email, save to the database, etc.

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // For now, just output the form data
            // You can later replace this with actual logic to send an email or save the message.

            // Return to the same page with a success message
            TempData["SuccessMessage"] = "Thank you for reaching out! We will get back to you shortly.";

            // Redirect or reload the page after posting
            return RedirectToPage();
        }
    }
}
