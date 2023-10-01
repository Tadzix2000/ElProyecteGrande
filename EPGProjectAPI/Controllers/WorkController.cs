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
    [Route("/api/works")]
    public class WorkController : ControllerBase
    {
        public IWorkService service;
        public IWorkRepository repository;
        public IMapper mapper;
        public WorkController(IWorkService service, IWorkRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.service.GetMapper(this.mapper);
        }
        [HttpGet]
        public ActionResult<IEnumerable<WorkDTO>> GetAll(
            [FromQuery] string? search,
            [FromQuery] DateTime? earliestRealeaseDate,
            [FromQuery] DateTime? latestReleaseDate,
            [FromQuery] DateTime? earliestPublicationDate,
            [FromQuery] DateTime? latestPublicationDate,
            [FromQuery] DateTime? earliestNoteDate,
            [FromQuery] DateTime? latestNoteDate,
            [FromQuery] double? popularityWeight,
            [FromQuery] string? language,
            [FromQuery] bool? searchTranslations,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc,
            [FromQuery] bool? averageNotesWithTranslations
            )
        {
            var works = service.GetWorks(repository);
            if (works == null) { return NotFound(); }
            return Ok(works);
        }
        [HttpGet("{id:int}")]
        public ActionResult<WorkDTO> GetOne(int id)
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            return Ok(service.GetWork(work));
        }


        [HttpGet("{id:int}/notes")]
        public ActionResult<IEnumerable<NoteDTO>> GetNotesByWork(
            int id, 
            [FromQuery] int? minValue,
            [FromQuery] int? maxValue,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc
            )
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            NoteQueryParameters parameters = new(minValue, maxValue, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var notes = service.GetNotes(work, repository, parameters);
            if (notes is null) return NotFound();
            return Ok(notes);
        }
        [HttpGet("{id:int}/translations")]
        public ActionResult<IEnumerable<WorkDTO>> GetAllWorksTranlsations (int id, [FromQuery] String? search)
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            var translations = service.GetTranslations(work, repository);
            if (translations is null) return NotFound();
            return Ok(translations);
        }
        [HttpGet("{id:int}/reviews")]
        public ActionResult<IEnumerable<ReviewDTO>> GetReviewsByWork(int id, [FromQuery] string? search,
            [FromQuery] DateTime? earliestDate,
            [FromQuery] DateTime? latestDate,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            [FromQuery] string? orderBy,
            [FromQuery] bool? desc)
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            ReviewQueryParameters parameters = new(search, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var reviews = service.GetReviews(work, repository, parameters);
            if (reviews is null) return NotFound();
            return Ok(reviews);
        }


        [HttpPost]
        public IActionResult CreateNewWork([FromBody] Work4Create work4Create)
        {
            var newWork = service.CreateWork(work4Create, repository);
            if (newWork is null) return BadRequest();
            return CreatedAtAction(nameof(GetOne), new { id = newWork.Id }, newWork);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteWork(int id) 
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            var DTO = service.DeleteWork(work, repository);
            if (DTO is null) return BadRequest();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateWork(int id, [FromBody] Work4Create UpdateData)
        {
            var work = service.JustGetWork(id, repository);
            if (work is null) return NotFound();
            var workDTO = service.UpdateWork(UpdateData, work, repository);
            if (workDTO is null) return BadRequest();
            return NoContent();
        }
    }
}
