using EPGApplication.DTOs.CreateUpdate;
using EPGDomain;
using EPGApplication.DTOs.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Repositories.IRepositories
{
    public interface IReviewRepository
    {
        public List<Review>? GetReviews();
        public Review? GetReview(int id);
        public List<Comment>? GetCommentsFromReview(Review review);
        public Review? CreateReview(Review review);
        public bool UpdateReview(Review oldReview, Review Data);
        public bool DeleteReview(Review review);
        public void DeleteReviewComments(Review review);
        public void GetSuperiorObjects(Review4Create data, Review review);
    }
}
