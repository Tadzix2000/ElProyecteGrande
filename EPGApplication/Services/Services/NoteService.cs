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
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Services.Services
{
    public class NoteService : INoteService
    {
        public IMapper Mapper;

        public async Task<List<NoteDTO>?> GetAllNotes(INoteRepository repository, NoteQueryParameters parameters)
        {
            var Notes = await repository.GetNotes(parameters);
            if (Notes is null || Notes.Count() == 0) return null;
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
        public async Task<Note?> JustGetNote(int id, INoteRepository repository)
        {
            return await repository.GetNote(id);
        }
        public async Task<NoteDTO?> CreateNote(Note4Create note, INoteRepository repository)
        {
            var Note = Mapper.Map<Note>(note);
            await repository.GetSuperiorObjects(note, Note);
            if (!Note.VerifyNullables()) return null;
            Note = await repository.CreateNote(Note);
            return Mapper.Map<NoteDTO>(Note);
        }
        public async Task<NoteDTO?> UpdateNote(Note4Create note, Note oldNote, INoteRepository repository)
        {
            var Note = Mapper.Map<Note>(note);
            await repository.GetSuperiorObjects(note, Note);
            if (!Note.VerifyNullables()) return null;
            if (await repository.UpdateNote(oldNote, note)) return Mapper.Map<NoteDTO>(oldNote);
            return null;
        }
        public NoteDTO? DeleteNote(Note note, INoteRepository repository)
        {
            if (await repository.DeleteNote(note)) return Mapper.Map<NoteDTO>(note);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
