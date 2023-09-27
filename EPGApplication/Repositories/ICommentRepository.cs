using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.DTOs.Read;
using EPGApplication.DTOs.CreateUpdate;

namespace EPGApplication.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        public List<Comment>? GetComments();
        public Comment? GetComment(int? id);
        public List<Comment>? GetResponsesFromComment(Comment comment);
        public Comment? CreateComment(Comment Data);
        public bool UpdateComment(Comment oldComment, Comment Data);
        public bool DeleteComment(Comment comment);
        public void DeleteCommentResponses(Comment comment);
        public void GetSuperiorObjects(Comment4Create data, Comment comment);
    }
}
