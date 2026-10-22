namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidClientRegionException : DomainException
{
    private const string _message = "Неверное значение региона клиента";

    public InvalidClientRegionException()
        : base(_message) { }
}
