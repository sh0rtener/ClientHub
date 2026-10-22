namespace ClientHub.Domain.Clients.Exceptions;

public sealed class CannotChangeClientStatusException : DomainException
{
    private const string _message = "Невозможно изменить статус";

    public CannotChangeClientStatusException()
        : base(_message) { }
}
