using EPGApplication.DTOs.Read;
using EPGApplication.DTOs.CreateUpdate;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.Repositories.IRepositories;
using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Services.IServices
{
    public interface ICommentService
    {
        public List<CommentDTO>? GetComments(ICommentRepository repository, CommentQueryParameters parameters);
        public CommentDTO? GetComment(Comment comment);
        public Comment? JustGetComment(int id, ICommentRepository repository);
        public List<CommentDTO>? GetResponsesFromComment(Comment comment, ICommentRepository repository, CommentQueryParameters parameters);
        public CommentDTO? CreateComment(Comment4Create Data, ICommentRepository repository);
        public CommentDTO? UpdateComment(Comment4Create Data, Comment oldComment, ICommentRepository repository);
        public CommentDTO? DeleteComment(Comment comment, ICommentRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
