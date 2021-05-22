using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Comment.Contracts.CreateComment;
using Advertisement.Application.Services.Comment.Contracts.DeleteComment;
using Advertisement.Application.Services.Comment.Contracts.GetPagedComment;
using Advertisement.Application.Services.Comment.Contracts.UpdateComment;

namespace Advertisement.Application.Services.Comment.Interfaces
{
    public interface ICommentService
    {
        Task<CreateCommentResponse> CreateComment(CreateCommentRequest commentRequest, CancellationToken cancellationToken);
        Task<GetPagedCommentResponse> GetPagedComment(GetPagedCommentRequest request, CancellationToken cancellationToken);
        Task DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken);
        Task UpdateComment(UpdateCommentRequest commentRequest, CancellationToken cancellationToken);
    }
}