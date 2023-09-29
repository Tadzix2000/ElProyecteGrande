using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPGDomain;
using EPGApplication.DTOs.Read;
using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Repositories.IRepositories
{
    public interface IAuthorRepository
    {
        public List<Author>? GetAuthors(AuthorQueryParameters parameters);
        public Author? GetAuthor(int id);
        public List<Work>? GetWorksFromAuthor(Author author);
        public Author? CreateAuthor(Author author);
        public bool UpdateAuthor(Author oldAuthor, Author4Create data);
        public void DeleteAuthorWorks(Author author);
        public bool DeleteAuthor(Author author);
        public void GetSuperiorObjects(Author4Create data, Author author);
    }
}
