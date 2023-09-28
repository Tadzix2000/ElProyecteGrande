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

namespace EPGApplication.Repositories.NormalRepositories
{
    public class AuthorRepository : MainRepository, IAuthorRepository
    {
        public AuthorRepository(DataInstance instance, IMapper mapper) : base(instance, mapper)
        {
        }

        public List<Author>? GetAuthors()
        {
            return Instance.Authors.ToList();
        }
        public Author? GetAuthor(int id)
        {
            return Instance.Authors.Find(id);
        }
        public List<Work>? GetWorksFromAuthor(Author author)
        {
            return Instance.Works.Include(w => w.Author).Where(w => w.Author == author).ToList();
        }
        public Author? CreateAuthor(Author author)
        {
            Instance.Add(author);
            Instance.SaveChanges();
            return author;
        }
        public bool UpdateAuthor(Author oldAuthor, Author4Create data)
        {
            var authorToUpdate = Instance.Authors.FirstOrDefault(a => a.Id == oldAuthor.Id);
            Mapper.Map(data, authorToUpdate);
            Instance.SaveChanges();
            return true;
        }
        public void DeleteAuthorWorks(Author author)
        {
            var works = Instance.Works.Where(w => w.Author == author);
            var fastRepo = new WorkRepository(Instance, Mapper);
            foreach(var work in works) fastRepo.DeleteWork(work);
        }
        public bool DeleteAuthor(Author author)
        {
            if (author != null)
            {
                DeleteAuthorWorks(author);
                Instance.Remove(author);
                Instance.SaveChanges();
                return true;
            }
            return false;
        }

        public void GetSuperiorObjects(Author4Create data, Author author) { }



    }
}
