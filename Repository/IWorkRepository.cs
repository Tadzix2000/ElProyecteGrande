using EPGDataAccess.AddItems;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IWorkRepository
    {
        public IQueryable<Work> GetWorks();
        public Work GetWork(int id);
        public IQueryable<Work> GetTranslations(int id);
        public IQueryable<Review> GetReviews(int id);
        public IQueryable<Note> GetNotes(int id);
        public void CreateWork(Work4Create Data);
        public void UpdateWork(Work4Create Data, int id);
        public void DeleteWork(int id);
    }
}
