using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.QueryConfigurations.QueryParameters
{
    public class CommentQueryParameters
    {
        public string? search;
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public int? currentPage;
        public int? pageSize;
        public string? orderBy;
        public bool? desc;
        public CommentQueryParameters(string? search, DateTime? earliestDate, DateTime? latestDate, int? currentPage, int? pageSize, string? orderBy, bool? desc) 
        {
            this.search = search;
            this.DateBorders.earliestDate = earliestDate;
            this.DateBorders.latestDate = latestDate;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.orderBy = orderBy;
            this.desc = desc;
        }
    }
}
