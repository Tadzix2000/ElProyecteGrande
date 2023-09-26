using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System.ComponentModel.DataAnnotations;

namespace EPGApplication.DTOs.Read
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        public ServiceUser Owner { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string ProfileImageFile { get; set; }
        [MaxLength(32)]
        public string Country { get; set; }
        [MaxLength(1024)]
        public string FurtherLinks { get; set; }
        public DateTime CreationDate { get; set; }
        public List<WorkDTO> Works { get; set; }
        //public void AssignFeatures(IAuthorService service, Author author, IAuthorRepository repository, IWorkService workService)
        //{
        //    Works = service.GetWorks(author, repository, workService);
        //}
        
    }
}
