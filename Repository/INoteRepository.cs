using EPGDataAccess.AddItems;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface INoteRepository
    {
        public IQueryable<Note> GetNotes();
        public Note GetNote(int id);
        public void CreateNote(Note4Create Data, int id);
        public void UpdateNote(Note4Create Data, int id);
        public void DeleteNote(int id);
    }
}
