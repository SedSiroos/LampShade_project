﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPictures
{
    public class CreateProductPicture
    {
        [Range(1,1000,ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        public List<ProductViewModel> ListProduct { get; set; }  
    }
}
