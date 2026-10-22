using System.Text.RegularExpressions;

namespace ClientHub.Domain.Clients;

public enum PersonGender
{
    Male = 1,
    Female,
}

public sealed class Person : AuditableEntity<Guid>
{
    private const string _nameRegex = @"\A[\p{L}][\p{L}\-']{0,99}\z";
    private const string _regionCodeRegex = @"\A[0-9]{2,3}\z";

    private const int _maxNameLength = 100;
    private const int _maxRegionCodeLength = 3;
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Patronymic { get; private set; }
    public DateTime Birthdate { get; private set; }
    public PersonGender Gender { get; private set; }
    public string RegionCode { get; private set; }

    public Person(
        string firstname,
        string lastname,
        string? patronymic,
        DateTime birthdate,
        PersonGender gender,
        string regionCode
    )
    {
        ValidateFirstname(firstname);
        Firstname = firstname.Trim();
        ValidateLastname(lastname);
        Lastname = lastname.Trim();
        ValidatePatronymic(patronymic);
        Patronymic = patronymic?.Trim() ?? null;
        ValidateBirthdate(birthdate);
        Birthdate = birthdate;
        Guard.ValidateEnum(gender);
        Gender = gender;
        ValidateRegionCode(regionCode);
        RegionCode = regionCode.Trim();
    }

    public void ChangeFirstname(string firstname)
    {
        ValidateFirstname(firstname);
        Firstname = firstname.Trim();
    }

    public void ChangeLastname(string lastname)
    {
        ValidateLastname(lastname);
        Lastname = lastname.Trim();
    }

    public void ChangePatronymic(string? patronymic)
    {
        ValidatePatronymic(patronymic);
        Patronymic = patronymic?.Trim();
    }

    public void ChangeBirthdate(DateTime birthdate)
    {
        ValidateBirthdate(birthdate);
        Birthdate = birthdate;
    }

    public void ChangeGender(PersonGender gender)
    {
        Guard.ValidateEnum(gender);
        Gender = gender;
    }

    public void ChangeRegionCode(string regionCode)
    {
        ValidateRegionCode(regionCode);
        RegionCode = regionCode.Trim();
    }

    private void ValidateFirstname(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (value.Trim().Length > _maxNameLength)
            throw new InvalidPropertyException(
                nameof(value),
                $"не должно превышать {_maxNameLength} символов"
            );
        if (!Regex.IsMatch(value.Trim(), _nameRegex))
            throw new InvalidPropertyException(
                nameof(value),
                "должно содержать только буквы, дефисы и апострофы"
            );
    }

    private void ValidateLastname(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (value.Trim().Length > _maxNameLength)
            throw new InvalidPropertyException(
                nameof(value),
                $"не должно превышать {_maxNameLength} символов"
            );
        if (!Regex.IsMatch(value.Trim(), _nameRegex))
            throw new InvalidPropertyException(
                nameof(value),
                "должно содержать только буквы, дефисы и апострофы"
            );
    }

    private void ValidatePatronymic(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;
        if (value.Trim().Length > _maxNameLength)
            throw new InvalidPropertyException(
                nameof(value),
                $"не должно превышать {_maxNameLength} символов"
            );
        if (!Regex.IsMatch(value.Trim(), _nameRegex))
            throw new InvalidPropertyException(
                nameof(value),
                "должно содержать только буквы, дефисы и апострофы"
            );
    }

    private void ValidateBirthdate(DateTime value)
    {
        if (value == default)
            throw new InvalidPropertyException(nameof(value));
        if (value.Date > DateTime.Today)
            throw new InvalidPropertyException(nameof(value), "не может быть в будущем");
        if (value.Date < DateTime.Today.AddYears(-150))
            throw new InvalidPropertyException(nameof(value), "не может быть больше 150 лет назад");
    }

    private void ValidateRegionCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropertyException(nameof(value));
        if (value.Trim().Length > _maxRegionCodeLength)
            throw new InvalidPropertyException(
                nameof(value),
                $"не должен превышать {_maxRegionCodeLength} символа"
            );
        if (!Regex.IsMatch(value.Trim(), _regionCodeRegex))
            throw new InvalidPropertyException(nameof(value), "должен содержать 2 или 3 цифры");
    }
}
