namespace ClientHub.Domain.Clients.Exceptions;

public sealed class CannotChangeEnforcementProceedingsStatusException : DomainException
{
    private const string _message = "Невозможно поменять статус исполнительного производства";

    public CannotChangeEnforcementProceedingsStatusException()
        : base(_message) { }
}
