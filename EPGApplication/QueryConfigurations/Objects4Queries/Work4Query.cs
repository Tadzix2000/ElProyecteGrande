using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGDomain;
using System.Diagnostics.Metrics;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.Services;

namespace EPGApplication.QueryConfigurations.Objects4Queries
{
    public class Work4Query
    {
        public string? search;
        public (DateTime? earliestRealeaseDate, DateTime? latestReleaseDate) realeaseRange;
        public (DateTime? earliestPublicationDate, DateTime? latestPublicationDate) publicationRange;
        public (DateTime? earliestNoteDate, DateTime? latestNoteDate) noteRange;
        public double? popularityWeight;
        public string? language;
        public bool? searchTranslations;
        public PaginationMetadata pagination;
        public string? orderBy;
        public bool? desc;
        public string? country;

        public Work4Query(WorkQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
        public double findValueForWork(bool chartValue, List<Note> notes, Work work)
        {
            if (chartValue)
            {
                return work.GetForTopChart(notes, this.popularityWeight);
            }
            else return work.GetAverageNote(notes);
        }
        public (List<Work>, List<Note>) GetDesiredData(IQueryable<Work> query, IQueryable<Note> notesQuery)
        {
            //query = publicationRange.latestPublicationDate == null || publicationRange.earliestPublicationDate == null || publicationRange.earliestPublicationDate > publicationRange.latestPublicationDate ? query : query.Where(w => w.CreationDate >= publicationRange.earliestPublicationDate && w.CreationDate <= publicationRange.latestPublicationDate);
            query = country == null ? query : query.Where(w => w.Author.Country == country);
            query = searchTranslations == true ? query : query.Where(w => w.OriginalWork == null);
            query = language == null? query : query.Where(w => w.Language == language);
            query = search == null ? query : query.Where(w => w.Name.Contains(search) || w.Description.Contains(search) || w.OriginalWork.Name.Contains(search) || w.OriginalWork.Description.Contains(search));
            var notesQueryManager = new Note4Query(this);
            var allNotes = notesQueryManager.GetDesiredData(notesQuery);
            if (orderBy != null)
            {
                if (orderBy == nameof(Work.Name))
                {
                    query = desc == true ? query.OrderByDescending(w => w.Name) : query.OrderBy(w => w.Name);
                }
                else if (orderBy == nameof(Work.GetForTopChart))
                {
                    query = desc == true ? query.OrderByDescending(w => w.GetForTopChart(allNotes, this.popularityWeight)) : query.OrderBy(w => w.GetForTopChart(allNotes, this.popularityWeight));
                }
                else if (orderBy == nameof(Work.GetAverageNote))
                {
                    query = desc == true ? query.OrderByDescending(w => w.GetAverageNote(allNotes)) : query.OrderBy(w => w.GetAverageNote(allNotes));
                }
                else if (orderBy == nameof(Work.ReleaseDate))
                {
                    query = desc == true ? query.OrderByDescending(w => w.ReleaseDate) : query.OrderBy(w => w.ReleaseDate);
                }
                else if (orderBy == nameof(Work.ReleaseDate))
                {
                    query = desc == true ? query.OrderByDescending(w => w.PublicationDate) : query.OrderBy(w => w.PublicationDate);
                }
                else if (orderBy == nameof(Work.Author))
                {
                    query = desc == true ? query.OrderByDescending(w => w.Author.Name) : query.OrderBy(w => w.Author.Name);
                }
            }
            query = query.Skip((int)((pagination.currentPage - 1) * pagination.pageSize)).Take((int)pagination.pageSize);
            return (query.ToList(), allNotes);
        }

    }
}
