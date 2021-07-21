﻿using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long,ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}