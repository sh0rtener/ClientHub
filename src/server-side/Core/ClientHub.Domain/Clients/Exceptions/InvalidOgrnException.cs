namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidOgrnException : DomainException
{
    private const string _message = "Неверное значение параметра ОГРН. ";

    public InvalidOgrnException()
        : base(_message) { }
}
