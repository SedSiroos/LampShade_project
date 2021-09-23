using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public const string CookieName= "cart-item";
        public CartCheckout CartCheckout;
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculator;

        public CheckoutModel(ICartCalculatorService cartCalculator, ICartService cartService, IProductQuery productQuery, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper, IOrderApplication orderApplication)
        {
            _cartCalculator = cartCalculator;
            _cartService = cartService;
            _productQuery = productQuery;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            _orderApplication = orderApplication;
        }

        public void OnGet()
        {
            
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculatorTotalItemPrice();

            CartCheckout = _cartCalculator.ComputeCartCheckout(cartItems);
            _cartService.Set(CartCheckout);
        }

        public IActionResult OnPostPay()
        {
             var cart= _cartService.Get();
             var result = _productQuery.CheckInventoryStatus(cart.Items);
             if (result.Any(x => !x.IsInStock))
                 return RedirectToPage("/Cart");
                    
             var orderId = _orderApplication.PlaceOrder(cart);
             var accountUserMobile = _authHelper.CurrentAccountInfo().Mobile;
             var accountUserEmail = _authHelper.CurrentAccountInfo().Email;

             var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(),
                 accountUserMobile, accountUserEmail,"خرید از سیروس شاپ",orderId);

            return Redirect(
                $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            return null;
        }
    }
}
