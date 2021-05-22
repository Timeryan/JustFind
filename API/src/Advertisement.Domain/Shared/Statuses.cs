namespace Advertisement.Domain.Shared
{
    public enum Statuses
    {
        /// <summary>
        /// Одобрено.
        /// </summary>
        OnSale,

        /// <summary>
        /// Недоступно.
        /// </summary>
        NotAvailable,

        /// <summary>
        /// Снято с публикации.
        /// </summary>
        Closed,

        /// <summary>
        /// Требуется проверка.
        /// </summary>
        ModerationRequired,

        /// <summary>
        /// Отклонено.
        /// </summary>
        Rejected,
        
        /// <summary>
        /// Повторная проверка.
        /// </summary>
        Remoderation
    }
}