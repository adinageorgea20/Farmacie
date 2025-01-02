using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Farmacie.Data;
using Farmacie.Models;
using System.IO;
using System.Threading.Tasks;

namespace Farmacie.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly FarmacieContext _context;
        private readonly string _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public CreateModel(FarmacieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageUpload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageUpload != null)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(_imageDirectory))
                {
                    Directory.CreateDirectory(_imageDirectory);
                }

                var fileName = Path.GetRandomFileName() + Path.GetExtension(ImageUpload.FileName);
                var filePath = Path.Combine(_imageDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                Product.ImageUrl = "/images/" + fileName;
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
