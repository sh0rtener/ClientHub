namespace ClientHub.Domain.LegalEntities;

public enum LegalEntityLinkType
{
    Maintainer = 1,
    Beneficiar,
    Founder,
}

public class LegalEntityLink : AuditableEntity<Guid>
{
    public LegalEntityLinkType Type { get; private set; }
    public DateTime AssignedAt { get; private set; }
    public DateTime? DisassignedAt { get; private set; }
    public decimal IncomeShareInDollars { get; private set; }
    public decimal IncomeShareInPercent { get; private set; }

    public LegalEntityLink(
        LegalEntityLinkType type,
        DateTime assignedAt,
        DateTime? disassignedAt,
        decimal incomeShareInDollars,
        decimal incomeShareInPercent
    )
    {
        Guard.ValidateEnum(type);
        Type = type;
        ValidateAssignedAt(assignedAt);
        AssignedAt = assignedAt;
        ValidateDisassignedAt(disassignedAt, assignedAt);
        DisassignedAt = disassignedAt;
        ValidateIncomeShareInDollars(incomeShareInDollars);
        IncomeShareInDollars = incomeShareInDollars;
        ValidateIncomeShareInPercent(incomeShareInPercent);
        IncomeShareInPercent = incomeShareInPercent;
    }

    public void ChangeType(LegalEntityLinkType type)
    {
        Guard.ValidateEnum(type);
        Type = type;
    }

    public void ChangeAssignedAt(DateTime assignedAt)
    {
        ValidateAssignedAt(assignedAt);
        AssignedAt = assignedAt;
    }

    public void ChangeDisassignedAt(DateTime? disassignedAt)
    {
        ValidateDisassignedAt(disassignedAt, AssignedAt);
        DisassignedAt = disassignedAt;
    }

    public void ChangeIncomeShareInDollars(decimal incomeShareInDollars)
    {
        ValidateIncomeShareInDollars(incomeShareInDollars);
        IncomeShareInDollars = incomeShareInDollars;
    }

    public void ChangeIncomeShareInPercent(decimal incomeShareInPercent)
    {
        ValidateIncomeShareInPercent(incomeShareInPercent);
        IncomeShareInPercent = incomeShareInPercent;
    }

    public void Terminate(DateTime disassignedAt)
    {
        if (DisassignedAt.HasValue)
            throw new InvalidPropertyException(nameof(disassignedAt), "связь уже прекращена");

        ValidateDisassignedAt(disassignedAt, AssignedAt);

        DisassignedAt = disassignedAt;
    }

    private void ValidateAssignedAt(DateTime assignedAt)
    {
        if (assignedAt == default)
            throw new InvalidPropertyException(nameof(assignedAt));
        if (assignedAt.Date > DateTime.Today)
            throw new InvalidPropertyException(nameof(assignedAt), "не может быть в будущем");
    }

    private void ValidateDisassignedAt(DateTime? disassignedAt, DateTime assignedAt)
    {
        if (disassignedAt.HasValue)
        {
            if (disassignedAt.Value == default)
                throw new InvalidPropertyException(nameof(disassignedAt));
            if (disassignedAt.Value.Date < assignedAt.Date)
                throw new InvalidPropertyException(
                    nameof(disassignedAt),
                    "не может быть раньше даты назначения"
                );
        }
    }

    private void ValidateIncomeShareInDollars(decimal incomeShareInDollars)
    {
        if (incomeShareInDollars < 0)
            throw new InvalidPropertyException(
                nameof(incomeShareInDollars),
                "не может быть отрицательным"
            );
    }

    private void ValidateIncomeShareInPercent(decimal incomeShareInPercent)
    {
        if (incomeShareInPercent < 0 || incomeShareInPercent > 100)
            throw new InvalidPropertyException(
                nameof(incomeShareInPercent),
                "должна быть в диапазоне от 0 до 100"
            );
    }
}
