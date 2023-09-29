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
        public List<AuthorDTO>? GetAuthors(IAuthorRepository repository, AuthorQueryParameters parameters);
        public AuthorDTO? GetAuthor(Author author);
        public Author? JustGetAuthor(int id, IAuthorRepository repository);
        public List<WorkDTO>? GetWorks(Author author, IAuthorRepository repository);
        public AuthorDTO? CreateAuthor(Author4Create author, IAuthorRepository repository);
        public AuthorDTO? UpdateAuthor(Author4Create data, Author oldAuthor, IAuthorRepository repository);
        public AuthorDTO? DeleteAuthor(Author author, IAuthorRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
