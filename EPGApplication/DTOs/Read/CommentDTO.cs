using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System.ComponentModel.DataAnnotations;

namespace EPGApplication.DTOs.Read
{
    public class CommentDTO
    {
        public int Id { get; set; }
        //public ServiceUser User { get; set; }
        public ReviewDTO? Review { get; set; }
        public CommentDTO? OriginalComment { get; set; }
        public DateTime PublicationDate { get; set; }
        [MaxLength(2048)]
        public string Body { get; set; }
        //public List<CommentDTO>? Responses { get; set; }
        //public void AssignFeatures(ICommentService service, Comment comment, ICommentRepository repository)
        //{
        //    this.Responses = service.GetResponsesFromComment(comment, repository);
        //}
    }
}
