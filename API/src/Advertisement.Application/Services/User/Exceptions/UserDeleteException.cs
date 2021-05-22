using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.User.Exceptions
{
    public class UserDeleteException : DomainException
    {
        public UserDeleteException(string message) : base(message)
        {
        }
    }
}