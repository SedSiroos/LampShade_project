using System;
using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-item";

        private readonly IProductQuery _productQuery;
        public List<ProductQueryModel> ProductQuery;
        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            CartItems = new List<CartItem>();
        }
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName]; 
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculatorTotalItemPrice();

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItem = serializer.Deserialize<List<CartItem>>(value);
            var itemRemove = cartItem.FirstOrDefault(x => x.Id == id);
            cartItem.Remove(itemRemove);
            var options = new CookieOptions {Expires = DateTime.Now.AddDays(2)};
            Response.Cookies.Append(CookieName,serializer.Serialize(cartItem),options);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculatorTotalItemPrice();
            CartItems = _productQuery.CheckInventoryStatus(cartItems);

            if (CartItems.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            return RedirectToPage("./Checkout");
        }
    }
}
