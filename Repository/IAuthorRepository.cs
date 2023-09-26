using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPGDomain;
using EPGDataAccess.AddItems;

namespace Repository
{
    public interface IAuthorRepository
    {
        public IQueryable<Author>? GetAuthors();
        public Author? GetAuthor(int id);
        public IQueryable<Work>? GetWorksFromAuthor(int id);
        public void CreateAuthor(Author4Create Data);
        public void UpdateAuthor(Author4Create Data,  int id);
        public void DeleteAuthorWorks(int id);
        public void DeleteAuthor(int id);
    }
}
