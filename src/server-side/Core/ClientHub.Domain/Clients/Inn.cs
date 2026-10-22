using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients.CourtValues;

public sealed class Inn : ValueObject
{
    private const string _innRegex = @"\A(?:[0-9]{10}|[0-9]{12})\z";
    public string Value { get; }

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), _innRegex))
            throw new InvalidPropertyException(nameof(value), "должно быть от 10 до 12 цифр");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
