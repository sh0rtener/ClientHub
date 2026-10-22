namespace ClientHub.Domain.Clients.Exceptions;

public sealed class CannotChangeArbitrageDealStatusException : DomainException
{
    private const string _message = "Невозможно поменять статус арбитражного дела";

    public CannotChangeArbitrageDealStatusException()
        : base(_message) { }
}
