namespace ClientHub.Domain.Clients.Exceptions;

public sealed class InvalidInnException : DomainException
{
    private const string _message = "Неверное значение ИНН. ";

    public InvalidInnException()
        : base(_message) { }
}
