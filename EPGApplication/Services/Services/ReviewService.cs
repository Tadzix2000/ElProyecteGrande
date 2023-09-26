using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.Services.Services
{
    public class ReviewService : IReviewService
    {
        public IMapper Mapper;


        public List<ReviewDTO>? GetReviews(IReviewRepository repository)
        {
            var Reviews = repository.GetReviews();
            if (Reviews is null || Reviews.Count() == 0) return null;
            var ReviewsDTO = new List<ReviewDTO>();
            foreach (var Review in Reviews)
            {
                var ReviewDTO = Mapper.Map<ReviewDTO>(Review);
                //ReviewDTO.AssignFeatures(this, Review);
                ReviewsDTO.Add(ReviewDTO);
            }
            return ReviewsDTO;
        }
        public Review? JustGetReview(int id, IReviewRepository repository)
        {
            return repository.GetReview(id);
        }
        public ReviewDTO? GetReview(Review review)
        {
            if (review is null) return null;
            var ReviewDTO = Mapper.Map<ReviewDTO>(review);
            //ReviewDTO.AssignFeatures(this, review);
            return ReviewDTO;
        }
        public List<CommentDTO>? GetCommentsFromReview(Review review, IReviewRepository repository)
        {
            if (review == null) return null;
            var reviewComments = repository.GetCommentsFromReview(review);
            if (reviewComments is null) return null;
            var comments = new List<CommentDTO>();
            foreach(var Comment in reviewComments)
            {
                var commentDTO = Mapper.Map<CommentDTO>(Comment);
                //commentDTO.AssignFeatures(commentService, Comment);
                comments.Add(commentDTO);
            }
            return comments;
        }
        public ReviewDTO? CreateReview(Review4Create Data, IReviewRepository repository)
        {
            var newReview = Mapper.Map<Review>(Data);
            if (!newReview.VerifyNullables()) return null;
            newReview = repository.CreateReview(newReview);
            if (newReview is null) return null;
            return Mapper.Map<ReviewDTO>(newReview);
        }
        public ReviewDTO? UpdateReview(Review4Create review, Review oldReview, IReviewRepository repository)
        {
            var reviewData = Mapper.Map<Review>(review);
            if (!reviewData.VerifyNullables()) return null;
            if (repository.UpdateReview(oldReview, reviewData)) return Mapper.Map<ReviewDTO>(oldReview);
            return null;
        }
        public ReviewDTO? DeleteReview(Review review, IReviewRepository repository)
        {
            if (repository.DeleteReview(review)) return Mapper.Map<ReviewDTO?>(review);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
