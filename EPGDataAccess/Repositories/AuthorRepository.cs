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

namespace EPGApplication.Repositories.NormalRepositories
{
    public class AuthorRepository : MainRepository, IAuthorRepository
    {
        public AuthorRepository(DataInstance instance) : base(instance)
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
            return Instance.Works.Where(w => w.Author == author).ToList();
        }
        public Author? CreateAuthor(Author author)
        {
            Instance.Add(author);
            Instance.SaveChanges();
            return author;
        }
        public bool UpdateAuthor(Author oldAuthor, Author data)
        {
            oldAuthor = data;
            Instance.SaveChanges();
            if (GetAuthor(oldAuthor.Id) == data) return true;
            return false;
        }
        public void DeleteAuthorWorks(Author author)
        {
            var works = Instance.Works.Where(w => w.Author == author);
            var fastRepo = new WorkRepository(Instance);
            foreach(var work in works) fastRepo.DeleteWork(work);
        }
        public bool DeleteAuthor(Author author)
        {
            if (author != null)
            {
                DeleteAuthorWorks(author);
                Instance.Remove(author);
                return true;
            }
            return false;
        }



    }
}
