﻿using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Comments;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;
        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }


        public OperationResult AddComment(AddComment command)
        {
            var operation = new OperationResult();

            var add = new Comment(command.Name,command.Email ,command.Message,command.ProductId);
            _commentRepository.Create(add);
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ConfirmedComment(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Confirmed();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult CanceledComment(long id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMessage.RecordNotFound);

            comment.Canceled();
            _commentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchViewModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}
