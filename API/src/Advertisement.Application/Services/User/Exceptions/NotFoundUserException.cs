using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.User.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с пользователем.
    /// </summary>
    public sealed class NotFoundUserException : NotFoundException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Пользователь";
        public NotFoundUserException(Guid Id) : base(entityName, Id.ToString())
        {
        }
    }
}