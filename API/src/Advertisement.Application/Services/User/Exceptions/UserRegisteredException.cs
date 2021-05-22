using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.User.Exceptions
{
    public class UserRegisteredException : DomainException
    {
        public UserRegisteredException(string message) : base(message)
        {
        }
    }
}