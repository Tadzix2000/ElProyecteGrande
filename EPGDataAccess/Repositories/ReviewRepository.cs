using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Repositories.NormalRepositories;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDataAccess.Repositories
{
    public class ReviewRepository : MainRepository, IReviewRepository
    {
        public ReviewRepository(DataInstance instance) : base(instance) { }
        public List<Review>? GetReviews()
        {
            return Instance.Reviews.ToList();
        }
        public Review? GetReview(int id)
        {
            return Instance.Reviews.Find(id);
        }
        public List<Comment>? GetCommentsFromReview(Review review)
        {
            return Instance.Comments.Where(c => c.Review == review).ToList();
        }
        public Review? CreateReview(Review review)
        {
            Instance.Reviews.Add(review);
            Instance.SaveChanges();
            return review;
        }
        public bool UpdateReview(Review oldReview, Review Data)
        {
            oldReview = Data;
            Instance.SaveChanges();
            if (GetReview(oldReview.Id) == Data) return true;
            return false;
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
    }
}
