using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Articles;
using _01_LampShadeQuery.Contracts.CommentQueryModels;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;
        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _blogContext = context;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> LatestArticle()
        {
            return _blogContext.Articles.Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    CategoryId = x.CategoryId,
                }).AsNoTracking().ToList();
        }

        public ArticleQueryModel GetArticle(string slug)
        {
            var article= _blogContext.Articles.Where(x => x.PublishDate <= DateTime.Now)
                .Include(x=>x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Title = x.Title,
                    Picture =x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Description = x.Description,
                    ShortDescription = x.ShortDescription,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);


            if (article != null && !string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordList = article.Keywords.Split(",").ToList();

            var commentsQuery = _commentContext.Comments
                .Where(x => !x.IsCanceled).Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi(),
                }).OrderByDescending(x=>x.Id).ToList();

            foreach (var comment in commentsQuery)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = commentsQuery.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            if (article != null)
            {
                article.Comments = commentsQuery;
            }
            return article;
        }
    }
}
