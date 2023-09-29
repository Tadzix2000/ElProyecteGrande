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
    public class Review4Query
    {
        public string? search;
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public PaginationMetadata pagination;
        public string? orderBy;
        public bool? desc;
        public Review4Query(ReviewQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
        public List<Review> GetDesiredData(IQueryable<Review> query)
        {
            query = DateBorders.latestDate == null || DateBorders.earliestDate == null || DateBorders.earliestDate > DateBorders.latestDate ? query : query.Where(r => r.ReviewDate >= DateBorders.earliestDate && r.ReviewDate <= DateBorders.latestDate);
            query = search == null ? query : query.Where(r => r.Body.Contains(search) || r.Title.Contains(search));
            if (orderBy != null)
            {
                if (orderBy == nameof(Review.ReviewDate))
                {
                    query = desc == true ? query.OrderByDescending(r => r.ReviewDate) : query.OrderBy(r => r.ReviewDate);
                }
            }
            query = query.Skip((int)((pagination.currentPage - 1) * pagination.pageSize)).Take((int)pagination.pageSize);
            return query.ToList();
        }
    }
}
