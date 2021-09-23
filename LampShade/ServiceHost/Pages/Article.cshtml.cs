using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.Articles;
using CommentManagement.Application.Contract.Comments;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategories;
        private readonly ICommentApplication _commentApplication;
        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategories, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategories = articleCategories;
            _commentApplication = commentApplication;
        }

        public ArticleQueryModel Article;
        public List<ArticleCategoryQueryModel> ArticleCategory;
        public List<ArticleQueryModel> LatestArticle;   
        public void OnGet(string id)
        {
           Article = _articleQuery.GetArticle(id);
           ArticleCategory = _articleCategories.GetArticleCategory();
           LatestArticle = _articleQuery.LatestArticle();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var article = _commentApplication.AddComment(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
