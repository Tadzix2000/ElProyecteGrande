using AutoMapper;
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
            CreateMap<NoteDTO, Note>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<WorkDTO, Work>();
            CreateMap<Author4Create, Author>();
            CreateMap<Comment, Comment4Create>();
            CreateMap<Note, Note4Create>();
            CreateMap<Review, Review4Create>();
            CreateMap<Work, Work4Create>();
        }
    }
}
