using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts.Slide;
using Shopmanagement.infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlide()
        {
            return _context.Slides
                .Where(x => x.IsRemove == false)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    Heading = x.Heading,
                    Title = x.Title,
                    Text = x.Text,
                    BtnText = x.BtnText,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Link = x.Link
                }).ToList();
        }
    }
}
