using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System.ComponentModel.DataAnnotations;

namespace EPGApplication.DTOs.Read
{
    public class WorkDTO
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string Language { get; set; }
        [MaxLength(32)]
        public string CoverFile { get; set; }  //png/jpg file
        [MaxLength(32)]
        public string WorkFile { get; set; } //pdf file
        public DateTime ReleaseDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public WorkDTO? OriginalWork { get; set; }
        public double? AverageNote { get; set; }
        public AuthorDTO Author { get; set; }
        //public List<NoteDTO>? Notes { get; set; }
        //public List<ReviewDTO>? Reviews { get; set; }
        //public List<WorkDTO>? Translations { get; set; }
        //public void AssignFeatures(IWorkService service, Work work, IWorkRepository repository, IReviewRepository reviewRepository, IReviewService reviewService)
        //{
        //    Translations = service.GetTranslations(work, repository);
        //    Notes = service.GetNotes(work, repository);
        //    Reviews = service.GetReviews(work, repository, reviewService, reviewRepository);
        //}
    }
}
