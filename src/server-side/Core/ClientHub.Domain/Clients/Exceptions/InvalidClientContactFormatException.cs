namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidClientContactFormatException : DomainException
{
    private const string _message = "Произошла ошибка при форматировании переданных контактов";

    public InvalidClientContactFormatException()
        : base(_message) { }
}
