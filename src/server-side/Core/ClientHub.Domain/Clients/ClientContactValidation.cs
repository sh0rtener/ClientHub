using System.Net.Mail;
using System.Text.RegularExpressions;
using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients;

public static class ClientContactValidationFactory
{
    public static ClientContactValidation Create(ClientContactType type) =>
        type switch
        {
            ClientContactType.Email => new EmailContactValidation(),
            ClientContactType.Phone => new PhoneContactValidation(),
            _ => throw new InvalidClientContactException(),
        };
}

public abstract class ClientContactValidation
{
    public abstract bool IsValidInput(string input);
}

public sealed class EmailContactValidation : ClientContactValidation
{
    public override bool IsValidInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        try
        {
            var email = new MailAddress(input);
            return email.Address == input;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}

public sealed class PhoneContactValidation : ClientContactValidation
{
    private static readonly Regex PhoneRegex = new(
        @"^(\+7|8)[\s\-]?\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}$",
        RegexOptions.Compiled
    );

    public override bool IsValidInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;
        return PhoneRegex.IsMatch(input);
    }
}
