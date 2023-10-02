using Microsoft.AspNetCore.Mvc;
using EPGDataAccess;
using EPGApplication.DTOs.Read;
using EPGApplication;
using EPGApplication.Services.Services;
using EPGApplication.DTOs.CreateUpdate;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGProjectAPI.Controllers
{
    [ApiController]
    [Route("/api/comments")]
    public class CommentController : ControllerBase
    {
        public ICommentService service;
        public ICommentRepository repository;
        public IMapper mapper;
        public CommentController(ICommentService service, ICommentRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.service.GetMapper(this.mapper);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc
            )
        {
            CommentQueryParameters parameters = new(search, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var Comments = await service.GetComments(repository, parameters);
            if (Comments is null) return NotFound();
            else return Ok(Comments);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentDTO>> GetOne(int id)
        {
            var Comment = await service.JustGetComment(id, repository);
            if (Comment is null) return NotFound();
            else return service.GetComment(Comment);
        }
        [HttpGet("{id:int}/responses")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllResponses(
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
            var Comment = await service.JustGetComment(id, repository);
            if (Comment is null) return NotFound();
            CommentQueryParameters parameters = new(search, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var Comments = await service.GetResponsesFromComment(Comment, repository, parameters);
            if (Comments is null) return NotFound();
            else return Comments;
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment4Create comment4Create)
        {
            var newComment = await service.CreateComment(comment4Create, repository);
            if (newComment is null) return BadRequest();
            else return CreatedAtAction(nameof(GetOne), new {id = newComment.Id}, newComment);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deletedComment = await service.JustGetComment(id, repository);
            var deletedCommentDTO = await service.DeleteComment(deletedComment, repository);
            if (deletedCommentDTO is null) return NotFound();
            else return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment4Create UpdateData)
        {
            var comment = await service.JustGetComment(id, repository);
            if (comment is null) return NotFound();
            var commentDTO = await service.UpdateComment(UpdateData, comment, repository);
            if (commentDTO is null) return BadRequest();
            return NoContent();
        }
    }
}
