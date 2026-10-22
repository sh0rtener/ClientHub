namespace ClientHub.Domain;

public sealed class InvalidPropertyException : DomainException
{
    public InvalidPropertyException(string field, string reason = "не должно быть пустым")
        : base($"Неверно заполнено {field}: {reason}") { }
}
