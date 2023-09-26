using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGDomain;

namespace EPGApplication.Services.Services
{
    public class NoteService : INoteService
    {
        public IMapper Mapper;

        public List<NoteDTO>? GetAllNotes(INoteRepository repository)
        {
            var Notes = repository.GetNotes();
            if (Notes is null || Notes.Count() == 0) return null;
            // var Notes = QueryCenter.SearchNotes(Notes)
            var NotesDTO = new List<NoteDTO>();
            foreach (var Note in Notes)
            {
                NotesDTO.Add(Mapper.Map<NoteDTO>(Note));
            }
            return NotesDTO;
        }
        public NoteDTO? GetNote(Note note)
        {
            if (note is null) return null;
            return Mapper.Map<NoteDTO>(note);
        }
        public Note? JustGetNote(int id, INoteRepository repository)
        {
            return repository.GetNote(id);
        }
        public NoteDTO? CreateNote(Note4Create note, INoteRepository repository)
        {
            var Note = Mapper.Map<Note>(note);
            if (!Note.VerifyNullables()) return null;
            Note = repository.CreateNote(Note);
            return Mapper.Map<NoteDTO>(Note);
        }
        public NoteDTO? UpdateNote(Note4Create note, Note oldNote, INoteRepository repository)
        {
            var Note = Mapper.Map<Note>(note);
            if (!Note.VerifyNullables()) return null;
            if (repository.UpdateNote(oldNote, Note)) return Mapper.Map<NoteDTO>(oldNote);
            return null;
        }
        public NoteDTO? DeleteNote(Note note, INoteRepository repository)
        {
            if (repository.DeleteNote(note)) return Mapper.Map<NoteDTO>(note);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
