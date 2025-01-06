using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Farmacie.Models;
using System.Collections.Generic;

namespace Farmacie.Services
{
    public class ShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private const string ShoppingCartSessionKey = "ShoppingCart";

        // Obține coșul de cumpărături din sesiune
        public List<ShoppingCartItem> GetCart()
        {
            var cart = _httpContextAccessor.HttpContext.Session.GetString(ShoppingCartSessionKey);
            if (string.IsNullOrEmpty(cart))
            {
                return new List<ShoppingCartItem>();
            }
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cart);
        }

        // Adaugă un produs în coșul de cumpărături
        public void AddToCart(Product product, int quantity)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(item => item.Product.ID == product.ID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new ShoppingCartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            SaveCart(cart);
        }

        // Șterge un produs din coș
        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(item => item.Product.ID == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
            }
            SaveCart(cart);
        }

        // Salvează coșul în sesiune
        private void SaveCart(List<ShoppingCartItem> cart)
        {
            _httpContextAccessor.HttpContext.Session.SetString(ShoppingCartSessionKey, JsonConvert.SerializeObject(cart));
        }
    }

    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
