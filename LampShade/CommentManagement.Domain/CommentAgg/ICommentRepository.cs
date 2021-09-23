using System.Collections.Generic;
using _0_Framework.Domain;
using CommentManagement.Application.Contract.Comments;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchViewModel searchModel);
    
    }
}
