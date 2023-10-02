using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGDataAccess;
using EPGDataAccess.Repositories;
using EPGDomain;
using EPGApplication.DTOs.CreateUpdate;
using Microsoft.EntityFrameworkCore;
using EPGApplication.QueryConfigurations.Objects4Queries;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGApplication.Repositories.NormalRepositories
{
    public class AuthorRepository : MainRepository, IAuthorRepository
    {
        public AuthorRepository(DataInstance instance, IMapper mapper) : base(instance, mapper)
        {
        }

        public async Task<List<Author>?> GetAuthors(AuthorQueryParameters parameters)
        {
            var query = await Instance.Authors.AsQueryable();
            int itemCount = await query.Count();
            var queryManager = new Author4Query(parameters, itemCount, Mapper);
            return await queryManager.GetDesiredData(query);
        }
        public async Task<Author?> GetAuthor(int id)
        {
            return Instance.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Work>?> GetWorksFromAuthor(Author author)
        {
            return Instance.Works.Include(w => w.Author).Where(w => w.Author == author).ToListAsync();
        }
        public async Task<Author?> CreateAuthor(Author author)
        {
            await Instance.AddAsync(author);
            await Instance.SaveChanges();
            return author;
        }
        public async Task<bool> UpdateAuthor(Author oldAuthor, Author4Create data)
        {
            var authorToUpdate = await Instance.Authors.FirstOrDefault(a => a.Id == oldAuthor.Id);
            Mapper.Map(data, authorToUpdate);
            await Instance.SaveChanges();
            return true;
        }
        public async Task<void> DeleteAuthorWorks(Author author)
        {
            var works = Instance.Works.Where(w => w.Author == author);
            var fastRepo = new WorkRepository(Instance, Mapper);
            foreach(var work in works) await fastRepo.DeleteWork(work);
        }
        public async Task<bool> DeleteAuthor(Author author)
        {
            if (author != null)
            {
                await DeleteAuthorWorks(author);
                Instance.RemoveAsync(author);
                Instance.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<void> GetSuperiorObjects(Author4Create data, Author author) { }



    }
}
