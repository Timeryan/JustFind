using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Contracts.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с объявлениями.
    /// </summary>
    public sealed class NoAdFoundException : NotFoundException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Объявление";

        public NoAdFoundException(Guid adId) : base(entityName, adId.ToString())
        {
        }
    }
}