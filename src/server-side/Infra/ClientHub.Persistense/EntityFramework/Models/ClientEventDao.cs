using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class ClientEventProfile : Profile
{
    public ClientEventProfile()
    {
        CreateMap<ClientEvent, ClientEventDao>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(
                dest => dest.Source,
                opt => opt.MapFrom(src => src.Source.Source))
            .ForMember(
                dest => dest.SourceUrl,
                opt => opt.MapFrom(src => src.Source.SourceUrl));

        CreateMap<ClientEventDao, ClientEvent>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(
                dest => dest.Source,
                opt => opt.MapFrom(src =>
                    new ClientSource(src.Source, src.SourceUrl)));
    }
}

public sealed class ClientEventDao
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;
    public DateTime EventedAt { get; set; }
}
