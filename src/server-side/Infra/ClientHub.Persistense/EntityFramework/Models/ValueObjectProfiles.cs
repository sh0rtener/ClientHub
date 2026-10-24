using AutoMapper;
using ClientHub.Domain.Clients;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class ValueObjectProfiles : Profile
{
    public ValueObjectProfiles()
    {
        CreateMap<Inn, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Inn>().ConvertUsing(x => new Inn(x));

        CreateMap<Ogrn, string>().ConvertUsing(x => x.Value);

        CreateMap<Okato, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Okato>().ConvertUsing(x => new Okato(x));

        CreateMap<CaseNumber, string>().ConvertUsing(x => x.Value);
        CreateMap<string, CaseNumber>().ConvertUsing(x => new CaseNumber(x));

        CreateMap<CourtName, string>().ConvertUsing(x => x.Value);
        CreateMap<string, CourtName>().ConvertUsing(x => new CourtName(x));

        CreateMap<Instanse, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Instanse>().ConvertUsing(x => new Instanse(x));

        CreateMap<PartyRole, string>().ConvertUsing(x => x.Value);
        CreateMap<string, PartyRole>().ConvertUsing(x => new PartyRole(x));

        CreateMap<CountrAgentName, string>().ConvertUsing(x => x.Value);
        CreateMap<string, CountrAgentName>().ConvertUsing(x => new CountrAgentName(x));

        CreateMap<Subject, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Subject>().ConvertUsing(x => new Subject(x));

        CreateMap<Category, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Category>().ConvertUsing(x => new Category(x));

        CreateMap<CaseType, string>().ConvertUsing(x => x.Value);
        CreateMap<string, CaseType>().ConvertUsing(x => new CaseType(x));

        CreateMap<FsspDepartament, string>().ConvertUsing(x => x.Value);
        CreateMap<string, FsspDepartament>().ConvertUsing(x => new FsspDepartament(x));

        CreateMap<Bailiff, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Bailiff>().ConvertUsing(x => new Bailiff(x));

        CreateMap<BasicDocType, string>().ConvertUsing(x => x.Value);
        CreateMap<string, BasicDocType>().ConvertUsing(x => new BasicDocType(x));

        CreateMap<BasicDocNumber, string>().ConvertUsing(x => x.Value);
        CreateMap<string, BasicDocNumber>().ConvertUsing(x => new BasicDocNumber(x));

        CreateMap<Creditor, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Creditor>().ConvertUsing(x => new Creditor(x));
        
        CreateMap<Okato, string>().ConvertUsing(x => x.Value);
        CreateMap<string, Okato>().ConvertUsing(x => new Okato(x));

        CreateMap<int, ClientStatus>().ConvertUsing(x => ClientStatus.DefineStatus(x));

        CreateMap<ClientType, int>().ConvertUsing(x => x.Identifier);

        CreateMap<int, ClientType>()
            .ConvertUsing(
                (source, _) =>
                {
                    switch (source)
                    {
                        case 1:
                            return ClientType.IndividualEntrepreneurs;
                        case 2:
                            return ClientType.LegalEntity;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(source));
                    }
                }
            );

        CreateMap<ClaimAmount, decimal>().ConvertUsing(x => x.Value);

        CreateMap<decimal, ClaimAmount>().ConvertUsing(x => new ClaimAmount(x));
    }
}
