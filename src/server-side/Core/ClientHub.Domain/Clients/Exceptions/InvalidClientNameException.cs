namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidClientNameException : DomainException
{
    private const string _message = "Неправильно передано имя пользователя";

    public InvalidClientNameException()
        : base(_message) { }
}
