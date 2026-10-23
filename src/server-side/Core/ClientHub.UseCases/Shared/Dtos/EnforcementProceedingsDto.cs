using AutoMapper;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class EnforcementProceedingsDto
{
    public Guid Id { get; set; }
    public string FsspDepartament { get; set; } = string.Empty;
    public string Bailiff { get; set; } = string.Empty;
    public string BasicDocType { get; set; } = string.Empty;
    public string BasicDocNumber { get; set; } = string.Empty;
    public DateTime BasicDocDate { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Creditor { get; set; } = string.Empty;
    public string CreditorInn { get; set; } = string.Empty;
    public decimal DeptAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public string? ClosureReason { get; set; }
    public int Status { get; set; }
}

public sealed class EnforcementProceedingsProfile : Profile
{
    public EnforcementProceedingsProfile()
    {
        CreateMap<EnforcementProceedings, EnforcementProceedingsDto>()
            .ForMember(
                dest => dest.FsspDepartament,
                opt => opt.MapFrom(src => src.FsspDepartament.Value)
            )
            .ForMember(dest => dest.Bailiff, opt => opt.MapFrom(src => src.Bailiff.Value))
            .ForMember(dest => dest.BasicDocType, opt => opt.MapFrom(src => src.BasicDocType.Value))
            .ForMember(
                dest => dest.BasicDocNumber,
                opt => opt.MapFrom(src => src.BasicDocNumber.Value)
            )
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Value))
            .ForMember(dest => dest.Creditor, opt => opt.MapFrom(src => src.Creditor.Value))
            .ForMember(dest => dest.CreditorInn, opt => opt.MapFrom(src => src.CreditorInn.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

        CreateMap<EnforcementProceedingsDto, EnforcementProceedings>()
            .ForCtorParam(
                "fsspDepartament",
                opt => opt.MapFrom(src => new FsspDepartament(src.FsspDepartament))
            )
            .ForCtorParam("bailiff", opt => opt.MapFrom(src => new Bailiff(src.Bailiff)))
            .ForCtorParam(
                "basicDocNumber",
                opt => opt.MapFrom(src => new BasicDocNumber(src.BasicDocNumber))
            )
            .ForCtorParam(
                "basicDocType",
                opt => opt.MapFrom(src => new BasicDocType(src.BasicDocType))
            )
            .ForCtorParam("basicDocDate", opt => opt.MapFrom(src => src.BasicDocDate))
            .ForCtorParam("subject", opt => opt.MapFrom(src => new Subject(src.Subject)))
            .ForCtorParam("creditor", opt => opt.MapFrom(src => new Creditor(src.Creditor)))
            .ForCtorParam("creditorInn", opt => opt.MapFrom(src => new Inn(src.CreditorInn)))
            .ForCtorParam("deptAmount", opt => opt.MapFrom(src => src.DeptAmount))
            .ForCtorParam("remainingAmount", opt => opt.MapFrom(src => src.RemainingAmount))
            .ForCtorParam("openedAt", opt => opt.MapFrom(src => src.OpenedAt))
            .ForCtorParam("closedAt", opt => opt.MapFrom(src => src.ClosedAt))
            .ForCtorParam("closureReason", opt => opt.MapFrom(src => src.ClosureReason))
            .ForCtorParam(
                "status",
                opt => opt.MapFrom(src => (EnforcementProceedingsStatuses)src.Status)
            );
    }
}
