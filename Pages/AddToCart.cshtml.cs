using Farmacie.Data;
using Farmacie.Models;
using Farmacie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Farmacie.Pages.Products
{
    public class AddToCartModel : PageModel
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly FarmacieContext _context;

        public AddToCartModel(ShoppingCartService shoppingCartService, FarmacieContext context)
        {
            _shoppingCartService = shoppingCartService;
            _context = context;
        }

        [BindProperty]
        public int ProductId { get; set; }

        public IActionResult OnPost()
        {
            var product = _context.Product.FirstOrDefault(p => p.ID == ProductId);
            if (product != null)
            {
                _shoppingCartService.AddToCart(product, 1); // Adăugăm 1 unitate din produsul selectat
            }
            return RedirectToPage("./Index");
        }
    }
}
