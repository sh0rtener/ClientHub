namespace ClientHub.Domain.Clients.Exceptions;

public sealed class CannotChangeCourtStatusException : DomainException
{
    private const string _message = "Невозможно поменять статус судов юриздикций";

    public CannotChangeCourtStatusException()
        : base(_message) { }
}
