using System;

namespace Advertisement.Application.Services.Identity.Exceptions
{
    public class IdentityUserLoginException : ApplicationException
    {
        public IdentityUserLoginException(string message) : base(message)
        {
        }
    }
}