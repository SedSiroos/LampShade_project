using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.Slides
{
    public class CreateSlide
    {
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Heading { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Link { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string BtnText { get; set; }
    }
}
