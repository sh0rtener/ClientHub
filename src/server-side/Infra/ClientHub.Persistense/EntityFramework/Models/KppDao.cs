using AutoMapper;
using ClientHub.Domain.LegalEntities;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class KppProfile : Profile
{
    public KppProfile()
    {
        CreateMap<Kpp, KppDao>().ReverseMap();
    }
}

public sealed class KppDao
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
}
