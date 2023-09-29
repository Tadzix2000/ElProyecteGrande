using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGDomain;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Repositories.IRepositories;
using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Services.IServices
{
    public interface INoteService
    {
        List<NoteDTO>? GetAllNotes(INoteRepository repository, NoteQueryParameters parameters);
        Note? JustGetNote(int id, INoteRepository repository);
        NoteDTO? GetNote(Note note);
        NoteDTO? CreateNote(Note4Create note, INoteRepository repository);
        NoteDTO? UpdateNote(Note4Create note, Note oldNote, INoteRepository repository);
        NoteDTO? DeleteNote(Note note, INoteRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
