using System;
using Advertisement.Domain.Shared;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с объявлениями.
    /// </summary>
    public sealed class AdShouldBeInCreatedStateForClosingException : EntityNotInValidStateException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Объявление";
        public AdShouldBeInCreatedStateForClosingException(Guid adId) 
            : base(entityName, adId.ToString(), $"{entityName} должно быть в статусе {Statuses.OnSale} для закрытия.")
        {
        }
    }
}