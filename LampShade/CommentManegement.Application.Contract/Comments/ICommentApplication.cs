using System.Collections.Generic;
using _0_Framework.Application;


namespace CommentManagement.Application.Contract.Comments
{
    public interface ICommentApplication
    {
        OperationResult AddComment(AddComment command);
        OperationResult ConfirmedComment(long id);
        OperationResult CanceledComment(long id);
        List<CommentViewModel> Search(CommentSearchViewModel searchModel);
    }
}
