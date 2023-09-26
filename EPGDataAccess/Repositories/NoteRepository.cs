using AutoMapper;
using EPGDataAccess;
using EPGDomain;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using System.Reflection.Metadata.Ecma335;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class NoteRepository : MainRepository, INoteRepository
    {
        public NoteRepository(DataInstance instance) : base(instance) { }
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
        public bool UpdateNote(Note oldNote, Note Data)
        {
            oldNote = Data;
            Instance.SaveChanges();
            if (GetNote(oldNote.Id) != Data) return false;
            return true;
        }
        public bool DeleteNote(Note Note)
        {
            Instance.Remove(Note);
            Instance.SaveChanges();
            if (GetNote(Note.Id) != null) return false;
            return true;
        }
    }
}
