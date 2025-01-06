using Farmacie.Data;
using Farmacie.Models;
using Farmacie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Farmacie.Pages.Products
{
    public class CartModel : PageModel
    {
        private readonly FarmacieContext _context;
        private readonly ShoppingCartService _cartService;

        public CartModel(FarmacieContext context, ShoppingCartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public List<ShoppingCartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }


        public void OnGet()
        {
            CartItems = _cartService.GetCart();
            TotalPrice = CartItems.Sum(item => item.Product.Price * item.Quantity);
        }

        public async Task<IActionResult> OnPostPlaceOrderAsync()
        {
            var productsInCart = _cartService.GetCart();

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderDetails = productsInCart.Select(item => new OrderDetail
                {
                    ProductID = item.Product.ID,
                    Quantity = item.Quantity // Poți pune 1 dacă nu folosești cantitatea
                }).ToList()
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();


            return RedirectToPage("/OrderDetails/Index", new { id = order.ID });
        }
    }
}

