using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Category.Exceptions
{
    public class NoRightsToAddCategoryException : NoRightsException
    {
        public NoRightsToAddCategoryException(string message) : base(message)
        {
        }
    }
}