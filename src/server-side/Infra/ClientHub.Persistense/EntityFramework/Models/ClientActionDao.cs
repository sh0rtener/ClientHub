using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class ClientActionProfile : Profile
{
    public ClientActionProfile()
    {
        CreateMap<ClientAction, ClientActionDao>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(x => x.Kind, opt => opt.MapFrom(x => (int)x.Kind));

        CreateMap<ClientActionDao, ClientAction>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => (ClientActionType)src.Kind));
    }
}

public sealed class ClientActionDao
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Kind { get; set; }
    public string Code { get; set; } = string.Empty;
}
