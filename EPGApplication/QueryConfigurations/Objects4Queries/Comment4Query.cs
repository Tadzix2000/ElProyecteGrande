using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGDataAccess.Repositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.QueryConfigurations.Objects4Queries
{
    public class Comment4Query
    {
        public string? search;
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public PaginationMetadata pagination;
        public string? orderBy;
        public bool? desc;
        public Comment4Query(CommentQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
        public List<Comment> GetDesiredData(IQueryable<Comment> query)
        {
            query = DateBorders.latestDate == null || DateBorders.earliestDate == null || DateBorders.earliestDate > DateBorders.latestDate ? query : query.Where(c => c.PublicationDate >= DateBorders.earliestDate && c.PublicationDate <= DateBorders.latestDate);
            query = search == null ? query : query.Where(c => c.Body.Contains(search));
            if (orderBy != null)
            {
                if (orderBy == nameof(Comment.PublicationDate))
                {
                    query = desc == true ? query.OrderByDescending(c => c.PublicationDate) : query.OrderBy(c => c.PublicationDate);
                }
            }
            query = query.Skip((int)((pagination.currentPage - 1) * pagination.pageSize)).Take((int)pagination.pageSize);
            return query.ToList();
        }
    }
}
