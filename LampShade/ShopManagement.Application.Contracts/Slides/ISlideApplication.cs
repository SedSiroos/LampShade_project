using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Slides
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<SlideViewMode> GetList();
        EditSlide GetDetails(long id);
    }
}
