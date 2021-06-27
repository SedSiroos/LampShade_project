using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slides;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication: ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var slide = new Slide(command.Picture, command.PictureTitle, command.PictureAlt,
                command.Heading, command.Title,command.Text ,command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slideId = _slideRepository.Get(command.Id);
            if (slideId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slideId.Edit(command.Picture,command.PictureTitle,command.PictureAlt,command.Heading
                ,command.Heading,command.Title,command.BtnText);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slideId = _slideRepository.Get(id);
            if (slideId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slideId.Remove();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {

            var operation = new OperationResult();
            var slideId = _slideRepository.Get(id);
            if (slideId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            slideId.Restore();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<SlideViewMode> GetList()
        {
            return _slideRepository.GetList();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }
    }
}
