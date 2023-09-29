using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDataAccess.Repositories
{
    public class PaginationMetadata
    {
        public const int maxPageSize = 50;
        public const int defaultPageSize = 20;
        public int totalItemCount { get; init; }
        public int? totalPageCount { get; init; }
        public int? currentPage { get; init; }
        public int? pageSize { get; init; }

        public PaginationMetadata(int totalItemCount, int? currentPage, int? pageSize)
        {
            this.pageSize = pageSize;
            if (this.pageSize == null || this.pageSize <= 0 || this.pageSize > maxPageSize) this.pageSize = defaultPageSize;
            this.totalItemCount = totalItemCount;
            this.totalPageCount = this.totalItemCount % this.pageSize == 0 ? this.totalItemCount / this.pageSize : (this.totalItemCount / this.pageSize) + 1;
            this.currentPage = currentPage;
            if (this.currentPage == null || this.currentPage <= 0 || this.currentPage > this.totalPageCount) this.currentPage = 1;
        }
    }
}
