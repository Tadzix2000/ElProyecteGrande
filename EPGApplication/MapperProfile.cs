﻿using AutoMapper;
using EPGDomain;
using EPGApplication.DTOs.Read;
using EPGApplication.DTOs.CreateUpdate;

namespace EPGApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Note, NoteDTO>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<Work, WorkDTO>();
            CreateMap<Author4Create, Author>();
            CreateMap<Comment4Create, Comment>();
            CreateMap<Note4Create, Note>();
            CreateMap<Review4Create, Review>();
            CreateMap<Work4Create, Work>();
        }
    }
}