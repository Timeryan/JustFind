using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Contracts.Exceptions
{
    public class UserNoHaveRightsToEditAdException : Exception
    {
        public UserNoHaveRightsToEditAdException(string message) : base(message)
        {
        }
    }
}