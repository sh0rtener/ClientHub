using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class ClientEventProfile : Profile
{
    public ClientEventProfile()
    {
        CreateMap<ClientEvent, ClientEventDto>();

        CreateMap<ClientEventDto, ClientEvent>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("source", opt => opt.MapFrom(src => src.Source))
            .ForCtorParam("eventedAt", opt => opt.MapFrom(src => src.EventedAt));
    }
}

public sealed class ClientSourceProfile : Profile
{
    public ClientSourceProfile()
    {
        CreateMap<ClientSource, ClientSourceDto>();

        CreateMap<ClientSourceDto, ClientSource>()
            .ForCtorParam("source", opt => opt.MapFrom(src => src.Source))
            .ForCtorParam("sourceUrl", opt => opt.MapFrom(src => src.SourceUrl));
    }
}

public sealed class ClientEventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ClientSourceDto Source { get; set; } = new();
    public DateTime EventedAt { get; set; }
}

public sealed class ClientSourceDto
{
    public string Source { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
}
