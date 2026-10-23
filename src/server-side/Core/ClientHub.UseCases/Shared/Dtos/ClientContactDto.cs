using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class ClientContactProfile : Profile
{
    public ClientContactProfile()
    {
        CreateMap<ClientContact, ClientContactDto>()
            .ForMember(x => x.ContactType, opt => opt.MapFrom(x => (int)x.ContactType));

        CreateMap<ClientContactDto, ClientContact>()
            .ForCtorParam("type", opt => opt.MapFrom(x => (ClientContactType)x.ContactType))
            .ForCtorParam("value", opt => opt.MapFrom(x => x.Value));
    }
}

public class ClientContactDto
{
    public int Id { get; set; }
    public int ContactType { get; set; }
    public string Value { get; set; } = string.Empty;
}
