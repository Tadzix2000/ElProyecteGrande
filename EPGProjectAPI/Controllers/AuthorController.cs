
using Microsoft.AspNetCore.Mvc;
using EPGDataAccess;
using EPGDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using EPGApplication.DTOs.Read;
using EPGApplication;
using EPGApplication.DTOs.CreateUpdate;
using Microsoft.Extensions.Caching.Memory;
using EPGApplication.Services.IServices;
using EPGApplication.Services.Services;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;

namespace EPGProjectAPI.Controllers
{
    [ApiController]
    [Route("/api/authors")]
    public class AuthorController : ControllerBase
    {
        public IAuthorService service;
        public IAuthorRepository repository;
        public IMapper mapper;
        public AuthorController(IAuthorService service, IAuthorRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.service.GetMapper(this.mapper);
        }
        //public IMemoryCache MemoryCache = new IMemoryCache();
        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-60")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAll([FromQuery] String? search)
        {

            var authors = service.GetAuthors(repository);
            if (authors == null) { return NotFound(); }
            return Ok(authors);

        }
        [HttpGet("{id:int}")]
        public ActionResult<AuthorDTO> GetOne(int id)
        {
            var author = service.JustGetAuthor(id, repository);
            if (author is null) return NotFound();
            return Ok(service.GetAuthor(author));
        }


        [HttpGet("{id:int}/works")]
        public ActionResult<IEnumerable<WorkDTO>> GetWorksFromAuthor(int id, [FromQuery] String? search)
        {
            var author = service.JustGetAuthor(id, repository);
            if (author is null) return NotFound();
            var works = service.GetWorks(author, repository);
            if (works is null) return NotFound();
            return Ok(works);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Author4Create author4Create)
        {
            var newAuthor = service.CreateAuthor(author4Create, repository);
            if (newAuthor is null) return BadRequest();
            return CreatedAtAction(nameof(GetOne), new { id = newAuthor.Id }, newAuthor);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = service.JustGetAuthor(id, repository);
            if (author is null) return NotFound();
            var DTO = service.DeleteAuthor(author, repository);
            if (DTO is null) return BadRequest();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] Author4Create UpdateData)
        {
            throw new NotImplementedException();

        }
    }
}
