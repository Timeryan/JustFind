using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Comment.Exceptions
{
    public sealed class NoAdForCommentCreationException : NoRightsException
    {
        public NoAdForCommentCreationException(string message) : base(message)
        {
        }
    }
}
