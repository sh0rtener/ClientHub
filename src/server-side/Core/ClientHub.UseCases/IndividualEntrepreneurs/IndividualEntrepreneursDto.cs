using AutoMapper;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.IndividualEntrepreneurs;
using ClientHub.UseCases.Shared.Dtos;

namespace ClientHub.UseCases.IndividualEntrepreneurs;

public sealed class IndividualEntrepreneursDto
{
    public int Id { get; set; }
    public string Inn { get; set; } = string.Empty;
    public string Name { get; set; }
    public string Ogrn { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Status { get; set; }
    public GoodsMarkDto GoodsMark { get; set; } = new();
    public DateTime? RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
    public List<ClientContactDto> ClientContacts { get; set; } = [];
    public List<ClientActionDto> ClientActions { get; set; } = [];
    public List<ClientEventDto> ClientEvents { get; set; } = [];
    public List<ArbitrageDealDto> ArbitrageDeals { get; set; } = [];
    public List<EnforcementProceedingsDto> Proceedings { get; set; } = [];
    public List<CourtsOfGeneralJurisdictionDto> Jurisdictions { get; set; } = [];
    public IndividualEntrepreneurRequisiteDto Requisite { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}

public sealed class IndividualEntrepreneursProfile : Profile
{
    public IndividualEntrepreneursProfile()
    {
        CreateMap<IndividualEntrepreneur, IndividualEntrepreneursDto>()
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Inn.Value))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Ogrn.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ClientContacts, opt => opt.MapFrom(src => src.Contacts))
            .ForMember(dest => dest.ClientActions, opt => opt.MapFrom(src => src.Actions))
            .ForMember(dest => dest.ClientEvents, opt => opt.MapFrom(src => src.Events))
            .ForMember(dest => dest.ArbitrageDeals, opt => opt.MapFrom(src => src.ArbitragesDeals))
            .ForMember(dest => dest.Proceedings, opt => opt.MapFrom(src => src.Proceedings))
            .ForMember(dest => dest.Jurisdictions, opt => opt.MapFrom(src => src.Jurisdictions))
            .ForMember(dest => dest.Requisite, opt => opt.MapFrom(src => src.Requisite));

        CreateMap<IndividualEntrepreneursDto, IndividualEntrepreneur>()
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
            .ForCtorParam("requisite", opt => opt.MapFrom(src => src.Requisite));
    }
}
