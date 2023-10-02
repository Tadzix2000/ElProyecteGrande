using AutoMapper;
using EPGDomain;
using EPGApplication.DTOs.Read;
using System.Reflection.Metadata.Ecma335;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using EPGDataAccess;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EPGApplication.QueryConfigurations.Objects4Queries;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class CommentRepository : MainRepository, ICommentRepository
    {
        public CommentRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public async Task<List<Comment>?> GetComments(CommentQueryParameters parameters)
        {
            var query = await Instance.Comments.Include(c => c.Review).Include(c => c.OriginalComment).AsQueryable();
            int itemCount = await query.Count();
            var queryManager = new Comment4Query(parameters, itemCount, Mapper);
            return await queryManager.GetDesiredData(query);
        }
        public Task<Comment?> GetComment(int? id)
        {
            return Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).FirstOrDefaultAsync(c => c.Id == id);
        }
        public Task<List<Comment>?> GetResponsesFromComment(Comment? comment, CommentQueryParameters parameters)
        {
            var query = await Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).Where(c => c.OriginalComment == comment).AsQueryable();
            int itemCount = await query.Count();
            var queryManager = new Comment4Query(parameters, itemCount, Mapper);
            return await queryManager.GetDesiredData(query);
        }
        public async Task<Comment?> CreateComment(Comment? Data)
        {
            await Instance.Comments.AddAsync(Data);
            await Instance.SaveChangesAsync();
            return Data;
        }
        public async Task<bool> UpdateComment(Comment OldComment, Comment4Create Data)
        {
            var commentToUpdate = await Instance.Comments.FirstOrDefaultAsync(a => a.Id == OldComment.Id);
            Mapper.Map(Data, commentToUpdate);
            await Instance.SaveChangesAsync();
            return true;
        }
        public async Task<void> DeleteCommentResponses(Comment comment)
        {
                await Instance.Comments.RemoveRangeAsync(Instance.Comments.Where(c => c.OriginalComment == comment));
                await Instance.SaveChangesAsync();
        }
        public async Task<bool> DeleteComment(Comment? comment)
        {
            if (comment != null)
            {
                await DeleteCommentResponses(comment);
                await Instance.RemoveAsync(comment);
                await Instance.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public void GetSuperiorObjects(Comment4Create data, Comment comment)
        {
            comment.Review = await Instance.Reviews.Include(r => r.Work).FirstOrDefaultAsync(r => r.Id == data.ReviewId);
            comment.OriginalComment = await Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).FirstOrDefaultAsync(c => c.Id == data.OriginalCommentId);
        }
    }
}
