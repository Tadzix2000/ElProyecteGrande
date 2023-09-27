using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Repositories.NormalRepositories;
using EPGDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EPGDataAccess.Repositories
{
    public class WorkRepository: MainRepository, IWorkRepository
    {
        public WorkRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public List<Work>? GetWorks()
        {
            return Instance.Works.Include(w => w.Author).Include(w => w.OriginalWork).ToList();
        }
        public Work? GetWork(int id)
        {
            return Instance.Works.Include(w => w.Author).Include(w => w.OriginalWork).FirstOrDefault(w => w.Id == id);
        }
        public List<Work>? GetTranslations(Work work)
        {
            return Instance.Works.Where(w => w.OriginalWork == work).Include(w => w.Author).Include(w => w.OriginalWork).ToList();
        }
        public List<Review>? GetReviews(Work work)
        {
            return Instance.Reviews.Include(r => r.Work).Where(w => w.Work == work).ToList();
        }
        public List<Note>? GetNotes(Work work)
        {
            return Instance.Notes.Where(n => n.Work == work).ToList();
        }
        public Work? CreateWork(Work work)
        {
            Instance.Add(work);
            Instance.SaveChanges();
            return work;
        }
        public bool UpdateWork(Work oldWork, Work data)
        {
            oldWork = data;
            Instance.SaveChanges();
            if (GetWork(oldWork.Id) == data) return true;
            return false;
        }
        public void DeleteWorkTranslations(Work work)
        {
            var Translations = Instance.Works.Where(w => w.OriginalWork == work);
            foreach(var translation in Translations)
            {
                DeleteWork(translation);
            }
        }
        public void DeleteWorkReviews(Work work)
        {
            var Reviews = Instance.Reviews.Where(r => r.Work == work);
            foreach (var Review in Reviews)
            {
                Instance.RemoveRange(Instance.Comments.Where(c => c.Review == Review));
            }
            Instance.RemoveRange(Reviews);
        }
        public void DeleteWorkNotes(Work work)
        {
            Instance.RemoveRange(Instance.Notes.Where(n => n.Work == work));
        }
        public bool DeleteWork(Work work)
        {
            if (work != null)
            {
                DeleteWorkNotes(work);
                DeleteWorkReviews(work);
                DeleteWorkTranslations(work);
                Instance.Remove(work);
                Instance.SaveChanges();
                return true;
            }
            return false;
        }
        public void GetSuperiorObjects(Work4Create data, Work work)
        {
            work.OriginalWork = Instance.Works.Include(w => w.Author).Include(w => w.OriginalWork).FirstOrDefault(w => w.Id == data.OriginalWorkId);
            work.Author = Instance.Authors.FirstOrDefault(a => a.Id == data.AuthorId);
        }
    }
}
