using Microsoft.AspNetCore.Mvc;
using EPGDataAccess;
using Microsoft.EntityFrameworkCore;
using EPGApplication.DTOs.Read;
using EPGApplication;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.Services.Services;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGProjectAPI.Controllers
{
    [ApiController]
    [Route("/api/reviews")]
    public class ReviewController : ControllerBase
    {
        public IReviewService service;
        public IReviewRepository repository;
        public IMapper mapper;
        public ReviewController(IReviewService service, IReviewRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.service.GetMapper(this.mapper);
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReviewDTO>> GetAll(
            [FromQuery] string? search,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc
            )
        {
            ReviewQueryParameters parameters = new(search, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var reviews = service.GetReviews(repository, parameters);
            if (reviews is null) return NotFound();
            return Ok(reviews);
        }
        [HttpGet("{id:int}")]
        public ActionResult<ReviewDTO> GetOne(int id)
        {
            var review = service.JustGetReview(id, repository);
            if (review is null) return NotFound();
            return Ok(service.GetReview(review));
        }


        [HttpGet("{id:int}/comments")]
        public ActionResult<IEnumerable<CommentDTO>> GetCommentsFromReview(
            int id,
            [FromQuery] string? search,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc
            )
        {
            var review = service.JustGetReview(id, repository);
            if (review is null) return NotFound();
            CommentQueryParameters parameters = new(search, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var comments = service.GetCommentsFromReview(review, repository, parameters);
            if (comments is null) return NotFound();
            return Ok(comments);
        }


        [HttpPost]
        public IActionResult CreateReview([FromBody] Review4Create review4Create)
        {
            var newReview = service.CreateReview(review4Create, repository);
            if (newReview is null) return BadRequest();
            return CreatedAtAction(nameof(GetOne), new { id = newReview.Id }, newReview);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteReview(int id)
        {
            var review = service.JustGetReview(id, repository);
            if (review is null) return NotFound();
            var DTO = service.DeleteReview(review, repository);
            if (DTO is null) return BadRequest();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateReview(int id, [FromBody] Review4Create UpdateData)
        {
            var review = service.JustGetReview(id, repository);
            if (review is null) return NotFound();
            var ReviewDTO = service.UpdateReview(UpdateData, review, repository);
            if (ReviewDTO is null) return BadRequest();
            return NoContent();
        }
    }
}
