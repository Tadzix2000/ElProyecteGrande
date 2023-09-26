using EPGDataAccess.AddItems;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IReviewRepository
    {
        public IQueryable<Review> GetReviews();
        public Review GetReview(int id);
        public IQueryable<Comment> GetCommentsFromReview(int id);
        public void CreateReview(Review4Create review);
        public void UpdateReview(Review4Create review, int id);
        public void DeleteReview(int id);
    }
}
