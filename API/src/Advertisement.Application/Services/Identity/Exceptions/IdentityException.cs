using System;

namespace Advertisement.Application.Services.Identity.Exceptions
{
    public class IdentityException : ApplicationException
    {
        public IdentityException(string message) : base(message)
        {
        } 
    }
}