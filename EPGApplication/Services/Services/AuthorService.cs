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


        public async Task<List<AuthorDTO>?> GetAuthors(IAuthorRepository repository, AuthorQueryParameters parameters)
        {
            var authors = await repository.GetAuthors(parameters);
            if (authors is null || authors.Count == 0) return null;
            var authorsDTO = new List<AuthorDTO>();
            foreach(var author in authors)
            {
                var authorDTO = Mapper.Map<AuthorDTO>(author);
                authorsDTO.Add(authorDTO);
            }
            return authorsDTO;
        }
        publi AuthorDTO? GetAuthor(Author author)
        {
            if (author is null) return null;
            var authorDTO = Mapper.Map<AuthorDTO>(author);
            return authorDTO;
        }
        public async Task<Author?> (int id, IAuthorRepository repository)
        {
            return await repository.GetAuthor(id);
        }
        public async Task<List<WorkDTO>?? GetWorks(Author author, IAuthorRepository repository)
        {
            if (author is null) return null;
            var works = await repository.GetWorksFromAuthor(author);
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
        public async Task<AuthorDTO?> CreateAuthor(Author4Create author, IAuthorRepository repository)
        {
            var newAuthor = Mapper.Map<Author>(author);
            await repository.GetSuperiorObjects(author, newAuthor);
            if (!newAuthor.VerifyNullables()) return null;
            newAuthor = await repository.CreateAuthor(newAuthor);
            if (newAuthor is null) return null;
            return Mapper.Map<AuthorDTO>(newAuthor);
        }
        public async Task<AuthorDTO?> UpdateAuthor(Author4Create data, Author oldAuthor, IAuthorRepository repository)
        {
            var Author = Mapper.Map<Author>(data);
            var update = await repository.UpdateAuthor(oldAuthor, data);
            await repository.GetSuperiorObjects(data, Author);
            if (!Author.VerifyNullables()) return null;
            if (update) return Mapper.Map<AuthorDTO>(oldAuthor);
            return null;
        }
        public async Task<AuthorDTO?> DeleteAuthor(Author author, IAuthorRepository repository)
        {
            if (await repository.DeleteAuthor(author)) return Mapper.Map<AuthorDTO>(author);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
