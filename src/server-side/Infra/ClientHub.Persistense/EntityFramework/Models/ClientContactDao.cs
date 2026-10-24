using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class ClientContactProfile : Profile
{
    public ClientContactProfile()
    {
        CreateMap<ClientContact, ClientContactDao>()
            .ForMember(x => x.ContactType, opt => opt.MapFrom(x => (int)x.ContactType));

        CreateMap<ClientContactDao, ClientContact>()
            .ForMember(
                dest => dest.ContactType,
                opt => opt.MapFrom(src => (ClientContactType)src.ContactType)
            );
        ;
    }
}

public class ClientContactDao
{
    public int Id { get; set; }
    public int ContactType { get; set; }
    public string Value { get; set; } = string.Empty;
}
