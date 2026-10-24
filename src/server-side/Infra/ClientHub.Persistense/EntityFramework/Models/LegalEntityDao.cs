using AutoMapper;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;
using ClientHub.Domain.IndividualEntrepreneurs;
using ClientHub.Domain.LegalEntities;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class LegalEntityDao
{
    public int Id { get; set; }
    public string Okato { get; set; } = string.Empty;
    public int HeadCount { get; set; }
    public List<FounderClientDao> FounderClients { get; set; } = [];
    public List<FounderPersonDao> FounderPersons { get; set; } = [];
    public List<BeneficiarDao> Beneficiars { get; set; } = [];
    public List<MaintainerDao> Maintainers { get; set; } = [];
    public List<KppDao> Kpps { get; set; } = [];

    public int ClientId { get; set; }
    public ClientDao Client { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public static LegalEntity ToDomain(LegalEntityDao dao, IMapper mapper)
    {
        var client = dao.Client;

        return new LegalEntity(
            inn: mapper.Map<Inn>(client.Inn),
            name: client.Name,
            ogrn: new Ogrn(client.Ogrn, ClientType.LegalEntity),
            region: client.Region,
            address: client.Address,
            status: mapper.Map<ClientStatus>(client.Status),
            goodsMark: mapper.Map<GoodsMark>(client.GoodsMark),
            registrationDate: client.RegistrationDate,
            liquidationDate: client.LiquidationDate,
            contacts: mapper.Map<List<ClientContact>>(client.ClientContacts),
            actions: mapper.Map<List<ClientAction>>(client.ClientActions),
            events: mapper.Map<List<ClientEvent>>(client.ClientEvents),
            arbitragesDeals: mapper.Map<List<ArbitrageDeal>>(client.ArbitrageDeals),
            proceedings: mapper.Map<List<EnforcementProceedings>>(client.Proceedings),
            jurisdictions: mapper.Map<List<CourtsOfGeneralJurisdiction>>(client.Jurisdictions),
            okatoCode: new Okato(dao.Okato),
            headCount: dao.HeadCount,
            clients: mapper.Map<List<FounderClient>>(dao.FounderClients),
            persons: mapper.Map<List<FounderPerson>>(dao.FounderPersons),
            beneficiars: mapper.Map<List<Beneficiar>>(dao.Beneficiars),
            maintainers: mapper.Map<List<Maintainer>>(dao.Maintainers),
            kpps: mapper.Map<List<Kpp>>(dao.Kpps)
        )
        {
            Id = dao.Id,
        };
    }
}

public sealed class LegalEntityProfile : Profile
{
    public LegalEntityProfile()
    {
        CreateMap<LegalEntity, LegalEntityDao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ClientId, opt => opt.Ignore())
            .ForMember(x => x.Okato, opt => opt.MapFrom(src => src.OkatoCode.Value))
            .ForMember(x => x.HeadCount, opt => opt.MapFrom(src => src.HeadCount))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src));

        CreateMap<LegalEntity, ClientDao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Inn.Value))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Ogrn.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Identifier))
            .ForMember(dest => dest.GoodsMark, opt => opt.MapFrom(src => src.GoodsMark))
            .ForMember(dest => dest.ClientContacts, opt => opt.MapFrom(src => src.Contacts))
            .ForMember(dest => dest.ClientActions, opt => opt.MapFrom(src => src.Actions))
            .ForMember(dest => dest.ClientEvents, opt => opt.MapFrom(src => src.Events))
            .ForMember(dest => dest.ArbitrageDeals, opt => opt.MapFrom(src => src.ArbitragesDeals))
            .ForMember(dest => dest.Proceedings, opt => opt.MapFrom(src => src.Proceedings))
            .ForMember(dest => dest.Jurisdictions, opt => opt.MapFrom(src => src.Jurisdictions));
    }
}
