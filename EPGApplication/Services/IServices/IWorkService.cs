using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGApplication.Repositories.IRepositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Services.IServices
{
    public interface IWorkService
    {
        public List<WorkDTO>? GetWorks(IWorkRepository repository);
        public WorkDTO? GetWork(Work work);
        public Work? JustGetWork(int id, IWorkRepository repository);
        public List<WorkDTO>? GetTranslations(Work work, IWorkRepository repository);
        public List<ReviewDTO>? GetReviews(Work work, IWorkRepository repository, ReviewQueryParameters parameters);
        public List<NoteDTO>? GetNotes(Work work, IWorkRepository repository, NoteQueryParameters parameters);
        public WorkDTO? CreateWork(Work4Create Data, IWorkRepository repository);
        public WorkDTO? UpdateWork(Work4Create Data, Work oldWork, IWorkRepository repository);
        public WorkDTO? DeleteWork(Work work, IWorkRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
