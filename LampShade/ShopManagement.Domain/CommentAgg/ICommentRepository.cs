using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Comments;

namespace ShopManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchViewModel searchModel);
    }
}
