using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Farmacie.Data;
using Farmacie.Models;
using System.IO;
using System.Linq;
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

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageUpload { get; set; }

        // Lista de categorii pentru dropdown
        public SelectList CategorySelectList { get; set; }

        public IActionResult OnGet()
        {
            // Populăm lista de categorii pentru dropdown
            CategorySelectList = new SelectList(_context.Category, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Dacă modelul nu este valid, ne întoarcem la pagină pentru corectarea greșelilor
                CategorySelectList = new SelectList(_context.Category, "ID", "Name");
                return Page();
            }

            // Gestionarea încărcării imaginii
            if (ImageUpload != null)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(_imageDirectory))
                {
                    Directory.CreateDirectory(_imageDirectory);
                }

                var fileName = Path.GetRandomFileName() + Path.GetExtension(ImageUpload.FileName);
                var filePath = Path.Combine(_imageDirectory, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageUpload.CopyToAsync(stream);
                    }
                    Product.ImageUrl = "/images/" + fileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la salvarea imaginii: {ex.Message}");
                }
            }

            // Salvăm produsul în baza de date
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
