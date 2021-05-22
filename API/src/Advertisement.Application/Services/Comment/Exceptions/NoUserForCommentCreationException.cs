using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Comment.Exceptions
{
    public sealed class NoUserForCommentCreationException : NoRightsException
    {
        public NoUserForCommentCreationException(string message) : base(message)
        {
        }
    }
}
