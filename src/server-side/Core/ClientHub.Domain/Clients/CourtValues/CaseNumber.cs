using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients.CourtValues;

public sealed class CaseNumber : DefaultStringValueObject
{
    private const string _caseNumberRegex =
        @"\A[\u0410A][0-9]{2}-[0-9]{1,7}/[0-9]{4}(?:-[\p{L}0-9]{1,10})?\z";

    public CaseNumber(string value)
        : base(value) { }

    public override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (!Regex.IsMatch(value.Trim(), _caseNumberRegex))
            throw new InvalidPropertyException(nameof(value), "ожидаемый формат - А40-12345/2024");
    }
}
