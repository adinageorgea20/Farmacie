using Farmacie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Farmacie.Pages.Products
{
    public class CartModel : PageModel
    {
        private readonly ShoppingCartService _shoppingCartService;

        public CartModel(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal TotalPrice => CartItems.Sum(item => item.Product.Price * item.Quantity);

        public void OnGet()
        {
            CartItems = _shoppingCartService.GetCart();
        }
    }
}
