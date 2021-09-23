using System.Collections.Generic;
using CommentManagement.Application.Contract.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;
        //private readonly IProductApplication _productApplication;
         
        public IndexModel(ICommentApplication commentApplication, IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            //_productApplication = productApplication;
        }

        [TempData] public string Message { get; set; }
        public List<CommentViewModel> Comment;
        public CommentSearchViewModel Search;
        public SelectList Products;

        public void OnGet(CommentSearchViewModel seeModel)
        {
            Comment = _commentApplication.Search(seeModel);
            //Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.CanceledComment(id);
            if(result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.ConfirmedComment(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
