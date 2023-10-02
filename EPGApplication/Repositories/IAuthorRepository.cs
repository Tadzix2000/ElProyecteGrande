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
        public async Task<List<Author>?> GetAuthors(AuthorQueryParameters parameters);
        public async Task<Author?> GetAuthor(int id);
        public async Task<List<Work>?> GetWorksFromAuthor(Author author);
        public Author? CreateAuthor(Author author);
        public async Task<bool> UpdateAuthor(Author oldAuthor, Author4Create data);
        public async Task<void> DeleteAuthorWorks(Author author);
        public async Task<bool> DeleteAuthor(Author author);
        public async Task<void> GetSuperiorObjects(Author4Create data, Author author);
    }
}
