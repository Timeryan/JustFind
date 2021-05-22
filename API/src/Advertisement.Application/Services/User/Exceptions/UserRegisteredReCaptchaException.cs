using System;

namespace Advertisement.Application.Services.User.Exceptions
{
    public class UserRegisteredReCaptchaException : Exception
    {
        public UserRegisteredReCaptchaException(string message) : base(message)
        {
        }
    }
}