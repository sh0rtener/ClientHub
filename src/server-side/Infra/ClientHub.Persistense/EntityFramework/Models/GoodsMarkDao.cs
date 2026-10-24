using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class GoodsMarkProfile : Profile
{
    public GoodsMarkProfile()
    {
        CreateMap<GoodsMark, GoodsMarkDao>().ReverseMap();
    }
}

public sealed class GoodsMarkDao
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? DisassignedAt { get; set; }
}
