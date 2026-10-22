using System.Text.RegularExpressions;

namespace ClientHub.Domain.LegalEntities;

public sealed class Kpp : Entity<Guid>
{
    private const string _kppRegex = @"\A\d{9}\z";
    private const int _kppLength = 9;

    public string Code { get; private set; }

    public Kpp(string code)
    {
        ValidateKpp(code);
        Code = code.Trim();
    }

    public void ChangeCode(string code)
    {
        ValidateKpp(code);
        Code = code.Trim();
    }

    private void ValidateKpp(string kpp)
    {
        if (string.IsNullOrWhiteSpace(kpp))
            throw new InvalidPropertyException(nameof(kpp));
        if (!Regex.IsMatch(kpp.Trim(), _kppRegex))
            throw new InvalidPropertyException(
                nameof(kpp),
                $"должен содержать ровно {_kppLength} цифр"
            );
    }
}
