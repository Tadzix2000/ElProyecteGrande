using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGDataAccess.Repositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.QueryConfigurations.Objects4Queries
{
    public class Note4Query
    {
        public (int? minValue, int? maxValue) ValueRange;
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public PaginationMetadata pagination;
        public string? orderBy;
        public bool? desc;
        public Note4Query(NoteQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
        public async Task<List<Note>> GetDesiredData(IQueryable<Note> query)
        {
            query = DateBorders.latestDate == null || DateBorders.earliestDate == null || DateBorders.earliestDate > DateBorders.latestDate ? query : query.Where(n => n.NoteDate >= DateBorders.earliestDate && n.NoteDate <= DateBorders.latestDate);
            query = ValueRange.minValue == null || ValueRange.maxValue == null || ValueRange.minValue > ValueRange.maxValue || ValueRange.minValue < 1 || ValueRange.maxValue > 10 ? query : query.Where(n => n.NoteNumber >= ValueRange.minValue && n.NoteNumber <= ValueRange.maxValue);
            if (orderBy != null)
            {
                if (orderBy == nameof(Note.NoteDate))
                {
                    query = desc == true ? query.OrderByDescending(n => n.NoteDate) : query.OrderBy(n => n.NoteDate);
                }
                else if (orderBy == nameof(Note.NoteNumber))
                {
                    query = desc == true ? query.OrderByDescending(n => n.NoteNumber) : query.OrderBy(n => n.NoteNumber);
                }
            }
            query = query.Skip((int)((pagination.currentPage - 1) * pagination.pageSize)).Take((int)pagination.pageSize);
            return query.ToListAsync();
        }
    }
}
