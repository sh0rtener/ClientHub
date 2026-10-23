using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class ClientActionProfile : Profile
{
    public ClientActionProfile()
    {
        CreateMap<ClientAction, ClientActionDto>()
            .ForMember(x => x.Kind, opt => opt.MapFrom(x => (int)x.Kind));

        CreateMap<ClientActionDto, ClientAction>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("code", opt => opt.MapFrom(src => src.Code))
            .ForCtorParam("kind", opt => opt.MapFrom(src => (ClientActionType)src.Kind));
    }
}

public sealed class ClientActionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Kind { get; set; }
    public string Code { get; set; } = string.Empty;
}
