using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.DTOs.Read;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        public List<Comment>? GetComments(CommentQueryParameters parameters);
        public Comment? GetComment(int? id);
        public List<Comment>? GetResponsesFromComment(Comment comment, CommentQueryParameters parameters);
        public Comment? CreateComment(Comment Data);
        public bool UpdateComment(Comment oldComment, Comment4Create Data);
        public bool DeleteComment(Comment comment);
        public void DeleteCommentResponses(Comment comment);
        public void GetSuperiorObjects(Comment4Create data, Comment comment);
    }
}
