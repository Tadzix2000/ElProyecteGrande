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
        public ActionResult<List<CommentDTO>> GetAll([FromQuery] String? search)
        {
            var Comments = service.GetComments(repository);
            if (Comments is null) return NotFound();
            else return Ok(Comments);
        }
        [HttpGet("{id:int}")]
        public ActionResult<CommentDTO> GetOne(int id)
        {
            var Comment = service.JustGetComment(id, repository);
            if (Comment is null) return NotFound();
            else return service.GetComment(Comment);
        }
        [HttpGet("{id:int}/responses")]
        public ActionResult<IEnumerable<CommentDTO>> GetAllResponses(int id, [FromQuery] string? search)
        {
            var Comment = service.JustGetComment(id, repository);
            var Comments = service.GetResponsesFromComment(Comment, repository);
            if (Comments is null) return NotFound();
            else return Comments;
        }


        [HttpPost]
        public IActionResult CreateComment([FromBody] Comment4Create comment4Create)
        {
            var newComment = service.CreateComment(comment4Create, repository);
            if (newComment is null) return BadRequest();
            else return CreatedAtAction(nameof(GetOne), new {id = newComment.Id}, newComment);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteComment(int id)
        {
            var deletedComment = service.JustGetComment(id, repository);
            var deletedCommentDTO = service.DeleteComment(deletedComment, repository);
            if (deletedCommentDTO is null) return NotFound();
            else return NoContent();
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment4Create UpdateData)
        {
            var comment = service.JustGetComment(id, repository);
            if (comment is null) return NotFound();
            var commentDTO = service.UpdateComment(UpdateData, comment, repository);
            if (commentDTO is null) return BadRequest();
            return NoContent();
        }
    }
}
