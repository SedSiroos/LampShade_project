using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slides;
using ShopManagement.Domain.SlideAgg;

namespace Shopmanagement.infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewMode> GetList()
        {
            return _context.Slides.Select(x => new SlideViewMode
            {
                Id = x.Id,
                Title = x.Title,
                Heading = x.Heading,
                Picture = x.Picture,
                IsRemove = x.IsRemove,
                CreationDate = x.CreationDate.ToString()
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
