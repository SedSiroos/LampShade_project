using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.Articles
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticle();
        ArticleQueryModel GetArticle(string slug);
    }
}
