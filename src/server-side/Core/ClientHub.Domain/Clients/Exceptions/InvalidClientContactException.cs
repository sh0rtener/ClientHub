namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidClientContactException : DomainException
{
    private const string _message =
        "Контакты клиента заполнены неверно. Перепроверьте правильность ввода";

    public InvalidClientContactException()
        : base(_message) { }
}
