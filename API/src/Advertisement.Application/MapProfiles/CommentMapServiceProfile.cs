using System;
using Advertisement.Application.Services.Comment.Contracts.CreateComment;
using Advertisement.Application.Services.Comment.Contracts.GetPagedComment;
using Advertisement.Application.Services.Comment.Contracts.UpdateComment;
using Advertisement.Domain;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class CommentMapServiceProfile : Profile
    {
        public CommentMapServiceProfile()
        {
            CreateMap<CreateCommentRequest, Comment>()
                .ForMember(dest => dest.Id, 
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AuthorId, 
                    opt => opt.Ignore())
                .ForMember(dest => dest.Author, 
                    opt => opt.Ignore())
                .ForMember(dest => dest.AdOwner, 
                opt => opt.Ignore());
            CreateMap<Comment, CreateCommentResponse>();
            CreateMap<GetPagedCommentRequest, GetPagedCommentResponse>() 
                .ForMember(dest => dest.Total, 
                    opt => opt.MapFrom(src => 0)) 
                .ForMember(dest => dest.Items, 
                    opt => opt.Ignore());
            CreateMap<Comment, GetPagedCommentResponseItem>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.MiddleName}"))
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Author.Photo));
            CreateMap<UpdateCommentRequest, Comment>()
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Author,
                    opt => opt.Ignore())
                .ForMember(dest => dest.AdId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.AdOwner,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}