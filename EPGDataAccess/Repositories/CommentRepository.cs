using AutoMapper;
using EPGDomain;
using EPGApplication.DTOs.Read;
using System.Reflection.Metadata.Ecma335;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using EPGDataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class CommentRepository : MainRepository, ICommentRepository
    {
        public CommentRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public List<Comment>? GetComments()
        {
            return Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).ToList();
        }
        public Comment? GetComment(int? id)
        {
            return Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).FirstOrDefault(c => c.Id == id);
        }
        public List<Comment>? GetResponsesFromComment(Comment? comment)
        {
            return Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).Where(c => c.OriginalComment == comment).ToList();
        }
        public Comment? CreateComment(Comment? Data)
        {
            Instance.Comments.Add(Data);
            Instance.SaveChanges();
            return Data;
        }
        public bool UpdateComment(Comment OldComment, Comment4Create Data)
        {
            var commentToUpdate = Instance.Comments.FirstOrDefault(a => a.Id == OldComment.Id);
            Mapper.Map(Data, commentToUpdate);
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
        public void GetSuperiorObjects(Comment4Create data, Comment comment)
        {
            comment.Review = Instance.Reviews.Include(r => r.Work).FirstOrDefault(r => r.Id == data.ReviewId);
            comment.OriginalComment = Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).FirstOrDefault(c => c.Id == data.OriginalCommentId);
        }
    }
}
