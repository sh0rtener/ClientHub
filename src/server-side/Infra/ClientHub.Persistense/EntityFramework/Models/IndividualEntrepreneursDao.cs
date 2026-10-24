using AutoMapper;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;
using ClientHub.Domain.IndividualEntrepreneurs;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class IndividualEntrepreneursDao
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public ClientDao Client { get; set; }
    public Guid RequisiteId { get; set; }
    public IndividualEntrepreneurRequisiteDao Requisite { get; set; }
    public Guid PersonId { get; set; }
    public PersonDao Person { get; set; }

    public static IndividualEntrepreneur ToDomain(IndividualEntrepreneursDao dao, IMapper mapper)
    {
        var client = dao.Client;

        return new IndividualEntrepreneur(
            inn: mapper.Map<Inn>(client.Inn),
            name: client.Name,
            ogrn: new Ogrn(client.Ogrn, ClientType.IndividualEntrepreneurs),
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
            requisite: mapper.Map<IndividualEntrepreneurRequisite>(dao.Requisite),
            person: mapper.Map<Person>(dao.Person)
        )
        {
            Id = dao.Id,
        };
    }
}

public sealed class IndividualEntrepreneursProfile : Profile
{
    public IndividualEntrepreneursProfile()
    {
        CreateMap<IndividualEntrepreneur, IndividualEntrepreneursDao>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ClientId, opt => opt.Ignore())
            .ForMember(x => x.RequisiteId, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Requisite, opt => opt.MapFrom(src => src.Requisite));

        CreateMap<IndividualEntrepreneur, ClientDao>()
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
