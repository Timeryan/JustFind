namespace Advertisement.Domain.Shared.Exceptions
{
    /// <summary>
    /// Базовое исключение, возникающее когда сущность не валидна.
    /// </summary>
    public abstract class EntityNotInValidStateException : DomainException
    {
        protected EntityNotInValidStateException(string name, string id = null, string additionalMessage = null)
            : base($"Сущность \"{name}\" с идентификатором {id} - не валидна. {additionalMessage ?? string.Empty}")
        {
        }
    }
}