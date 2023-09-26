using AutoMapper;
using EPGDomain;
using EPGApplication.DTOs.Read;
using System.Reflection.Metadata.Ecma335;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using EPGDataAccess;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class CommentRepository : MainRepository, ICommentRepository
    {
        public CommentRepository(DataInstance instance) : base(instance) { }
        public List<Comment>? GetComments()
        {
            return Instance.Comments.ToList();
        }
        public Comment? GetComment(int? id)
        {
            return Instance.Comments.FirstOrDefault(c => c.Id == id);
        }
        public List<Comment>? GetResponsesFromComment(Comment? comment)
        {
            return Instance.Comments.Where(c => c.OriginalComment == comment).ToList();
        }
        public Comment? CreateComment(Comment? Data)
        {
            Instance.Comments.Add(Data);
            Instance.SaveChanges();
            return Data;
        }
        public bool UpdateComment(Comment? OldComment, Comment Data)
        {
            OldComment = Data;
            Instance.SaveChanges();
            return true;
        }
        public void DeleteCommentResponses(Comment comment)
        {
                Instance.Comments.RemoveRange(Instance.Comments.Where(c => c.OriginalComment == comment));
                Instance.SaveChanges();
        }
        public bool DeleteComment(Comment? comment)
        {
            if (comment != null)
            {
                DeleteCommentResponses(comment);
                Instance.Remove(comment);
                Instance.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
