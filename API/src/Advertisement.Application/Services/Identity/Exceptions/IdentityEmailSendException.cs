namespace Advertisement.Application.Services.Identity.Exceptions
{
    public class IdentityEmailSendException : IdentityException
    {
        public IdentityEmailSendException(string message) : base(message)
        {
        }
    }
}