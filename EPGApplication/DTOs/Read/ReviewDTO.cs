using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System.ComponentModel.DataAnnotations;

namespace EPGApplication.DTOs.Read
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        //public ServiceUser User { get; set; }
        public Work Work { get; set; }
        [MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(8192)]
        public string Body { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<CommentDTO>? Comments { get; set; }
        //public void AssignFeatures(IReviewService service, Review review, IReviewRepository repository, ICommentService commentService)
        //{
        //    Comments = service.GetCommentsFromReview(review, repository, commentService);
        //}
    }
}
