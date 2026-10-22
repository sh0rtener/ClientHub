using System.Text.RegularExpressions;

namespace ClientHub.Domain.IndividualEntrepreneurs;

public sealed class IndividualEntrepreneurRequisite : Entity<Guid>
{
    private const string _textRegex = @"\A[^\r\n\t]{1,300}\z";
    private const string _kindRegex = @"\A[^\r\n\t]{1,200}\z";

    private const int _maxKindLength = 200;
    private const int _maxRegistratorLength = 300;
    private const int _maxTaxAuthorityLength = 300;

    public string Kind { get; private set; }
    public string Registrator { get; private set; }
    public string TaxAuthority { get; private set; }

    public DateTime RegistrationDate { get; private set; }
    public DateTime TaxRegistrationDate { get; private set; }

    public IndividualEntrepreneurRequisite(
        string kind,
        string registrator,
        string taxAuthority,
        DateTime registrationDate,
        DateTime taxRegistrationDate
    )
    {
        ValidateKind(kind);
        Kind = kind.Trim();
        ValidateRegistrator(registrator);
        Registrator = registrator.Trim();
        ValidateTaxAuthority(taxAuthority);
        TaxAuthority = taxAuthority.Trim();
        ValidateRegDate(registrationDate);
        RegistrationDate = registrationDate;
        ValidateTaxDate(taxRegistrationDate, registrationDate);
        TaxRegistrationDate = taxRegistrationDate;
    }

    public void ChangeKind(string kind)
    {
        ValidateKind(kind);
        Kind = kind.Trim();
    }

    public void ChangeRegistrator(string registrator)
    {
        ValidateRegistrator(registrator);
        Registrator = registrator.Trim();
    }

    public void ChangeTaxAuthority(string taxAuthority)
    {
        ValidateTaxAuthority(taxAuthority);
        TaxAuthority = taxAuthority.Trim();
    }

    private void ValidateKind(string kind)
    {
        if (string.IsNullOrWhiteSpace(kind))
            throw new InvalidPropertyException(nameof(kind));
        if (kind.Trim().Length > _maxKindLength)
            throw new InvalidPropertyException(
                nameof(kind),
                $"не должен превышать {_maxKindLength} символов"
            );
        if (!Regex.IsMatch(kind.Trim(), _kindRegex))
            throw new InvalidPropertyException(
                nameof(kind),
                "не должен содержать переносы строк и табуляцию"
            );
    }

    private void ValidateRegistrator(string registrator)
    {
        if (string.IsNullOrWhiteSpace(registrator))
            throw new InvalidPropertyException(nameof(registrator));
        if (registrator.Trim().Length > _maxRegistratorLength)
            throw new InvalidPropertyException(
                nameof(registrator),
                $"не должен превышать {_maxRegistratorLength} символов"
            );
        if (!Regex.IsMatch(registrator.Trim(), _textRegex))
            throw new InvalidPropertyException(
                nameof(registrator),
                "не должен содержать переносы строк и табуляцию"
            );
    }

    private void ValidateTaxAuthority(string taxAuthority)
    {
        if (string.IsNullOrWhiteSpace(taxAuthority))
            throw new InvalidPropertyException(nameof(taxAuthority));
        if (taxAuthority.Trim().Length > _maxTaxAuthorityLength)
            throw new InvalidPropertyException(
                nameof(taxAuthority),
                $"не должен превышать {_maxTaxAuthorityLength} символов"
            );
        if (!Regex.IsMatch(taxAuthority.Trim(), _textRegex))
            throw new InvalidPropertyException(
                nameof(taxAuthority),
                "не должен содержать переносы строк и табуляцию"
            );
    }

    public void ValidateRegDate(DateTime registrationDate)
    {
        if (registrationDate == default)
            throw new InvalidPropertyException(nameof(registrationDate));
        if (registrationDate.Date > DateTime.Today)
            throw new InvalidPropertyException(nameof(registrationDate), "не может быть в будущем");
    }

    public void ValidateTaxDate(DateTime taxDate, DateTime registrationDate)
    {
        if (taxDate == default)
            throw new InvalidPropertyException(nameof(taxDate));
        if (taxDate.Date > DateTime.Today)
            throw new InvalidPropertyException(nameof(taxDate), "не может быть в будущем");

        if (taxDate.Date < registrationDate.Date)
            throw new InvalidPropertyException(
                nameof(taxDate),
                "не может быть раньше даты регистрации"
            );
    }
}
