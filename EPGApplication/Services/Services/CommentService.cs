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

        public async Task<List<CommentDTO>?> GetComments(ICommentRepository repository, CommentQueryParameters parameters)
        {
            var await Result = repository.GetComments(parameters);
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
        public async Task<Comment?> JustGetComment(int id, ICommentRepository repository)
        {
            return await repository.GetComment(id);
        }
        public async Task<List<CommentDTO>?> GetResponsesFromComment(Comment comment, ICommentRepository repository, CommentQueryParameters parameters)
        {
            var Responses = await repository.GetResponsesFromComment(comment, parameters);
            if (Responses is null) return null;
            var ResultDTO = new List<CommentDTO>();
            foreach(var response in ResultDTO)
            {
                ResultDTO.Add(Mapper.Map<CommentDTO>(response));
            }
            return ResultDTO;
        }
        public async Task<CommentDTO?> CreateComment(Comment4Create Data, ICommentRepository repository)
        {
            var Comment = Mapper.Map<Comment>(Data);
            await repository.GetSuperiorObjects(Data, Comment);
            if (!Comment.VerifyNullables()) return null;
            Comment = await repository.CreateComment(Comment);
            if (Comment is null) return null;
            return Mapper.Map<CommentDTO>(Comment);
        }
        public async Task<CommentDTO?> UpdateComment(Comment4Create Data, Comment oldComment, ICommentRepository repository)
        {
            var Comment = Mapper.Map<Comment>(Data);
            var update = await repository.UpdateComment(oldComment, Data);
            await repository.GetSuperiorObjects(Data, Comment);
            if (!Comment.VerifyNullables()) return null;
            if (update) return Mapper.Map<CommentDTO>(oldComment);
            return null;
        }
        public async Task<CommentDTO?> DeleteComment(Comment comment, ICommentRepository repository)
        {
            if (await repository.DeleteComment(comment)) return Mapper.Map<CommentDTO>(comment);
            return null;
        }
        public void GetMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
