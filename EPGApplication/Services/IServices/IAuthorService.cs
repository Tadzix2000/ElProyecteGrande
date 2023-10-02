using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGApplication.Repositories.IRepositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Services.IServices
{
    public interface IAuthorService
    {
        public async Task<List<AuthorDTO>?> GetAuthors(IAuthorRepository repository, AuthorQueryParameters parameters);
        public AuthorDTO? GetAuthor(Author author);
        public async Task<Author?> JustGetAuthor(int id, IAuthorRepository repository);
        public async Task<List<WorkDTO>?> GetWorks(Author author, IAuthorRepository repository);
        public async Task<AuthorDTO?> CreateAuthor(Author4Create author, IAuthorRepository repository);
        public async Task<AuthorDTO?> UpdateAuthor(Author4Create data, Author oldAuthor, IAuthorRepository repository);
        public async Task<AuthorDTO?> DeleteAuthor(Author author, IAuthorRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
