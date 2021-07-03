using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CollegueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDisCountApplication :IColleagueDisCountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDisCountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }



        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (_colleagueDiscountRepository.Exists(x => 
                x.ProductId == command.ProductId && x.DiscountRate==command.DiscountRate))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var define = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(define);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueId = _colleagueDiscountRepository.Get(command.Id);

            if (colleagueId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);
            if (_colleagueDiscountRepository.Exists(x =>
                x.ProductId == command.Id && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            colleagueId.Edit(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Removed(long id)
        {
            var operation = new OperationResult();
            var colleagueId = _colleagueDiscountRepository.Get(id);
            if (colleagueId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            colleagueId.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restored(long id)
        {
            var operation = new OperationResult();
            var colleagueId = _colleagueDiscountRepository.Get(id);
            if (colleagueId == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            colleagueId.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }
    }
}

