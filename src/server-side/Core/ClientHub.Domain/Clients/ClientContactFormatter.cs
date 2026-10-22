using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients;

public static class ClientContactFormatterFactory
{
    public static ClientContactFormatter Create(ClientContactType type) =>
        type switch
        {
            ClientContactType.Email => new EmailContactFormatter(),
            ClientContactType.Phone => new PhoneContactFormatter(),
            _ => throw new InvalidClientContactFormatException(),
        };
}

public abstract class ClientContactFormatter
{
    public virtual string Format(string input) => input;
}

public sealed class PhoneContactFormatter : ClientContactFormatter
{
    public override string Format(string input)
    {
        var validator = new PhoneContactValidation();
        if (!validator.IsValidInput(input))
            throw new InvalidClientContactFormatException();

        if (input[0].Equals('8'))
            input = "7" + input[1..];
        return input
            .Trim()
            .Replace("(", "")
            .Replace(")", "")
            .Replace("-", "")
            .Replace("+", "")
            .Replace(" ", "");
    }
}

public sealed class EmailContactFormatter : ClientContactFormatter
{
    public override string Format(string input)
    {
        var validator = new EmailContactValidation();
        if (!validator.IsValidInput(input))
            throw new InvalidClientContactFormatException();

        return input.ToLowerInvariant().Trim();
    }
}