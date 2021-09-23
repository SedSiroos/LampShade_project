using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.Articles;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;
        public ArticleCategoryModel(  IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public ArticleCategoryQueryModel ArticleCategories;
        public List<ArticleCategoryQueryModel> ArticleCategory;
        public List<ArticleQueryModel> LatestArticle;

        public void OnGet(string id)
        {
            ArticleCategories = _articleCategoryQuery.GetArticleCategories(id);
            ArticleCategory = _articleCategoryQuery.GetArticleCategory();
            LatestArticle = _articleQuery.LatestArticle();
        }
    }
}
