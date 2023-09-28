using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Repositories.NormalRepositories;
using EPGDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDataAccess.Repositories
{
    public class ReviewRepository : MainRepository, IReviewRepository
    {
        public ReviewRepository(DataInstance instance, IMapper mapper) : base(instance, mapper) { }
        public List<Review>? GetReviews()
        {
            return Instance.Reviews.Include(r => r.Work).ToList();
        }
        public Review? GetReview(int id)
        {
            return Instance.Reviews.Include(r => r.Work).FirstOrDefault(r => r.Id == id);
        }
        public List<Comment>? GetCommentsFromReview(Review review)
        {
            return Instance.Comments.Include(c => c.OriginalComment).Include(c => c.Review).Where(c => c.Review == review).ToList();
        }
        public Review? CreateReview(Review review)
        {
            Instance.Reviews.Add(review);
            Instance.SaveChanges();
            return review;
        }
        public bool UpdateReview(Review oldReview, Review4Create Data)
        {
            var reviewToUpdate = Instance.Reviews.FirstOrDefault(r => r.Id == oldReview.Id);
            Mapper.Map(Data, reviewToUpdate);
            Instance.SaveChanges();
            return true;
        }
        public bool DeleteReview(Review review)
        {
            if (review != null)
            {
                DeleteReviewComments(review);
                Instance.Remove(review); 
                Instance.SaveChanges();
                return true;
            }
            return false;
        }
        public void DeleteReviewComments(Review review)
        {
            Instance.RemoveRange(Instance.Comments.Where(c => c.Review == review));
        }
        public void GetSuperiorObjects(Review4Create data, Review review)
        {
            review.Work = Instance.Works.Include(w => w.Author).Include(w => w.OriginalWork).FirstOrDefault(w => w.Id == data.WorkId);
        }
    }
}
