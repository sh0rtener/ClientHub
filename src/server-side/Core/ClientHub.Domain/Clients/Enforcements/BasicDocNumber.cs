using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients.Enforcements;

public sealed class BasicDocNumber : DefaultStringValueObject
{
    private const string _docNumberRegex = @"\A[^\s].{0,99}\z";

    public BasicDocNumber(string value)
        : base(value) { }

    public override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), _docNumberRegex))
            throw new InvalidPropertyException(nameof(value), "ожидаемый формат - А40-12345/2024");
    }
}
