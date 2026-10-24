using AutoMapper;
using ClientHub.Domain.LegalEntities;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class FounderPersonProfile : Profile
{
    public FounderPersonProfile()
    {
        CreateMap<FounderPerson, FounderPersonDao>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

        CreateMap<FounderPersonDao, FounderPerson>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (LegalEntityLinkType)src.Type));
    }
}

public sealed class FounderPersonDao
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? DisassignedAt { get; set; }
    public decimal IncomeShareInDollars { get; set; }
    public decimal IncomeShareInPercent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public PersonDao Person { get; set; } = new();
}
