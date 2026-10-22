using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients;

public sealed class Okato : DefaultStringValueObject
{
    private const string _okatoRegex = @"\A\d{8}(?:\d{3})?\z";

    public Okato(string value)
        : base(value) { }

    public override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), _okatoRegex))
            throw new InvalidPropertyException(nameof(value), "Неверный формат ОКАТО");
    }
}
