using AutoMapper;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Work4Query(WorkQueryParameters parameters, int totalItemCount, IMapper mapper)
        {
            mapper.Map(parameters, this);
            pagination = new PaginationMetadata(totalItemCount, parameters.currentPage, parameters.pageSize);
        }
    }
}
