namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidClientAddressException : DomainException
{
    private const string _message = "Неверное значение адреса клиента";

    public InvalidClientAddressException()
        : base(_message) { }
}
