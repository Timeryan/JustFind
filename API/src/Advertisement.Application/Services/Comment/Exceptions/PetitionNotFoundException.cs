using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Comment.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с жалобами.
    /// </summary>
    public sealed class PetitionNotFoundException : NotFoundException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Жалоба";
        public PetitionNotFoundException(Guid commentId) : base(entityName, commentId.ToString())
        {
        }
    }
}