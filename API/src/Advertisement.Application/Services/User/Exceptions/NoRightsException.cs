namespace Advertisement.Application.Services.User.Exceptions
{
    public sealed class NoRightsException : Domain.Shared.Exceptions.NoRightsException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}