using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.QueryConfigurations.QueryParameters
{
    public class NoteQueryParameters
    {
        public (int? minValue, int? maxValue) ValueRange;
        public (DateTime? earliestDate, DateTime? latestDate) DateBorders;
        public int? currentPage;
        public int? pageSize;
        public string? orderBy;
        public bool? desc;
        public NoteQueryParameters(int? minValue, int? maxValue, DateTime? earliestDate, DateTime? latestDate, int? currentPage, int? pageSize, string? orderBy, bool? desc) 
        {
            this.ValueRange = (minValue, maxValue);
            this.DateBorders = (earliestDate, latestDate);
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.orderBy = orderBy;
            this.desc = desc;
        }
    }
}
