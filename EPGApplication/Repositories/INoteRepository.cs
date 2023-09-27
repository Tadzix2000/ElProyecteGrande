using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.DTOs.CreateUpdate;

namespace EPGApplication.Repositories.IRepositories
{
    public interface INoteRepository
    {
        public List<Note>? GetNotes();
        public Note? GetNote(int id);
        public Note? CreateNote(Note Data);
        public bool UpdateNote(Note oldNote, Note4Create Data);
        public bool DeleteNote(Note Note);
        public void GetSuperiorObjects(Note4Create data, Note note);
    }
}
