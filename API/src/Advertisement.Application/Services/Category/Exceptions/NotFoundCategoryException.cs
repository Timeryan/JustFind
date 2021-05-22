using Advertisement.Domain.Shared.Exceptions;
using System;

namespace Advertisement.Application.Services.Category.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с категориями.
    /// </summary>
    public class NotFoundCategoryException : NotFoundException
    {
     /// <summary>
     /// Имя сущности.
     /// </summary>
        private static string entityName = "Категория";
        public NotFoundCategoryException(Guid id) : base(entityName, id.ToString())
        {
        }
    }
}