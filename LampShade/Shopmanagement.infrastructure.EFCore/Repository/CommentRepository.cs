using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Comments;
using ShopManagement.Domain.CommentAgg;

namespace Shopmanagement.infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly ShopContext _context;

        public CommentRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchViewModel searchModel)
        {
            var query = _context.Comments
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    ProductId = x.ProductId,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
