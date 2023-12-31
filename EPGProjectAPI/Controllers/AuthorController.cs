﻿
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
using EPGApplication.QueryConfigurations.Objects4Queries;
using EPGApplication.QueryConfigurations.QueryParameters;

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
        //[ResponseCache(CacheProfileName = "Any-60")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? country,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] bool? desc
            )
        {
            AuthorQueryParameters parameters = new(earliestDate, latestDate, country, search, orderBy, desc, pageSize, currentPage);
            var authors = service.GetAuthors(repository, parameters);
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
        public ActionResult<IEnumerable<WorkDTO>> GetWorksFromAuthor(
            int id,
            [FromQuery] string? search,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc
            )
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
            var author = service.JustGetAuthor(id, repository);
            if (author is null) return NotFound();
            var authorDTO = service.UpdateAuthor(UpdateData, author, repository);
            if (authorDTO is null) return BadRequest();
            return NoContent();

        }
    }
}
