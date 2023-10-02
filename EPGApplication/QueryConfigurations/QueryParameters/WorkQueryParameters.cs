using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.QueryConfigurations.QueryParameters
{
    public class WorkQueryParameters
    {
        public string? search;
        public (DateTime? earliestRealeaseDate, DateTime? latestReleaseDate) realeaseRange;
        public (DateTime? earliestPublicationDate, DateTime? latestPublicationDate) publicationRange;
        public (DateTime? earliestNoteDate, DateTime? latestNoteDate) noteRange;
        public double? popularityWeight;
        public string? language;
        public bool? searchTranslations;
        public int? currentPage;
        public int? pageSize;
        public string? orderBy;
        public bool? desc;
        public string? country;
        public WorkQueryParameters(string? search, DateTime? earliestRealeaseDate, DateTime? latestReleaseDate, DateTime? earliestPublicationDate, DateTime? latestPublicationDate, DateTime? earliestNoteDate, DateTime? latestNoteDate, double? popularityWeight, string? language, bool? searchTranslations, int? currentPage, int? pageSize, string? orderBy, bool? desc, string? country)
        {
            this.search = search;
            this.realeaseRange = (earliestRealeaseDate, latestReleaseDate);
            this.publicationRange = (earliestPublicationDate, latestPublicationDate);
            this.noteRange = (earliestNoteDate, latestNoteDate);
            this.popularityWeight = popularityWeight;
            this.language = language;
            this.searchTranslations = searchTranslations;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.orderBy = orderBy;
            this.desc = desc;
            this.country = country;
        }
    }
}
