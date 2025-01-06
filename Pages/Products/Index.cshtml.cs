using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Farmacie.Data;
using Farmacie.Models;
using Farmacie.Services;  // Adaugă acest namespace pentru a accesa ShoppingCartService
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Farmacie.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly FarmacieContext _context;
        private readonly ShoppingCartService _shoppingCartService; // Injectăm serviciul pentru coș

        // Constructorul pentru injectarea dependențelor
        public IndexModel(FarmacieContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService; // Inițializăm serviciul pentru coș
        }

        // Proprietăți pentru a stoca produsele și categoriile
        public IList<Product> Products { get; set; } = new List<Product>();
        public string SelectedCategory { get; set; }
        public List<Category> CategoryList { get; set; } = new List<Category>();

        // Metoda OnGetAsync pentru a prelua produsele și categoriile
        public async Task OnGetAsync(string? selectedCategory)
        {
            CategoryList = await _context.Category.ToListAsync();

            var productsQuery = _context.Product.AsQueryable();

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                productsQuery = productsQuery.Where(p => p.Categories.Any(c => c.Name == selectedCategory));
            }

            Products = await productsQuery
                .Include(p => p.Categories)
                .ToListAsync();
        }

        // Metoda OnPostAddToCart pentru a adăuga produse în coș
        public IActionResult OnPostAddToCart(int productId, int quantity = 1)
        {
            var product = _context.Product.FirstOrDefault(p => p.ID == productId);
            if (product != null)
            {
                _shoppingCartService.AddToCart(product, quantity);
            }
            return RedirectToPage();  // Redirecționăm utilizatorul înapoi la pagina curentă
        }
    }
}
