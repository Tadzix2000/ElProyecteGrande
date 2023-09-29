using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Services.IServices;
using EPGApplication.Repositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPGApplication.Repositories.IRepositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Services.Services
{
    public class AuthorService: IAuthorService
    {
        public IMapper Mapper;


        public List<AuthorDTO>? GetAuthors(IAuthorRepository repository, AuthorQueryParameters parameters)
        {
            var authors = repository.GetAuthors(parameters);
            if (authors is null || authors.Count == 0) return null;
            var authorsDTO = new List<AuthorDTO>();
            foreach(var author in authors)
            {
                var authorDTO = Mapper.Map<AuthorDTO>(author);
                //authorDTO.AssignFeatures(this, author);
                authorsDTO.Add(authorDTO);
            }
            return authorsDTO;
        }
        public AuthorDTO? GetAuthor(Author author)
        {
            if (author is null) return null;
            var authorDTO = Mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }
        public Author? JustGetAuthor(int id, IAuthorRepository repository)
        {
            return repository.GetAuthor(id);
        }
        public List<WorkDTO>? GetWorks(Author author, IAuthorRepository repository)
        {
            if (author is null) return null;
            var works = repository.GetWorksFromAuthor(author);
            if (works is null || works.Count() == 0) return null;
            var worksDTO = new List<WorkDTO>();
            foreach (var work in works)
            {
                var workDTO = Mapper.Map<WorkDTO>(work);
                //workDTO.AssignFeatures(workService, work);
                worksDTO.Add(workDTO);
            }
            return worksDTO;
        }
        public AuthorDTO? CreateAuthor(Author4Create author, IAuthorRepository repository)
        {
            var newAuthor = Mapper.Map<Author>(author);
            repository.GetSuperiorObjects(author, newAuthor);
            if (!newAuthor.VerifyNullables()) return null;
            newAuthor = repository.CreateAuthor(newAuthor);
            if (newAuthor is null) return null;
            return Mapper.Map<AuthorDTO>(newAuthor);
        }
        public AuthorDTO? UpdateAuthor(Author4Create data, Author oldAuthor, IAuthorRepository repository)
        {
            var Author = Mapper.Map<Author>(data);
            repository.UpdateAuthor(oldAuthor, data);
            repository.GetSuperiorObjects(data, Author);
            if (!Author.VerifyNullables()) return null;
            if (repository.UpdateAuthor(oldAuthor, data)) return Mapper.Map<AuthorDTO>(oldAuthor);
            return null;
        }
        public AuthorDTO? DeleteAuthor(Author author, IAuthorRepository repository)
        {
            if (repository.DeleteAuthor(author)) return Mapper.Map<AuthorDTO>(author);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
