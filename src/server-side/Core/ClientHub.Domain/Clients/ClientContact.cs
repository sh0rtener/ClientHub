using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients;

public enum ClientContactType
{
    Phone,
    Email,
}

public sealed class ClientContact : Entity<int>
{
    public ClientContactType ContactType { get; private set; }
    public string Value { get; private set; }

    public ClientContact(ClientContactType type, string value)
    {
        ContactType = type;

        var valueValidation = ClientContactValidationFactory.Create(type);

        if (!valueValidation.IsValidInput(value))
            throw new InvalidClientContactException();

        Value = ClientContactFormatterFactory.Create(type).Format(value);
    }
}
