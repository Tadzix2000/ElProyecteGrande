﻿using AutoMapper;
using EPGDataAccess;
using EPGDomain;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.DTOs.CreateUpdate;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGApplication.QueryConfigurations.Objects4Queries;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class NoteRepository : MainRepository, INoteRepository
    {
        public NoteRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public async Task<List<Note>?> GetNotes(NoteQueryParameters parameters)
        {
            var query = await Instance.Notes.Include(n => n.Work).AsQueryable();
            int itemCount = await query.Count();
            var queryManager = new Note4Query(parameters, itemCount, Mapper);
            return await queryManager.GetDesiredData(query);
        }
        public Note? GetNote(int id)
        {
            return Instance.Notes.Include(n => n.Work).FirstOrDefaultAsync(n => n.Id == id);
        }
        public Note? CreateNote(Note Data)
        {
            await Instance.Notes.AddAsync(Data);
            await Instance.SaveChangesAsync();
            return Data;
        }
        public bool UpdateNote(Note oldNote, Note4Create Data)
        {            
            var noteToUpdate = await Instance.Notes.FirstOrDefaultAsync(n => n.Id == oldNote.Id);
            Mapper.Map(Data, noteToUpdate);
            await Instance.SaveChangesAsync();
            //if (GetNote(oldNote.Id) == oldNote) return false;
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
