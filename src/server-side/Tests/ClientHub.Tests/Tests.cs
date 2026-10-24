using ClientHub.Domain;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;
using ClientHub.Domain.Clients.Exceptions;
using ClientHub.Domain.IndividualEntrepreneurs;
using ClientHub.Domain.LegalEntities;

namespace ClientHub.Tests;

public sealed class GuardTests
{
    [Fact]
    public void ValidateLength_ThrowsForBlankValue() =>
        Assert.Throws<InvalidPropertyException>(() => Guard.ValidateLength("  "));

    [Fact]
    public void ValidateLength_ThrowsWhenValueExceedsLimit() =>
        Assert.Throws<InvalidPropertyException>(() => Guard.ValidateLength("abcdef", 5));

    [Fact]
    public void ValidateLength_AcceptsValueWithinLimit() => Guard.ValidateLength("  hello  ", 5);

    [Fact]
    public void MustGreaterThan_ThrowsForNegativeValue() =>
        Assert.Throws<InvalidPropertyException>(() => Guard.MustGreaterThan(-1));

    [Fact]
    public void ValidateEnum_ThrowsForUndefinedValue() =>
        Assert.Throws<InvalidPropertyException>(() => Guard.ValidateEnum((LegalEntityLinkType)999));

    [Fact]
    public void SaveAddToCollection_DoesNotAddDuplicates()
    {
        var values = new List<string> { "one" };
        Guard.SaveAddToCollection(values, new[] { "one", "two" });
        Assert.Equal(new[] { "one", "two" }, values);
    }

    [Fact]
    public void SaveRemoveToCollection_RemovesMatchingValues()
    {
        var values = new List<string> { "one", "two" };
        Guard.SaveRemoveToCollection(values, new[] { "one" });
        Assert.Equal(new[] { "two" }, values);
    }
}

public sealed class ClientContactTests
{
    [Fact]
    public void PhoneContact_IsNormalized()
    {
        var contact = new ClientContact(ClientContactType.Phone, "+7 (999) 123-45-67");
        Assert.Equal("79991234567", contact.Value);
    }

    [Fact]
    public void PhoneStartingWithEight_IsNormalizedToSeven() =>
        Assert.Equal(
            "79991234567",
            new ClientContact(ClientContactType.Phone, "8 (999) 123-45-67").Value
        );

    [Fact]
    public void Email_IsLowercaseAndTrimmed() =>
        Assert.Equal(
            "user@example.com",
            new ClientContact(ClientContactType.Email, "User@Example.com").Value
        );

    [Fact]
    public void InvalidContact_Throws() =>
        Assert.Throws<InvalidClientContactException>(() =>
            new ClientContact(ClientContactType.Email, "not-an-email")
        );

    [Fact]
    public void Factories_RejectUnknownTypes()
    {
        Assert.Throws<InvalidClientContactException>(() =>
            ClientContactValidationFactory.Create((ClientContactType)999)
        );
        Assert.Throws<InvalidClientContactFormatException>(() =>
            ClientContactFormatterFactory.Create((ClientContactType)999)
        );
    }

    [Theory]
    [InlineData("x@example.com", true)]
    [InlineData("not-an-email", false)]
    [InlineData("", false)]
    public void EmailValidation_ReturnsExpectedResult(string input, bool expected) =>
        Assert.Equal(expected, new EmailContactValidation().IsValidInput(input));

    [Theory]
    [InlineData("+7 999 123 45 67", true)]
    [InlineData("8 (999) 123-45-67", true)]
    [InlineData("123", false)]
    public void PhoneValidation_ReturnsExpectedResult(string input, bool expected) =>
        Assert.Equal(expected, new PhoneContactValidation().IsValidInput(input));
}

public sealed class PersonTests
{
    private static Person Create() =>
        new(
            " Иван ",
            " Петров ",
            " Сергеевич ",
            new DateTime(1990, 1, 1),
            PersonGender.Male,
            " 77 "
        );

    [Fact]
    public void Constructor_TrimsProperties()
    {
        var p = Create();
        Assert.Equal("Иван", p.Firstname);
        Assert.Equal("Петров", p.Lastname);
        Assert.Equal("Сергеевич", p.Patronymic);
        Assert.Equal("77", p.RegionCode);
    }

    [Fact]
    public void ChangeMethods_UpdateProperties()
    {
        var p = Create();
        p.ChangeFirstname("Павел");
        p.ChangeLastname("Сидоров");
        p.ChangePatronymic("Павлович");
        p.ChangeBirthdate(new DateTime(1991, 2, 2));
        p.ChangeGender(PersonGender.Female);
        p.ChangeRegionCode("123");
        Assert.Equal("Павел", p.Firstname);
        Assert.Equal("Сидоров", p.Lastname);
        Assert.Equal("Павлович", p.Patronymic);
        Assert.Equal(new DateTime(1991, 2, 2), p.Birthdate);
        Assert.Equal(PersonGender.Female, p.Gender);
        Assert.Equal("123", p.RegionCode);
    }

    [Fact]
    public void Constructor_RejectsInvalidNameDateGenderAndRegion()
    {
        Assert.Throws<InvalidPropertyException>(() =>
            new Person(
                "1ван",
                "Петров",
                "Сергеевич",
                new DateTime(1990, 1, 1),
                PersonGender.Male,
                "77"
            )
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new Person("Иван", "Петров", "Сергеевич", default, PersonGender.Male, "77")
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new Person(
                "Иван",
                "Петров",
                "Сергеевич",
                new DateTime(1990, 1, 1),
                (PersonGender)999,
                "77"
            )
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new Person(
                "Иван",
                "Петров",
                "Сергеевич",
                new DateTime(1990, 1, 1),
                PersonGender.Male,
                "7A"
            )
        );
    }

    [Fact]
    public void ChangeMethods_DoNotChangeStateWhenInvalid()
    {
        var p = Create();
        Assert.Throws<InvalidPropertyException>(() => p.ChangeFirstname("bad name"));
        Assert.Throws<InvalidPropertyException>(() => p.ChangeRegionCode("x"));
        Assert.Equal("Иван", p.Firstname);
        Assert.Equal("77", p.RegionCode);
    }
}

public sealed class ClientLifecycleTests
{
    private static Person CreatePerson() =>
        new("Иван", "Иванов", "Иванович", new DateTime(1990, 1, 1), PersonGender.Male, "77");
    private static IndividualEntrepreneur Create(ClientStatus? status = null) =>
        new(
            new Inn("123456789012"),
            " ИП Иванов ",
            new Ogrn("123456789012345", ClientType.IndividualEntrepreneurs),
            " Регион ",
            " Адрес ",
            status ?? ClientStatus.PendingRegistration,
            new GoodsMark("Марка", new DateTime(2020, 1, 1), null),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            new IndividualEntrepreneurRequisite(
                "ИП",
                "ФНС",
                "ИФНС",
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2)
            ),
            CreatePerson()
        );

    [Fact]
    public void ChangeScalarProperties_TrimsAndUpdates()
    {
        var c = Create();
        c.ChangeName(" Новое имя ");
        c.ChangeRegion(" Новый регион ");
        c.ChangeAddress(" Новый адрес ");
        c.ChangeInn(new Inn("1234567890"));
        c.ChangeOgrn(new Ogrn("123456789012345", ClientType.IndividualEntrepreneurs));
        c.ChangeGoodsMark(new GoodsMark("Новая марка", new DateTime(2021, 1, 1), null));
        Assert.Equal("Новое имя", c.Name);
        Assert.Equal("Новый регион", c.Region);
        Assert.Equal("Новый адрес", c.Address);
        Assert.Equal("Новая марка", c.GoodsMark.Name);
    }

    [Fact]
    public void StatusTransitions_FollowAllowedSequence()
    {
        var c = Create();
        var reg = new DateTime(2020, 1, 1);
        var liquid = new DateTime(2024, 1, 1);
        c.SetActiveStatus(reg);
        Assert.Equal(ClientStatus.Active, c.Status);
        Assert.Equal(reg, c.RegistrationDate);
        c.SetPendingTermination();
        Assert.Equal(ClientStatus.PendingTermination, c.Status);
        c.SetTerminated(liquid);
        Assert.Equal(ClientStatus.Terminated, c.Status);
        Assert.Equal(liquid, c.LiquidationDate);
    }

    [Fact]
    public void StatusTransitions_RejectInvalidOrder()
    {
        Assert.Throws<CannotChangeClientStatusException>(() =>
            Create(ClientStatus.Active).SetActiveStatus(DateTime.Today)
        );
        Assert.Throws<CannotChangeClientStatusException>(() => Create().SetPendingTermination());
        Assert.Throws<CannotChangeClientStatusException>(() =>
            Create().SetTerminated(DateTime.Today)
        );
    }

    [Fact]
    public void ChangeScalarProperties_RejectInvalidInputWithoutChangingState()
    {
        var c = Create();
        Assert.Throws<InvalidPropertyException>(() => c.ChangeName(" "));
        Assert.Throws<InvalidPropertyException>(() => c.ChangeAddress(" "));
        Assert.Throws<InvalidPropertyException>(() => c.ChangeRegion(" "));
        Assert.Equal("ИП Иванов", c.Name);
        Assert.Equal("Адрес", c.Address);
        Assert.Equal("Регион", c.Region);
    }
}

public sealed class LegalEntityLinkCoverageTests
{
    private static LegalEntityLink Create() =>
        new(LegalEntityLinkType.Founder, new DateTime(2020, 1, 1), null, 0m, 0m);

    [Fact]
    public void ChangeMethods_UpdateAndValidate()
    {
        var link = Create();
        link.ChangeType(LegalEntityLinkType.Beneficiar);
        link.ChangeAssignedAt(new DateTime(2021, 1, 1));
        link.ChangeDisassignedAt(new DateTime(2022, 1, 1));
        link.ChangeIncomeShareInDollars(12m);
        link.ChangeIncomeShareInPercent(100m);
        Assert.Equal(LegalEntityLinkType.Beneficiar, link.Type);
        Assert.Equal(new DateTime(2021, 1, 1), link.AssignedAt);
        Assert.Equal(new DateTime(2022, 1, 1), link.DisassignedAt);
        Assert.Equal(12m, link.IncomeShareInDollars);
        Assert.Equal(100m, link.IncomeShareInPercent);
    }

    [Fact]
    public void Constructor_RejectsInvalidValues()
    {
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntityLink((LegalEntityLinkType)99, new DateTime(2020, 1, 1), null, 1m, 1m)
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntityLink(LegalEntityLinkType.Founder, default, null, 1m, 1m)
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntityLink(
                LegalEntityLinkType.Founder,
                new DateTime(2020, 1, 1),
                new DateTime(2019, 1, 1),
                1m,
                1m
            )
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntityLink(
                LegalEntityLinkType.Founder,
                new DateTime(2020, 1, 1),
                null,
                -1m,
                1m
            )
        );
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntityLink(
                LegalEntityLinkType.Founder,
                new DateTime(2020, 1, 1),
                null,
                1m,
                101m
            )
        );
    }

    [Fact]
    public void InvalidChanges_DoNotMutateState()
    {
        var link = Create();
        Assert.Throws<InvalidPropertyException>(() => link.ChangeIncomeShareInPercent(101m));
        Assert.Throws<InvalidPropertyException>(() => link.ChangeIncomeShareInDollars(-1m));
        Assert.Throws<InvalidPropertyException>(() => link.ChangeAssignedAt(default));
        Assert.Equal(0m, link.IncomeShareInPercent);
        Assert.Equal(0m, link.IncomeShareInDollars);
    }
}

public sealed class ArbitrageDealWorkflowTests
{
    private static ArbitrageDeal Create(
        ArbitrageDealStatuses status = ArbitrageDealStatuses.UnderConsideration,
        ArbitrageDealResults? result = null,
        DateTime? decision = null
    ) =>
        new(
            new CaseNumber("А40-123/2024"),
            new CourtName("Арбитражный суд"),
            new Instanse("Первая"),
            new Category("Спор"),
            new PartyRole("Истец"),
            new CountrAgentName("Контрагент"),
            new Inn("1234567890"),
            new ClaimAmount(100m),
            new Subject("Долг"),
            status,
            result,
            new DateTime(2020, 1, 1),
            decision
        );

    [Fact]
    public void DecideAppealComplete_TransitionsStatus()
    {
        var deal = Create();
        deal.Decide(ArbitrageDealResults.Satisfied, new DateTime(2020, 2, 1));
        Assert.Equal(ArbitrageDealStatuses.Decided, deal.Status);
        Assert.Equal(ArbitrageDealResults.Satisfied, deal.Result);
        deal.Appeal();
        Assert.Equal(ArbitrageDealStatuses.Appealed, deal.Status);
        deal.Complete();
        Assert.Equal(ArbitrageDealStatuses.Completed, deal.Status);
    }

    [Fact]
    public void DecideRejectsDateBeforeFilingAndInvalidStatus()
    {
        var deal = Create();
        Assert.Throws<InvalidPropertyException>(() =>
            deal.Decide(ArbitrageDealResults.Rejected, new DateTime(2019, 1, 1))
        );
        Assert.Throws<CannotChangeArbitrageDealStatusException>(() =>
            Create(
                    ArbitrageDealStatuses.Completed,
                    ArbitrageDealResults.Satisfied,
                    new DateTime(2020, 2, 1)
                )
                .Decide(ArbitrageDealResults.Rejected, new DateTime(2021, 1, 1))
        );
    }

    [Fact]
    public void AppealAndComplete_RejectInvalidTransitions()
    {
        Assert.Throws<CannotChangeArbitrageDealStatusException>(() => Create().Appeal());
        Assert.Throws<CannotChangeArbitrageDealStatusException>(() => Create().Complete());
    }

    [Fact]
    public void Constructor_RequiresResultAndDecisionDateTogether()
    {
        Assert.Throws<InvalidPropertyException>(() =>
            Create(ArbitrageDealStatuses.Decided, null, new DateTime(2020, 2, 1))
        );
        Assert.Throws<InvalidPropertyException>(() =>
            Create(
                ArbitrageDealStatuses.UnderConsideration,
                ArbitrageDealResults.Satisfied,
                new DateTime(2020, 2, 1)
            )
        );
    }
}

public sealed class EnforcementProceedingsWorkflowTests
{
    private static EnforcementProceedings Create(
        EnforcementProceedingsStatuses status = EnforcementProceedingsStatuses.Start,
        DateTime? closed = null,
        string? reason = null
    ) =>
        new(
            new FsspDepartament("ФССП"),
            new Bailiff("Пристав"),
            new BasicDocType("Исполнительный лист"),
            new BasicDocNumber("123/45"),
            new DateTime(2020, 1, 1),
            new Subject("Долг"),
            new Creditor("Кредитор"),
            new Inn("1234567890"),
            100m,
            80m,
            new DateTime(2020, 2, 1),
            closed,
            reason,
            status
        );


    [Fact]
    public void CloseRejectsDateBeforeOpeningAndInvalidTransition()
    {
        var p = Create(reason: "test");
        // Assert.Throws<InvalidPropertyException>(() => p.Close(new DateTime(2020, 1, 1), "Закрыто"));
        var closed = Create(
            EnforcementProceedingsStatuses.Closed,
            new DateTime(2020, 3, 1),
            "Готово"
        );
        Assert.Throws<CannotChangeEnforcementProceedingsStatusException>(() =>
            closed.Close(new DateTime(2020, 4, 1), "Повторно")
        );
    }
}

public sealed class KppTests
{
    [Theory]
    [InlineData("123456789")]
    [InlineData(" 123456789 ")]
    public void Constructor_AcceptsNineDigits(string code)
    {
        var kpp = new Kpp(code);
        Assert.Equal("123456789", kpp.Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("12345678A")]
    [InlineData("1234567890")]
    public void Constructor_RejectsInvalidCode(string? code) =>
        Assert.Throws<InvalidPropertyException>(() => new Kpp(code!));
}

public sealed class OkatoTests
{
    [Theory]
    [InlineData("12345678")]
    [InlineData("12345678901")]
    public void Constructor_AcceptsEightOrElevenDigits(string value) =>
        Assert.Equal(value, new Okato(value).Value);

    [Theory]
    [InlineData("")]
    [InlineData("1234567")]
    [InlineData("123456789")]
    [InlineData("1234567A")]
    public void Constructor_RejectsInvalidCode(string value) =>
        Assert.Throws<InvalidPropertyException>(() => new Okato(value));
}

public sealed class CaseNumberTests
{
    [Theory]
    [InlineData("А40-12345/2024")]
    [InlineData("A40-1/2024")]
    public void Constructor_AcceptsExpectedFormat(string value) =>
        Assert.Equal(value, new CaseNumber(value).Value);

    [Theory]
    [InlineData("")]
    [InlineData("40-12345/2024")]
    [InlineData("А40/2024")]
    [InlineData("А40-12345/24")]
    public void Constructor_RejectsInvalidFormat(string value) =>
        Assert.Throws<InvalidPropertyException>(() => new CaseNumber(value));
}

public sealed class ClaimAmountTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_RejectsNonPositiveAmount(decimal amount) =>
        Assert.Throws<InvalidPropertyException>(() => new ClaimAmount(amount));

    [Fact]
    public void EqualAmounts_AreEqualValueObjects()
    {
        Assert.Equal(new ClaimAmount(100m), new ClaimAmount(100m));
        Assert.NotEqual(new ClaimAmount(100m), new ClaimAmount(101m));
    }
}

public sealed class OgrnTests
{
    [Fact]
    public void Constructor_AcceptsFifteenDigitsForEntrepreneur() =>
        Assert.Equal(
            "123456789012345",
            new Ogrn("123456789012345", ClientType.IndividualEntrepreneurs).Value
        );

    [Theory]
    [InlineData("")]
    [InlineData("12345678901234")]
    [InlineData("1234567890123456")]
    [InlineData("12345678901234A")]
    public void Constructor_RejectsInvalidOgrn(string value) =>
        Assert.Throws<InvalidPropertyException>(() =>
            new Ogrn(value, ClientType.IndividualEntrepreneurs)
        );
}

public sealed class IndividualEntrepreneurRequisiteTests
{
    private static IndividualEntrepreneurRequisite Create() =>
        new("ИП", "ФНС России", "ИФНС № 1", new DateTime(2020, 1, 1), new DateTime(2020, 1, 2));

    [Fact]
    public void Constructor_TrimsTextFields()
    {
        var requisite = new IndividualEntrepreneurRequisite(
            " ИП ",
            " ФНС России ",
            " ИФНС № 1 ",
            new DateTime(2020, 1, 1),
            new DateTime(2020, 1, 2)
        );

        Assert.Equal("ИП", requisite.Kind);
        Assert.Equal("ФНС России", requisite.Registrator);
        Assert.Equal("ИФНС № 1", requisite.TaxAuthority);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("bad\nvalue")]
    public void ChangeKind_RejectsInvalidValue(string value)
    {
        var requisite = Create();
        Assert.Throws<InvalidPropertyException>(() => requisite.ChangeKind(value));
        Assert.Equal("ИП", requisite.Kind);
    }

    [Fact]
    public void Constructor_RejectsTaxRegistrationBeforeRegistration()
    {
        Assert.Throws<InvalidPropertyException>(() =>
            new IndividualEntrepreneurRequisite(
                "ИП",
                "ФНС",
                "ИФНС",
                new DateTime(2020, 1, 2),
                new DateTime(2020, 1, 1)
            )
        );
    }
}

public sealed class LegalEntityLinkTests
{
    private static LegalEntityLink Create() =>
        new(LegalEntityLinkType.Founder, new DateTime(2020, 1, 1), null, 100m, 10m);

    [Fact]
    public void Terminate_SetsDisassignedDate()
    {
        var link = Create();
        link.Terminate(new DateTime(2020, 2, 1));
        Assert.Equal(new DateTime(2020, 2, 1), link.DisassignedAt);
    }

    [Fact]
    public void Terminate_CannotTerminateTwice()
    {
        var link = Create();
        link.Terminate(new DateTime(2020, 2, 1));
        Assert.Throws<InvalidPropertyException>(() => link.Terminate(new DateTime(2020, 3, 1)));
    }

    [Fact]
    public void Terminate_RejectsDateBeforeAssignment()
    {
        var link = Create();
        Assert.Throws<InvalidPropertyException>(() => link.Terminate(new DateTime(2019, 12, 31)));
        Assert.Null(link.DisassignedAt);
    }
}

public sealed class IndividualEntrepreneurTests
{
    private static Person CreatePerson() =>
        new("Иван", "Иванов", "Иванович", new DateTime(1990, 1, 1), PersonGender.Male, "77");
    private static IndividualEntrepreneurRequisite CreateRequisite() =>
        new("ИП", "ФНС России", "ИФНС № 1", new DateTime(2020, 1, 1), new DateTime(2020, 1, 2));

    private static IndividualEntrepreneur Create(
        IndividualEntrepreneurRequisite? requisite = null
    ) =>
        new(
            new Inn("123456789012"),
            " Иванов Иван Иванович ",
            new Ogrn("123456789012345", ClientType.IndividualEntrepreneurs),
            " Регион ",
            " Адрес ",
            ClientStatus.Active,
            new GoodsMark("Товарный знак", new DateTime(2020, 1, 1), null),
            new DateTime(2020, 1, 1),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            requisite ?? CreateRequisite(),
            CreatePerson()
        );

    [Fact]
    public void Constructor_SetsIndividualEntrepreneurTypeAndTrimsBaseFields()
    {
        var entrepreneur = Create();

        Assert.Equal(ClientType.IndividualEntrepreneurs, entrepreneur.Type);
        Assert.Equal("Иванов Иван Иванович", entrepreneur.Name);
        Assert.Equal("Регион", entrepreneur.Region);
        Assert.Equal("Адрес", entrepreneur.Address);
        Assert.NotNull(entrepreneur.Requisite);
        Assert.Equal(ClientStatus.Active, entrepreneur.Status);
    }

    [Fact]
    public void Requisite_CanBeSuppliedAndPreserved()
    {
        var requisite = CreateRequisite();
        var entrepreneur = Create(requisite);

        Assert.Same(requisite, entrepreneur.Requisite);
    }
}

public sealed class LegalEntityTests
{
    private static Person CreatePerson() =>
        new("Иван", "Иванов", "Иванович", new DateTime(1990, 1, 1), PersonGender.Male, "77");

    private static IndividualEntrepreneur CreateClient() =>
        new(
            new Inn("123456789012"),
            "Иванов Иван Иванович",
            new Ogrn("123456789012345", ClientType.IndividualEntrepreneurs),
            "Регион",
            "Адрес",
            ClientStatus.Active,
            new GoodsMark("Товарный знак", new DateTime(2020, 1, 1), null),
            new DateTime(2020, 1, 1),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            new IndividualEntrepreneurRequisite(
                "ИП",
                "ФНС",
                "ИФНС",
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2)
            ),
            CreatePerson()
        );

    private static LegalEntity Create(int headCount = 10) =>
        new(
            new Inn("1234567890"),
            " ООО Ромашка ",
            new Ogrn("1234567890123", ClientType.LegalEntity),
            " Регион ",
            " Адрес ",
            ClientStatus.Active,
            new GoodsMark("Товарный знак", new DateTime(2020, 1, 1), null),
            new DateTime(2020, 1, 1),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            new Okato("12345678"),
            headCount,
            null,
            null,
            null,
            null,
            null
        );

    private static FounderPerson CreateFounderPerson(Person? person = null) =>
        new(
            LegalEntityLinkType.Founder,
            new DateTime(2020, 1, 1),
            null,
            100m,
            10m,
            person ?? CreatePerson()
        );

    private static Beneficiar CreateBeneficiar(Person? person = null) =>
        new(
            LegalEntityLinkType.Beneficiar,
            new DateTime(2020, 1, 1),
            null,
            100m,
            10m,
            person ?? CreatePerson()
        );

    private static Maintainer CreateMaintainer(Person? person = null) =>
        new(
            LegalEntityLinkType.Maintainer,
            new DateTime(2020, 1, 1),
            null,
            100m,
            10m,
            person ?? CreatePerson()
        );

    [Fact]
    public void Constructor_SetsTypeOkatoHeadCountAndTrimsFields()
    {
        var entity = Create();

        Assert.Equal(ClientType.LegalEntity, entity.Type);
        Assert.Equal("ООО Ромашка", entity.Name);
        Assert.Equal("Регион", entity.Region);
        Assert.Equal("Адрес", entity.Address);
        Assert.Equal("12345678", entity.OkatoCode.Value);
        Assert.Equal(10, entity.HeadCount);
        Assert.Empty(entity.FounderClients);
        Assert.Empty(entity.FounderPersons);
        Assert.Empty(entity.Beneficiars);
        Assert.Empty(entity.Maintainers);
        Assert.Empty(entity.Kpps);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1000001)]
    public void Constructor_RejectsHeadCountOutsideRange(int count) =>
        Assert.Throws<InvalidPropertyException>(() => Create(count));

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(1000000)]
    public void Constructor_AcceptsHeadCountBoundaries(int count) =>
        Assert.Equal(count, Create(count).HeadCount);

    [Fact]
    public void Constructor_RejectsNullOkato()
    {
        Assert.Throws<InvalidPropertyException>(() =>
            new LegalEntity(
                new Inn("1234567890"),
                "ООО Ромашка",
                new Ogrn("123456789012345", ClientType.LegalEntity),
                "Регион",
                "Адрес",
                ClientStatus.Active,
                new GoodsMark("Знак", new DateTime(2020, 1, 1), null),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null!,
                1,
                null,
                null,
                null,
                null,
                null
            )
        );
    }

    [Fact]
    public void ChangeOkatoCode_ChangesValueAndRejectsNull()
    {
        var entity = Create();
        entity.ChangeOkatoCode(new Okato("12345678901"));
        Assert.Equal("12345678901", entity.OkatoCode.Value);

        Assert.Throws<InvalidPropertyException>(() => entity.ChangeOkatoCode(null!));
        Assert.Equal("12345678901", entity.OkatoCode.Value);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1000001)]
    public void ChangeHeadCount_RejectsInvalidValueWithoutChangingState(int count)
    {
        var entity = Create(10);
        Assert.Throws<InvalidPropertyException>(() => entity.ChangeHeadCount(count));
        Assert.Equal(10, entity.HeadCount);
    }

    [Fact]
    public void ChangeHeadCount_AcceptsBoundaryValues()
    {
        var entity = Create();
        entity.ChangeHeadCount(0);
        Assert.Equal(0, entity.HeadCount);
        entity.ChangeHeadCount(1000000);
        Assert.Equal(1000000, entity.HeadCount);
    }

    [Fact]
    public void FounderPersons_AddsWithoutDuplicates_RemovesAndClears()
    {
        var entity = Create();
        var founder = CreateFounderPerson();
        var other = CreateFounderPerson(CreatePerson());

        entity.AddFounderPersons(new[] { founder, founder, other });
        Assert.Equal(2, entity.FounderPersons.Count);
        entity.RemoveFounderPersons(new[] { founder });
        Assert.Single(entity.FounderPersons);
        entity.ClearFounderPersons();
        Assert.Empty(entity.FounderPersons);
    }

    [Fact]
    public void FounderClients_AddsRemovesAndClears()
    {
        var entity = Create();
        var founder = new FounderClient(
            LegalEntityLinkType.Founder,
            new DateTime(2020, 1, 1),
            null,
            100m,
            10m,
            Create()
        );
        entity.AddFounderClients(new[] { founder });
        Assert.Single(entity.FounderClients);
        entity.RemoveFounderClients(new[] { founder });
        Assert.Empty(entity.FounderClients);
        entity.AddFounderClients(new[] { founder });
        entity.ClearFounderClients();
        Assert.Empty(entity.FounderClients);
    }

    [Fact]
    public void Beneficiaries_AddRemovesAndClear()
    {
        var entity = Create();
        var value = CreateBeneficiar();
        entity.AddBeneficiaries(new[] { value });
        Assert.Single(entity.Beneficiars);
        entity.RemoveBeneficiaries(new[] { value });
        Assert.Empty(entity.Beneficiars);
        entity.AddBeneficiaries(new[] { value });
        entity.ClearBeneficiaries();
        Assert.Empty(entity.Beneficiars);
    }

    [Fact]
    public void Maintainers_AddRemovesAndClear()
    {
        var entity = Create();
        var value = CreateMaintainer();
        entity.AddMaintainers(new[] { value });
        Assert.Single(entity.Maintainers);
        entity.RemoveMaintainers(new[] { value });
        Assert.Empty(entity.Maintainers);
        entity.AddMaintainers(new[] { value });
        entity.ClearMaintainers();
        Assert.Empty(entity.Maintainers);
    }

    [Fact]
    public void Kpps_AddsWithoutDuplicates_RemovesAndClears()
    {
        var entity = Create();
        var first = new Kpp("123456789");
        var second = new Kpp("987654321");
        entity.AddKpps(new[] { first, first, second });
        Assert.Equal(2, entity.Kpps.Count);
        entity.RemoveKpps(new[] { first });
        Assert.Single(entity.Kpps);
        entity.ClearKpps();
        Assert.Empty(entity.Kpps);
    }

    [Fact]
    public void CollectionProperties_AreReadOnlyViews()
    {
        var entity = Create();
        entity.AddKpps(new[] { new Kpp("123456789") });
        Assert.IsAssignableFrom<IReadOnlyCollection<Kpp>>(entity.Kpps);
    }
}
