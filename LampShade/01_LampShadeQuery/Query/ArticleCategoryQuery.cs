using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using _01_LampShadeQuery.Contracts.Articles;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategory()
        {
            return _context.ArticleCategories.Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                    ArticleCount = x.Articles.Count,
                }).AsNoTracking().ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategories(string slug)
        {
            var articleCategory = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    Articles = MapArticle(x.Articles),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            
                if (articleCategory != null)
                {
                    if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                    {
                        articleCategory.KeywordList = articleCategory.Keywords.Split(",").ToList();
                    }
                }

            return articleCategory;
        }

        private static List<ArticleQueryModel> MapArticle(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Title = x.Title,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Slug = x.Slug,
                PublishDate = x.PublishDate.ToFarsi(),
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                Keywords = x.Keywords,
            }).ToList();
        }
    }
}
