namespace Advertisement.Domain.Shared.Exceptions
{
    /// <summary>
    /// Базовое исключение, возникающее когда сущность не найдена.
    /// </summary>
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string name, string id) : base($"Сущность \"{name}\" с идентификатором {id} - не найдена!")
        {
        }
    }
}