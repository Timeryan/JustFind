namespace Advertisement.Domain.Shared
{
    /// <summary>
    /// Результат рассмотрения жалобы.
    /// </summary>
    public enum DecisionEnum
    {
        /// <summary>
        /// Нарушений не обнаружено.
        /// </summary>
        Ok = 0,

        /// <summary>
        /// Бан пользователя.
        /// </summary>
        BanUser = 1,

        /// <summary>
        /// Бан обьявления.
        /// </summary>
        BanAd = 2,
    }
}