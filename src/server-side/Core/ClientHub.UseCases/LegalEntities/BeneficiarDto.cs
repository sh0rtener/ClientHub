using AutoMapper;
using ClientHub.Domain.LegalEntities;
using ClientHub.UseCases.Shared.Dtos;

namespace ClientHub.UseCases.LegalEntities;

public sealed class BeneficiarProfile : Profile
{
    public BeneficiarProfile()
    {
        CreateMap<Beneficiar, BeneficiarDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

        CreateMap<BeneficiarDto, Beneficiar>()
            .ForCtorParam("type", opt => opt.MapFrom(src => (LegalEntityLinkType)src.Type))
            .ForCtorParam("assignedAt", opt => opt.MapFrom(src => src.AssignedAt))
            .ForCtorParam("disassignedAt", opt => opt.MapFrom(src => src.CreatedAt))
            .ForCtorParam(
                "incomeShareInDollars",
                opt => opt.MapFrom(src => src.IncomeShareInDollars)
            )
            .ForCtorParam(
                "incomeShareInPercent",
                opt => opt.MapFrom(src => src.IncomeShareInPercent)
            )
            .ForCtorParam("person", opt => opt.MapFrom(src => src.Person));
    }
}

public sealed class BeneficiarDto
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? DisassignedAt { get; set; }
    public decimal IncomeShareInDollars { get; set; }
    public decimal IncomeShareInPercent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public PersonDto Person { get; set; } = new();
}
