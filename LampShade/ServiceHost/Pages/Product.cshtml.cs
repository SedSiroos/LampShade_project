using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comments;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public ProductQueryModel Product;

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment entity,string productSlug)
        {
            var result = _commentApplication.AddComment(entity);
            return RedirectToPage("/Product", new {Id=productSlug});
        }
    }
}
