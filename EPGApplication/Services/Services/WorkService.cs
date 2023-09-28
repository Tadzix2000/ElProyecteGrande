using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Services.Services
{
    public class WorkService : IWorkService
    {
        public IMapper Mapper;

        public List<WorkDTO>? GetWorks(IWorkRepository repository)
        {
            var works = repository.GetWorks();
            if (works is null || works.Count() == 0) return null;
            var worksDTO = new List<WorkDTO>();
            foreach (var work in works)
            {
                var workDTO = Mapper.Map<WorkDTO>(work);
                //workDTO.AssignFeatures(this, work);
                worksDTO.Add(workDTO);
            }
            return worksDTO;
        }
        public WorkDTO? GetWork(Work work)
        {
            if (work is null) return null;
            var workDTO = Mapper.Map<WorkDTO>(work);
            //workDTO.AssignFeatures(this, work, repository);
            return workDTO;
        }
        public Work? JustGetWork(int id, IWorkRepository repository)
        {
            return repository.GetWork(id);
        }
        public List<WorkDTO>? GetTranslations(Work work, IWorkRepository repository)
        {
            if (work is null) return null;
            var translations = repository.GetTranslations(work);
            if (translations is null || translations.Count() == 0) return null;
            var translationsDTO = new List<WorkDTO>();
            foreach(var translation in translations)
            {
                var translationDTO = Mapper.Map<WorkDTO>(translation);
                //translationDTO.AssignFeatures(this, translation, repository);
                translationsDTO.Add(translationDTO);
            }
            return translationsDTO;
        }
        public List<ReviewDTO>? GetReviews(Work work, IWorkRepository repository)
        {
            if (work is null) return null;
            var reviews = repository.GetReviews(work);
            if (reviews is null || reviews.Count() == 0) return null;
            var reviewsDTO = new List<ReviewDTO>();
            foreach(var review in reviews)
            {
                var reviewDTO = Mapper.Map<ReviewDTO>(review);
               // reviewDTO.AssignFeatures(reviewService, review, reviewRepository);
                reviewsDTO.Add(reviewDTO);
            }
            return reviewsDTO;
        }
        public List<NoteDTO>? GetNotes(Work work, IWorkRepository repository)
        {
            if (work is null) return null;
            var notes = repository.GetNotes(work);
            if (notes is null || notes.Count() == 0) return null;
            var notesDTO = new List<NoteDTO>();
            foreach (var note in notes)
            {
                var noteDTO = Mapper.Map<NoteDTO>(note);
                notesDTO.Add(noteDTO);
            }
            return notesDTO;
        }
        public WorkDTO? CreateWork(Work4Create Data, IWorkRepository repository)
        {
            var newWork = Mapper.Map<Work>(Data);
            repository.GetSuperiorObjects(Data, newWork);
            if (!newWork.VerifyNullables()) return null;
            newWork = repository.CreateWork(newWork);
            if (newWork is null) return null;
            return Mapper.Map<WorkDTO>(newWork);
        }
        public WorkDTO? UpdateWork(Work4Create Data, Work oldWork, IWorkRepository repository)
        {
            var Work = Mapper.Map<Work>(Data);
            repository.UpdateWork(oldWork, Data);
            repository.GetSuperiorObjects(Data, Work);
            if (!Work.VerifyNullables()) return null;
            if (repository.UpdateWork(oldWork, Data)) return Mapper.Map<WorkDTO>(oldWork);
            return null;
        }
        public WorkDTO? DeleteWork(Work work, IWorkRepository repository)
        {
            if (repository.DeleteWork(work)) return Mapper.Map<WorkDTO>(work); 
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
