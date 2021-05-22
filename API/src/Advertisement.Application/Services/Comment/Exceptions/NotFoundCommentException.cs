using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Comment.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с комментариями.
    /// </summary>
    public sealed class NotFoundCommentException : NotFoundException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Комментарий";
        public NotFoundCommentException(Guid commentId) : base(entityName, commentId.ToString())
        {
        }
    }
}