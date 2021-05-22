using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Identity.Exceptions
{
    public class IdentityUserNotFoundException : IdentityException
    {
        public IdentityUserNotFoundException(string message) : base(message)
        {
        }
    }
}
