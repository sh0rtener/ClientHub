using AutoMapper;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.LegalEntities;
using ClientHub.UseCases.Shared.Dtos;

namespace ClientHub.UseCases.LegalEntities;

public sealed class LegalEntityProfile : Profile
{
    public LegalEntityProfile()
    {
        CreateMap<LegalEntity, LegalEntityDto>()
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Inn.Value))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Ogrn.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClientContacts, opt => opt.MapFrom(src => src.Contacts))
            .ForMember(dest => dest.ClientActions, opt => opt.MapFrom(src => src.Actions))
            .ForMember(dest => dest.ClientEvents, opt => opt.MapFrom(src => src.Events))
            .ForMember(dest => dest.ArbitrageDeals, opt => opt.MapFrom(src => src.ArbitragesDeals))
            .ForMember(dest => dest.Proceedings, opt => opt.MapFrom(src => src.Proceedings))
            .ForMember(dest => dest.Jurisdictions, opt => opt.MapFrom(src => src.Jurisdictions))
            .ForMember(dest => dest.FounderClients, opt => opt.MapFrom(src => src.FounderClients))
            .ForMember(dest => dest.FounderPersons, opt => opt.MapFrom(src => src.FounderPersons))
            .ForMember(dest => dest.Beneficiars, opt => opt.MapFrom(src => src.Beneficiars))
            .ForMember(dest => dest.Maintainers, opt => opt.MapFrom(src => src.Maintainers))
            .ForMember(dest => dest.Kpps, opt => opt.MapFrom(src => src.Kpps));

        CreateMap<LegalEntityDto, LegalEntity>()
            .ForCtorParam("inn", opt => opt.MapFrom(src => new Inn(src.Inn)))
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(
                "ogrn",
                opt => opt.MapFrom(src => new Ogrn(src.Ogrn, ClientType.IndividualEntrepreneurs))
            )
            .ForCtorParam("region", opt => opt.MapFrom(src => src.Region))
            .ForCtorParam("address", opt => opt.MapFrom(src => src.Address))
            .ForCtorParam(
                "status",
                opt => opt.MapFrom(src => ClientStatus.DefineStatus(src.Status))
            )
            .ForCtorParam(
                "goodsMark",
                opt =>
                    opt.MapFrom(src => new GoodsMark(
                        src.GoodsMark.Name,
                        src.GoodsMark.AssignedAt,
                        src.GoodsMark.DissasignedAt
                    ))
            )
            .ForCtorParam("registrationDate", opt => opt.MapFrom(src => new Inn(src.Inn)))
            .ForCtorParam("liquidationDate", opt => opt.MapFrom(src => new Inn(src.Inn)))
            .ForCtorParam("contacts", opt => opt.MapFrom(src => src.ClientContacts))
            .ForCtorParam("actions", opt => opt.MapFrom(src => src.ClientActions))
            .ForCtorParam("events", opt => opt.MapFrom(src => src.ClientEvents))
            .ForCtorParam("arbitragesDeals", opt => opt.MapFrom(src => src.ArbitrageDeals))
            .ForCtorParam("proceedings", opt => opt.MapFrom(src => src.Proceedings))
            .ForCtorParam("jurisdictions", opt => opt.MapFrom(src => src.Jurisdictions))
            .ForCtorParam("okatoCode", opt => opt.MapFrom(src => new Okato(src.Okato)))
            .ForCtorParam("clients", opt => opt.MapFrom(src => src.FounderClients))
            .ForCtorParam("persons", opt => opt.MapFrom(src => src.FounderPersons))
            .ForCtorParam("beneficiars", opt => opt.MapFrom(src => src.Beneficiars))
            .ForCtorParam("maintainers", opt => opt.MapFrom(src => src.Maintainers))
            .ForCtorParam("kpps", opt => opt.MapFrom(src => src.Kpps));
    }
}

public sealed class LegalEntityDto
{
    public int Id { get; set; }
    public string Inn { get; set; } = string.Empty;
    public string Name { get; set; }
    public string Ogrn { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Okato { get; set; } = string.Empty;
    public int HeadCount { get; set; }
    public GoodsMarkDto GoodsMark { get; set; } = new();
    public DateTime? RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
    public List<ClientContactDto> ClientContacts { get; set; } = [];
    public List<ClientActionDto> ClientActions { get; set; } = [];
    public List<ClientEventDto> ClientEvents { get; set; } = [];
    public List<ArbitrageDealDto> ArbitrageDeals { get; set; } = [];
    public List<EnforcementProceedingsDto> Proceedings { get; set; } = [];
    public List<CourtsOfGeneralJurisdictionDto> Jurisdictions { get; set; } = [];

    public List<FounderClientDto> FounderClients { get; set; } = [];
    public List<FounderPersonDto> FounderPersons { get; set; } = [];
    public List<BeneficiarDto> Beneficiars { get; set; } = [];
    public List<MaintainerDto> Maintainers { get; set; } = [];
    public List<KppDto> Kpps { get; set; } = [];

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
