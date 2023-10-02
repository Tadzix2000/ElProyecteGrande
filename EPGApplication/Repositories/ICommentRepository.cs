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
        public async Task<List<Comment>?> GetComments(CommentQueryParameters parameters);
        public async Task<Comment?> GetComment(int? id);
        public async Task<List<Comment>?> GetResponsesFromComment(Comment comment, CommentQueryParameters parameters);
        public async Task<Comment?> CreateComment(Comment Data);
        public async Task<bool> UpdateComment(Comment oldComment, Comment4Create Data);
        public async Task<bool> DeleteComment(Comment comment);
        public async Task<void> DeleteCommentResponses(Comment comment);
        public async Task<void> GetSuperiorObjects(Comment4Create data, Comment comment);
    }
}
