using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class GoodsMarkProfile : Profile
{
    public GoodsMarkProfile()
    {
        CreateMap<GoodsMark, GoodsMarkDto>();

        CreateMap<GoodsMarkDto, GoodsMark>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("assignedAt", opt => opt.MapFrom(src => src.AssignedAt))
            .ForCtorParam("disassignedAt", opt => opt.MapFrom(src => src.DissasignedAt));
    }
}

public sealed class GoodsMarkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? DissasignedAt { get; set; }
}
