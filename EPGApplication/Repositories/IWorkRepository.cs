using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Repositories.IRepositories
{
    public interface IWorkRepository
    {
        public List<Work> GetWorks();
        public Work GetWork(int id);
        public List<Work> GetTranslations(Work work);
        public List<Review> GetReviews(Work work);
        public List<Note> GetNotes(Work work);
        public Work CreateWork(Work work);
        public bool UpdateWork(Work oldWork, Work data);
        public void DeleteWorkTranslations(Work work);
        public void DeleteWorkReviews(Work work);
        public void DeleteWorkNotes(Work work);
        public bool DeleteWork(Work work);
    }
}
