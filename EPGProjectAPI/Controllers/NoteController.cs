﻿using Microsoft.AspNetCore.Mvc;
using EPGApplication.DTOs.Read;
using EPGApplication.DTOs.CreateUpdate;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using EPGApplication.Services.Services;
using EPGDataAccess;
using AutoMapper;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.Services.IServices;
using EPGApplication.QueryConfigurations.QueryParameters;

namespace EPGProjectAPI.Controllers
{
    [ApiController]
    [Route("/api/notes")]
    public class NoteController:ControllerBase
    {
        public INoteService service;
        public INoteRepository repository;
        public IMapper mapper;
        public NoteController(INoteService service, INoteRepository repository, IMapper mapper)
        {
            this.service = service;
            this.repository = repository;
            this.mapper = mapper;
            this.service.GetMapper(this.mapper);
        }
        [HttpGet]
        public async Task<ActionResult<List<NoteDTO>>> GetAll(
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
            NoteQueryParameters parameters = new(minValue, maxValue, earliestDate, latestDate, currentPage, pageSize, orderBy, desc);
            var Notes = await service.GetAllNotes(repository, parameters);
            if (Notes is null) return NotFound();
            return Ok(Notes);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NoteDTO>> GetOne(int id)
        {
            var Note = await service.JustGetNote(id, repository);
            if (Note is null) return NotFound();
            var DTO = service.GetNote(Note);
            return Ok(DTO);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note4Create note4Create)
        {
            var NoteDTO = await service.CreateNote(note4Create, repository);
            if (NoteDTO is null) return BadRequest();
            return CreatedAtAction(nameof(GetOne), new { id = NoteDTO.Id }, NoteDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var Note = await service.JustGetNote(id, repository);
            if (Note is null) return NotFound();
            var NoteDTO = await service.DeleteNote(Note, repository);
            if (NoteDTO is null) return BadRequest();
            return NoContent();
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note4Create UpdateData)
        {
            var Note = await service.JustGetNote(id, repository);
            if (Note is null) return NotFound();
            var NoteDTO = await service.UpdateNote(UpdateData, Note, repository);
            if (NoteDTO is null) return BadRequest();
            return NoContent();
        }
    }
}
