using System.Text.RegularExpressions;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients.Enforcements;

public enum EnforcementProceedingsStatuses
{
    Start,
    Stopped,
    Closed,
}

public sealed class EnforcementProceedings : Entity<Guid>
{
    public FsspDepartament FsspDepartament { get; private set; }
    public Bailiff Bailiff { get; private set; }
    public BasicDocType BasicDocType { get; private set; }
    public BasicDocNumber BasicDocNumber { get; private set; }
    public DateTime BasicDocDate { get; private set; }
    public Subject Subject { get; private set; }
    public Creditor Creditor { get; private set; }
    public Inn CreditorInn { get; private set; }
    public decimal DeptAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public DateTime OpenedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public string? ClosureReason { get; private set; }
    public EnforcementProceedingsStatuses Status { get; private set; }

    public EnforcementProceedings(
        FsspDepartament fsspDepartament,
        Bailiff bailiff,
        BasicDocType basicDocType,
        BasicDocNumber basicDocNumber,
        DateTime basicDocDate,
        Subject subject,
        Creditor creditor,
        Inn creditorInn,
        decimal deptAmount,
        decimal remainingAmount,
        DateTime openedAt,
        DateTime? closedAt,
        string? closureReason,
        EnforcementProceedingsStatuses status
    )
    {
        Guard.ValidateEnum(status);
        Status = status;
        FsspDepartament =
            fsspDepartament ?? throw new InvalidPropertyException(nameof(fsspDepartament));
        Bailiff = bailiff ?? throw new InvalidPropertyException(nameof(bailiff));
        BasicDocType = basicDocType ?? throw new InvalidPropertyException(nameof(basicDocType));
        BasicDocNumber =
            basicDocNumber ?? throw new InvalidPropertyException(nameof(basicDocNumber));
        Guard.ValidateDate(basicDocDate);
        BasicDocDate = basicDocDate;
        Subject = subject ?? throw new InvalidPropertyException(nameof(subject));
        Creditor = creditor ?? throw new InvalidPropertyException(nameof(creditor));
        CreditorInn = creditorInn ?? throw new InvalidPropertyException(nameof(creditorInn));
        Guard.MustGreaterThan(deptAmount);
        DeptAmount = deptAmount;
        ValidateRemainingAmount(remainingAmount, deptAmount);
        RemainingAmount = remainingAmount;
        Guard.ValidateDate(openedAt);
        OpenedAt = openedAt;
        ValidateClosedAt(closedAt, status);
        ClosedAt = closedAt;
        if (closureReason is not null)
        {
            ValidateClosedReason(closureReason, status, openedAt, closedAt);
            ClosureReason = closureReason;
        }
    }

    public void ChangeFsspDepartament(FsspDepartament fsspDepartament) =>
        FsspDepartament =
            fsspDepartament ?? throw new InvalidPropertyException(nameof(fsspDepartament));

    public void ChangeBailiff(Bailiff bailiff) =>
        Bailiff = bailiff ?? throw new InvalidPropertyException(nameof(bailiff));

    public void ChangeBasicDocType(BasicDocType basicDocType) =>
        BasicDocType = basicDocType ?? throw new InvalidPropertyException(nameof(basicDocType));

    public void ChangeBasicDocNumber(BasicDocNumber basicDocNumber) =>
        BasicDocNumber =
            basicDocNumber ?? throw new InvalidPropertyException(nameof(basicDocNumber));

    public void ChangeBasicDocDate(DateTime basicDocDate)
    {
        Guard.ValidateDate(basicDocDate);
        BasicDocDate = basicDocDate;
    }

    public void ChangeSubject(Subject subject) =>
        Subject = subject ?? throw new InvalidPropertyException(nameof(subject));

    public void ChangeCreditor(Creditor creditor) =>
        Creditor = creditor ?? throw new InvalidPropertyException(nameof(creditor));

    public void ChangeCreditorInn(Inn creditorInn) =>
        CreditorInn = creditorInn ?? throw new InvalidPropertyException(nameof(creditorInn));

    public void ChangeDeptAmount(decimal deptAmount)
    {
        Guard.MustGreaterThan(deptAmount);
        DeptAmount = deptAmount;
    }

    public void ChangeRemainingAmount(decimal remainingAmount)
    {
        ValidateRemainingAmount(remainingAmount, DeptAmount);
        RemainingAmount = remainingAmount;
    }

    public void Stop()
    {
        if (Status != EnforcementProceedingsStatuses.Start)
            throw new CannotChangeEnforcementProceedingsStatusException();

        Status = EnforcementProceedingsStatuses.Stopped;
    }

    public void Resume()
    {
        if (Status != EnforcementProceedingsStatuses.Stopped)
            throw new CannotChangeEnforcementProceedingsStatusException();

        Status = EnforcementProceedingsStatuses.Start;
    }

    public void Close(DateTime closedAt, string closureReason)
    {
        if (
            Status
            is not (EnforcementProceedingsStatuses.Start or EnforcementProceedingsStatuses.Stopped)
        )
            throw new CannotChangeEnforcementProceedingsStatusException();

        ValidateClosedReason(closureReason, Status, OpenedAt, ClosedAt);

        ClosedAt = closedAt;
        ClosureReason = closureReason.Trim();
        Status = EnforcementProceedingsStatuses.Closed;
    }

    private void ValidateRemainingAmount(decimal remainingAmount, decimal deptAmount)
    {
        Guard.MustGreaterThan(remainingAmount);
        if (remainingAmount > deptAmount)
            throw new InvalidPropertyException(
                nameof(remainingAmount),
                "не может превышать сумму задолженности"
            );
    }

    private void ValidateClosedAt(DateTime? closedAt, EnforcementProceedingsStatuses status)
    {
        if (closedAt.HasValue && closedAt.Value == default)
            throw new InvalidPropertyException(nameof(closedAt));

        if (closedAt.HasValue != (status == EnforcementProceedingsStatuses.Closed))
            throw new InvalidPropertyException(
                nameof(closedAt),
                "дата закрытия должна быть указана только для закрытых производств"
            );
    }

    private void ValidateClosedReason(
        string closureReason,
        EnforcementProceedingsStatuses status,
        DateTime openedAt,
        DateTime? closedAt
    )
    {
        Guard.ValidateLength(closureReason);

        if (
            status == EnforcementProceedingsStatuses.Closed
            && string.IsNullOrWhiteSpace(closureReason)
        )
            throw new InvalidPropertyException(
                nameof(closureReason),
                "причина закрытия обязательна для закрытых производств"
            );

        if (closedAt is { } closed && closed.Date < openedAt.Date)
            throw new InvalidPropertyException(
                nameof(closedAt),
                "дата закрытия не может быть раньше даты открытия"
            );
    }
}
