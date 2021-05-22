using Advertisement.Application.Services.Comment.Contracts.CreateComment;
using Advertisement.Application.Services.Comment.Contracts.DeleteComment;
using Advertisement.Application.Services.Comment.Contracts.GetPagedComment;
using Advertisement.Application.Services.Comment.Contracts.UpdateComment;
using Advertisement.PublicApi.Models.Comment;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class CommentMapControllerProfile : Profile
    {
        public CommentMapControllerProfile()
        {
            CreateMap<CommentCreateDto, CreateCommentRequest>();
            CreateMap<CommentGetPagedDto, GetPagedCommentRequest>();
            CreateMap<CommentUpdateDto, UpdateCommentRequest>();
            CreateMap<CommentDeleteDto, DeleteCommentRequest>();
        }
        
    }
}