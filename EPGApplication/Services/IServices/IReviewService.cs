using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGApplication.Repositories.IRepositories;
using EPGDomain;

namespace EPGApplication.Services.IServices
{
    public interface IReviewService
    {
        public List<ReviewDTO>? GetReviews(IReviewRepository repository, ReviewQueryParameters parameters);
        public Review? JustGetReview(int id, IReviewRepository repository);
        public ReviewDTO? GetReview(Review review);
        public List<CommentDTO>? GetCommentsFromReview(Review review, IReviewRepository repository, CommentQueryParameters parameters);
        public ReviewDTO? CreateReview(Review4Create Data, IReviewRepository repository);
        public ReviewDTO? UpdateReview(Review4Create review, Review oldReview, IReviewRepository repository);
        public ReviewDTO? DeleteReview(Review review, IReviewRepository repository);
        public void GetMapper(IMapper mapper);
    }
}
