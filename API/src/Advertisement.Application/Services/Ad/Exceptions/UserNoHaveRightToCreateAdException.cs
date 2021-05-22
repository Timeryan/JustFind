using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Contracts.Exceptions
{
    public class UserNoHaveRightToCreateAdException : Exception
    {
        public UserNoHaveRightToCreateAdException(string message) : base(message)
        {
        }
    }
}