using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EPGApplication.QueryConfigurations.QueryParameters
{
    public class AuthorQueryParameters
    {
        public string? name;
        public int? year;
        public string? country;
        public string? search;
        public string? orderBy;
        public bool? desc;
        public int? currentPage;
        public int? pageSize;
        public AuthorQueryParameters(string? name, int? year, string? country, string? search, string? orderBy, bool? desc, int? pageSize, int? currentPage)
        {
            this.name = name;
            this.year = year;
            this.country = country;
            this.search = search;
            this.orderBy = orderBy;
            this.desc = desc;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
        }
    }
}
