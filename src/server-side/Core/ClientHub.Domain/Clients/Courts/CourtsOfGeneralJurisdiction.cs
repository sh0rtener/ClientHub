using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients.Courts;

public enum CourtsOfGeneralJurisdictionStatuses
{
    UnderConsideration = 1,
    Decided = 2,
    Appealed = 3,
    Completed = 4,
}

public enum CourtsOfGeneralJurisdictionResults
{
    Satisfied = 1,
    Rejected = 2,
    Terminated = 3,
}

public sealed class CourtsOfGeneralJurisdiction : Entity<Guid>
{
    public CaseNumber CaseNumber { get; private set; }
    public CourtName CourtName { get; private set; }
    public Instanse Instanse { get; private set; }
    public CaseType CaseType { get; private set; }
    public PartyRole PartyRole { get; private set; }
    public CountrAgentName CountrAgentName { get; private set; }
    public Inn CountrAgentInn { get; private set; }
    public ClaimAmount ClaimAmount { get; private set; }
    public Subject Subject { get; private set; }
    public CourtsOfGeneralJurisdictionStatuses Status { get; private set; }
    public CourtsOfGeneralJurisdictionResults? Result { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public DateTime? LiquidationDate { get; private set; }

    public CourtsOfGeneralJurisdiction(
        CaseNumber caseNumber,
        CourtName courtName,
        Instanse instance,
        CaseType caseType,
        PartyRole partyRole,
        CountrAgentName counterpartyName,
        Inn counterpartyInn,
        ClaimAmount claimAmount,
        Subject subject,
        CourtsOfGeneralJurisdictionStatuses status,
        CourtsOfGeneralJurisdictionResults? result,
        DateTime registrationDate,
        DateTime? liqudationDate
    )
    {
        CaseNumber = caseNumber ?? throw new InvalidPropertyException(nameof(caseNumber));
        CourtName = courtName ?? throw new InvalidPropertyException(nameof(courtName));
        Instanse = instance ?? throw new InvalidPropertyException(nameof(instance));
        CaseType = caseType ?? throw new InvalidPropertyException(nameof(caseType));
        PartyRole = partyRole ?? throw new InvalidPropertyException(nameof(partyRole));
        CountrAgentName =
            counterpartyName ?? throw new InvalidPropertyException(nameof(counterpartyName));
        CountrAgentInn =
            counterpartyInn ?? throw new InvalidPropertyException(nameof(counterpartyInn));
        ClaimAmount = claimAmount ?? throw new InvalidPropertyException(nameof(claimAmount));
        Subject = subject ?? throw new InvalidPropertyException((nameof(subject)));
        Guard.ValidateEnum(status);
        Status = status;
        ValidateResult(result);
        Result = result;
        Guard.ValidateDate(registrationDate);
        RegistrationDate = registrationDate;
        ValidateLiqudationDate(liqudationDate, registrationDate, result, status);
        LiquidationDate = liqudationDate;
    }

    public void ChangeCaseNumber(CaseNumber caseNumber) =>
        CaseNumber = caseNumber ?? throw new InvalidPropertyException(nameof(caseNumber));

    public void ChangeCourtName(CourtName courtName) =>
        CourtName = courtName ?? throw new InvalidPropertyException(nameof(courtName));

    public void ChangeInstance(Instanse instance) =>
        Instanse = instance ?? throw new InvalidPropertyException(nameof(instance));

    public void ChangeCaseType(CaseType caseType) =>
        CaseType = caseType ?? throw new InvalidPropertyException(nameof(caseType));

    public void ChangePartyRole(PartyRole partyRole) =>
        PartyRole = partyRole ?? throw new InvalidPropertyException(nameof(partyRole));

    public void ChangeCounterpartyName(CountrAgentName countr) =>
        CountrAgentName = countr ?? throw new InvalidPropertyException(nameof(countr));

    public void ChangeCounterpartyInn(Inn inn) =>
        CountrAgentInn = inn ?? throw new InvalidPropertyException(nameof(inn));

    public void ChangeClaimAmount(ClaimAmount claimAmount) =>
        ClaimAmount = claimAmount ?? throw new InvalidPropertyException(nameof(claimAmount));

    public void ChangeSubject(Subject subject) =>
        Subject = subject ?? throw new InvalidPropertyException(nameof(subject));

    public void Liquidate(CourtsOfGeneralJurisdictionResults result, DateTime liqudationDate)
    {
        if (
            Status
            is not (
                CourtsOfGeneralJurisdictionStatuses.UnderConsideration
                or CourtsOfGeneralJurisdictionStatuses.Appealed
            )
        )
        {
            throw new CannotChangeCourtStatusException();
        }

        Guard.ValidateEnum(result);
        Guard.ValidateDate(liqudationDate);

        if (liqudationDate.Date < RegistrationDate.Date)
            throw new InvalidPropertyException(
                nameof(liqudationDate),
                "дата решения не может быть раньше даты регистрации"
            );

        if (LiquidationDate is { } previous && liqudationDate.Date < previous.Date)
        {
            throw new InvalidPropertyException(
                nameof(liqudationDate),
                "дата решения не может быть раньше предыдущей даты"
            );
        }

        Result = result;
        LiquidationDate = liqudationDate;
        Status = CourtsOfGeneralJurisdictionStatuses.Decided;
    }

    public void Appeal()
    {
        if (Status != CourtsOfGeneralJurisdictionStatuses.Decided)
            throw new CannotChangeArbitrageDealStatusException();

        Status = CourtsOfGeneralJurisdictionStatuses.Appealed;
    }

    public void Complete()
    {
        if (
            Status
            is not (
                CourtsOfGeneralJurisdictionStatuses.Decided
                or CourtsOfGeneralJurisdictionStatuses.Appealed
            )
        )
        {
            throw new CannotChangeArbitrageDealStatusException();
        }

        Status = CourtsOfGeneralJurisdictionStatuses.Completed;
    }

    private void ValidateResult(CourtsOfGeneralJurisdictionResults? result)
    {
        if (result is { } r && !Enum.IsDefined(r))
            throw new InvalidPropertyException(nameof(result), "неопределенное значение");
    }

    private void ValidateLiqudationDate(
        DateTime? liquidationDate,
        DateTime regDate,
        CourtsOfGeneralJurisdictionResults? result,
        CourtsOfGeneralJurisdictionStatuses actualStatus
    )
    {
        if (
            actualStatus
                is not (
                    CourtsOfGeneralJurisdictionStatuses.Decided
                    or CourtsOfGeneralJurisdictionStatuses.Completed
                )
            && (liquidationDate.HasValue)
        )
            throw new InvalidPropertyException(
                nameof(regDate),
                "дата ликвидации не может быть без решения в целом"
            );

        if (liquidationDate.HasValue != result.HasValue)
            throw new InvalidPropertyException(
                nameof(liquidationDate),
                "дата результата не может быть без самого результата"
            );

        if (liquidationDate is { } decision && decision.Date < regDate.Date)
            throw new InvalidPropertyException(
                nameof(liquidationDate),
                "дата начала не может быть больше чем дата решения"
            );
    }
}
