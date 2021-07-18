﻿using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory:EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get;private set; }
        public string Slug { get;private set; }
        public List<Product> Products { get; private set; }


        protected ProductCategory()
        {
            
        }
        public ProductCategory(string name, string description, string picture, string pictureAlt, string pictureTitle,
            string keyWords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            Products = new List<Product>();
        }

        public void Edit(string name, string description, string picture, string pictureAlt, string pictureTitle,
            string keyWords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
