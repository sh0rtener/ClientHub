using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Exceptions;

namespace ClientHub.Domain.Clients.Arbitrages;

public enum ArbitrageDealStatuses
{
    UnderConsideration = 1,
    Decided = 2,
    Appealed = 3,
    Completed = 4,
}

public enum ArbitrageDealResults
{
    Satisfied = 1,
    Rejected = 2,
    Terminated = 3,
}

public sealed class ArbitrageDeal : Entity<Guid>
{
    public CaseNumber CaseNumber { get; private set; }
    public CourtName CourtName { get; private set; }
    public Instanse Instanse { get; private set; }
    public Category Category { get; private set; }
    public PartyRole PartyRole { get; private set; }
    public CountrAgentName CountrAgentName { get; private set; }
    public Inn CountrAgentInn { get; private set; }
    public ClaimAmount ClaimAmount { get; private set; }
    public Subject Subject { get; private set; }
    public ArbitrageDealStatuses Status { get; private set; }
    public ArbitrageDealResults? Result { get; private set; }
    public DateTime FillingDate { get; private set; }
    public DateTime? DecisionDate { get; private set; }

    public ArbitrageDeal(
        CaseNumber caseNumber,
        CourtName courtName,
        Instanse instance,
        Category category,
        PartyRole partyRole,
        CountrAgentName counterpartyName,
        Inn counterpartyInn,
        ClaimAmount claimAmount,
        Subject subject,
        ArbitrageDealStatuses status,
        ArbitrageDealResults? result,
        DateTime filingDate,
        DateTime? decisionDate
    )
    {
        CaseNumber = caseNumber ?? throw new InvalidPropertyException(nameof(caseNumber));
        CourtName = courtName ?? throw new InvalidPropertyException(nameof(courtName));
        Instanse = instance ?? throw new InvalidPropertyException(nameof(instance));
        Category = category ?? throw new InvalidPropertyException(nameof(category));
        PartyRole = partyRole ?? throw new InvalidPropertyException(nameof(partyRole));
        CountrAgentName = counterpartyName ?? throw new InvalidPropertyException(nameof(counterpartyName));
        CountrAgentInn = counterpartyInn ?? throw new InvalidPropertyException(nameof(counterpartyInn));
        ClaimAmount = claimAmount ?? throw new InvalidPropertyException(nameof(claimAmount));
        Subject = subject ?? throw  new InvalidPropertyException(nameof(subject));
        Guard.ValidateEnum(status);
        Status = status;
        ValidateResult(result);
        Result = result;

        Guard.ValidateDate(filingDate);
        FillingDate = filingDate;
        ValidateDecisionDate(decisionDate, filingDate, result, status);
        DecisionDate = decisionDate;
    }

    public void ChangeCaseNumber(CaseNumber caseNumber) =>
        CaseNumber = caseNumber ?? throw new InvalidPropertyException(nameof(caseNumber));

    public void ChangeCourtName(CourtName courtName) =>
        CourtName = courtName ?? throw new InvalidPropertyException(nameof(courtName));

    public void ChangeInstance(Instanse instance) =>
        Instanse = instance ?? throw new InvalidPropertyException(nameof(instance));

    public void ChangeCategory(Category category) =>
        Category = category ?? throw new InvalidPropertyException(nameof(category));

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

    public void Decide(ArbitrageDealResults result, DateTime decisionDate)
    {
        if (
            Status
            is not (ArbitrageDealStatuses.UnderConsideration or ArbitrageDealStatuses.Appealed)
        )
            throw new CannotChangeArbitrageDealStatusException();

        Guard.ValidateEnum(result);
        Guard.ValidateDate(decisionDate);

        if (decisionDate.Date < FillingDate.Date)
            throw new InvalidPropertyException(
                nameof(decisionDate),
                "дата решения не может быть раньше даты подачи"
            );

        if (DecisionDate is { } previous && decisionDate.Date < previous.Date)
            throw new InvalidPropertyException(
                nameof(decisionDate),
                "дата решения не может быть раньше даты предыдущего решения"
            );

        Result = result;
        DecisionDate = decisionDate;
        Status = ArbitrageDealStatuses.Decided;
    }

    public void Appeal()
    {
        if (Status != ArbitrageDealStatuses.Decided)
            throw new CannotChangeArbitrageDealStatusException();

        Status = ArbitrageDealStatuses.Appealed;
    }

    public void Complete()
    {
        if (Status is not (ArbitrageDealStatuses.Decided or ArbitrageDealStatuses.Appealed))
            throw new CannotChangeArbitrageDealStatusException();

        Status = ArbitrageDealStatuses.Completed;
    }

    private void ValidateResult(ArbitrageDealResults? result)
    {
        if (result is { } r && !Enum.IsDefined(r))
            throw new InvalidPropertyException(nameof(result), "неопределенное значение");
    }

    private void ValidateDecisionDate(
        DateTime? decisionDate,
        DateTime fillingDate,
        ArbitrageDealResults? result,
        ArbitrageDealStatuses actualStatus
    )
    {
        if (
            actualStatus is not (ArbitrageDealStatuses.Decided or ArbitrageDealStatuses.Completed)
            && (decisionDate.HasValue)
        )
            throw new InvalidPropertyException(
                nameof(decisionDate),
                "дата результата не может быть решения в целом"
            );

        if (decisionDate.HasValue != result.HasValue)
            throw new InvalidPropertyException(
                nameof(decisionDate),
                "дата результата не может быть без самого результата"
            );

        if (decisionDate is { } decision && decision.Date < fillingDate.Date)
            throw new InvalidPropertyException(
                nameof(decisionDate),
                "дата начала не может быть больше чем дата решения"
            );
    }
}
