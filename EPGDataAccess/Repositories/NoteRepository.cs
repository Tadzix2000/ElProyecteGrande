using AutoMapper;
using EPGDataAccess;
using EPGDomain;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class NoteRepository : MainRepository, INoteRepository
    {
        public NoteRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public List<Note>? GetNotes()
        {
            return Instance.Notes.ToList();
        }
        public Note? GetNote(int id)
        {
            return Instance.Notes.Find(id);
        }
        public Note? CreateNote(Note Data)
        {
            Instance.Notes.Add(Data);
            Instance.SaveChanges();
            return Data;
        }
        public bool UpdateNote(Note oldNote, Note4Create Data)
        {            
            var noteToUpdate = Instance.Notes.FirstOrDefault(n => n.Id == oldNote.Id);
            noteToUpdate = Mapper.Map<Note>(Data);
            Instance.SaveChanges();
            if (GetNote(oldNote.Id) == oldNote) return false;
            return true;
        }
        public bool DeleteNote(Note Note)
        {
            Instance.Remove(Note);
            Instance.SaveChanges();
            if (GetNote(Note.Id) != null) return false;
            return true;
        }
        public void GetSuperiorObjects(Note4Create data, Note note)
        {
            note.Work = Instance.Works.Include(w => w.Author).Include(w => w.OriginalWork).FirstOrDefault(w => w.Id == data.WorkId);
        }
    }
}
