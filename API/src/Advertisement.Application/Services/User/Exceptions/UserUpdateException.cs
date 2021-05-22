using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.User.Exceptions
{
    public class UserUpdateException : DomainException
    {
        public UserUpdateException(string message) : base(message)
        {
        }
    }
}