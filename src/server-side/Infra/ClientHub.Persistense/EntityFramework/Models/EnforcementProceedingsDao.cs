using AutoMapper;
using ClientHub.Domain.Clients.CourtValues;
using ClientHub.Domain.Clients.Enforcements;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class EnforcementProceedingsDao
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
        CreateMap<EnforcementProceedings, EnforcementProceedingsDao>()
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

        CreateMap<EnforcementProceedingsDao, EnforcementProceedings>()
            .ForMember(dest => dest.FsspDepartament, opt => opt.MapFrom(src => src.FsspDepartament))
            .ForMember(dest => dest.Bailiff, opt => opt.MapFrom(src => src.Bailiff))
            .ForMember(dest => dest.BasicDocType, opt => opt.MapFrom(src => src.BasicDocType))
            .ForMember(dest => dest.BasicDocNumber, opt => opt.MapFrom(src => src.BasicDocNumber))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Creditor, opt => opt.MapFrom(src => src.Creditor))
            .ForMember(dest => dest.CreditorInn, opt => opt.MapFrom(src => src.CreditorInn))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => (EnforcementProceedingsStatuses)src.Status)
            );
    }
}
