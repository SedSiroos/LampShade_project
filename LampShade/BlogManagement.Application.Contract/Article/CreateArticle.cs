using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public IFormFile Picture { get; set; }

        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PublishDate { get; set; }

        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [MaxLength(100, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [MaxLength(150, ErrorMessage = ValidationMessage.MaxLenght)]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }

        [MaxLength(1000, ErrorMessage = ValidationMessage.MaxLenght)]
        public string CanonicalAddress { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }
    }
}
