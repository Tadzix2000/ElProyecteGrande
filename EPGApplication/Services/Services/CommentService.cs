using AutoMapper;
using EPGApplication.DTOs.CreateUpdate;
using EPGApplication.DTOs.Read;
using EPGApplication.Repositories.IRepositories;
using EPGApplication.QueryConfigurations.QueryParameters;
using EPGApplication.Services.IServices;
using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EPGApplication.Services.Services
{
    public class CommentService : ICommentService
    {
        public IMapper Mapper;

        public List<CommentDTO>? GetComments(ICommentRepository repository, CommentQueryParameters parameters)
        {
            var Result = repository.GetComments(parameters);
            if (Result is null || Result.Count() == 0) return null;
            var ResultDTO = new List<CommentDTO>();
            foreach (var Comment in Result)
            {
                var CommentDTO = Mapper.Map<CommentDTO>(Comment);
                ResultDTO.Add(CommentDTO);
            }
            return ResultDTO;
        }
        public CommentDTO? GetComment(Comment Comment)
        {
            if (Comment is null) return null;
            var CommentDTO =  Mapper.Map<CommentDTO>(Comment);
            //CommentDTO.AssignFeatures(this, Comment);
            return CommentDTO;
        }
        public Comment? JustGetComment(int id, ICommentRepository repository)
        {
            return repository.GetComment(id);
        }
        public List<CommentDTO>? GetResponsesFromComment(Comment comment, ICommentRepository repository, CommentQueryParameters parameters)
        {
            var Responses = repository.GetResponsesFromComment(comment, parameters);
            if (Responses is null) return null;
            var ResultDTO = new List<CommentDTO>();
            foreach(var response in ResultDTO)
            {
                ResultDTO.Add(Mapper.Map<CommentDTO>(response));
            }
            return ResultDTO;
        }
        public CommentDTO? CreateComment(Comment4Create Data, ICommentRepository repository)
        {
            var Comment = Mapper.Map<Comment>(Data);
            repository.GetSuperiorObjects(Data, Comment);
            if (!Comment.VerifyNullables()) return null;
            Comment = repository.CreateComment(Comment);
            if (Comment is null) return null;
            return Mapper.Map<CommentDTO>(Comment);
        }
        public CommentDTO? UpdateComment(Comment4Create Data, Comment oldComment, ICommentRepository repository)
        {
            var Comment = Mapper.Map<Comment>(Data);
            repository.UpdateComment(oldComment, Data);
            repository.GetSuperiorObjects(Data, Comment);
            if (!Comment.VerifyNullables()) return null;
            if (repository.UpdateComment(oldComment, Data)) return Mapper.Map<CommentDTO>(oldComment);
            return null;
        }
        public CommentDTO? DeleteComment(Comment comment, ICommentRepository repository)
        {
            if (repository.DeleteComment(comment)) return Mapper.Map<CommentDTO>(comment);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
