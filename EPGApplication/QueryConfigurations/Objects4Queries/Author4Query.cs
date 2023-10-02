using EPGDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGDomain;
using EPGApplication.QueryConfigurations.QueryParameters;
using AutoMapper;

namespace EPGApplication.QueryConfigurations.Objects4Queries
{
    public class Author4Query
    {
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public string? country;
        public string? search;
        public PaginationMetadata pagination;
        public string? orderBy;
        public bool? desc;

        public Author4Query(AuthorQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
        public async Task<List<Author>> GetDesiredData(IQueryable<Author> query)
        {
            query = DateBorders.latestDate == null || DateBorders.earliestDate == null || DateBorders.earliestDate > DateBorders.latestDate ? query : query.Where(a => a.CreationDate >= DateBorders.earliestDate && a.CreationDate <= DateBorders.latestDate);
            query = country == null ? query : query.Where(a => a.Country == country);
            query = search == null ? query : query.Where(a => a.Name.Contains(search) || a.Description.Contains(search) || a.FurtherLinks.Contains(search));
            if (orderBy != null)
            {
                if (orderBy == nameof(Author.Name))
                {
                    query = desc == true ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name);
                }
                else if (orderBy == nameof(Author.Country))
                {
                    query = desc == true ? query.OrderByDescending(a => a.Country) : query.OrderBy(a => a.Country);
                }
                else if (orderBy == nameof(Author.CreationDate))
                {
                    query = desc == true ? query.OrderByDescending(a => a.CreationDate) : query.OrderBy(a => a.CreationDate);
                }
            }
            query = query.Skip((int)((pagination.currentPage - 1) * pagination.pageSize)).Take((int)pagination.pageSize);
            return query.ToListAsync();
        }
    }
}
