using System.Collections.Generic;
using BlogManagement.Application.contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Adminstration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategory;
        public IndexModel(IArticleCategoryApplication articleCategory)
        {
            _articleCategory = articleCategory;
        }

        public List<ArticleCategoryViewModel> ArticleCategory;
        public ArticleCategorySearchModel Search;

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategory = _articleCategory.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        public JsonResult OnPost(CreateArticleCategory entity)
        {
            var result = _articleCategory.Create(entity);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var articleCategoryId = _articleCategory.GetDetails(id);
           return Partial("Edit", articleCategoryId);
        }

        public JsonResult OnPostEdit(EditArticleCategory entity)
        {
            var result = _articleCategory.Edit(entity);
            return new JsonResult(result);
        }
    }
}
