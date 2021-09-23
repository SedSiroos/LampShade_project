using _01_LampShadeQuery.Contracts.Articles;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticleViewComponent : ViewComponent
    {
        private readonly IArticleQuery _articleQuery;

        public LatestArticleViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.LatestArticle();
            return View(articles);
        }
    }
}
