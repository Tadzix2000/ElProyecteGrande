using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Repositories.IRepositories
{
    public interface INoteRepository
    {
        public async Task<List<Note>?> GetNotes(NoteQueryParameters parameters);
        public async Task<Note?> GetNote(int id);
        public async Task<Note?> CreateNote(Note Data);
        public async Task<bool> UpdateNote(Note oldNote, Note4Create Data);
        public async Task<bool> DeleteNote(Note Note);
        public async Task<void> GetSuperiorObjects(Note4Create data, Note note);
    }
}
