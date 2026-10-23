using AutoMapper;
using ClientHub.Domain.LegalEntities;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class KppProfile : Profile
{
    public KppProfile()
    {
        CreateMap<Kpp, KppDto>();

        CreateMap<KppDto, Kpp>().ForCtorParam("code", opt => opt.MapFrom(src => src.Code));
    }
}

public sealed class KppDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
}
