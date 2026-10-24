using AutoMapper;
using ClientHub.Domain.Clients.Courts;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class CourtsOfGeneralJurisdictionDao
{
    public Guid Id { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string CourtName { get; set; } = string.Empty;
    public string Instanse { get; set; } = string.Empty;
    public string CaseType { get; set; } = string.Empty;
    public string PartyRole { get; set; } = string.Empty;
    public string CountrAgentName { get; set; } = string.Empty;
    public string CountrAgentInn { get; set; } = string.Empty;
    public decimal ClaimAmount { get; set; }
    public string Subject { get; set; } = string.Empty;
    public int Status { get; set; }
    public int? Result { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
}

public sealed class CourtsOfGeneralJurisdictionProfile : Profile
{
    public CourtsOfGeneralJurisdictionProfile()
    {
        CreateMap<CourtsOfGeneralJurisdiction, CourtsOfGeneralJurisdictionDao>()
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber.Value))
            .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.CourtName.Value))
            .ForMember(dest => dest.Instanse, opt => opt.MapFrom(src => src.Instanse.Value))
            .ForMember(dest => dest.CaseType, opt => opt.MapFrom(src => src.CaseType.Value))
            .ForMember(dest => dest.PartyRole, opt => opt.MapFrom(src => src.PartyRole.Value))
            .ForMember(
                dest => dest.CountrAgentName,
                opt => opt.MapFrom(src => src.CountrAgentName.Value)
            )
            .ForMember(
                dest => dest.CountrAgentInn,
                opt => opt.MapFrom(src => src.CountrAgentInn.Value)
            )
            .ForMember(dest => dest.ClaimAmount, opt => opt.MapFrom(src => src.ClaimAmount.Value))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(
                dest => dest.Result,
                opt => opt.MapFrom(src => src.Result ?? null)
            );

        CreateMap<CourtsOfGeneralJurisdictionDao, CourtsOfGeneralJurisdiction>()
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
            .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.CourtName))
            .ForMember(dest => dest.Instanse, opt => opt.MapFrom(src => src.Instanse))
            .ForMember(dest => dest.CaseType, opt => opt.MapFrom(src => src.CaseType))
            .ForMember(dest => dest.PartyRole, opt => opt.MapFrom(src => src.PartyRole))
            .ForMember(dest => dest.CountrAgentName, opt => opt.MapFrom(src => src.CountrAgentName))
            .ForMember(dest => dest.CountrAgentInn, opt => opt.MapFrom(src => src.CountrAgentInn))
            .ForMember(dest => dest.ClaimAmount, opt => opt.MapFrom(src => src.ClaimAmount))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => (CourtsOfGeneralJurisdictionStatuses)src.Status)
            )
            .ForMember(
                dest => dest.Result,
                opt => opt.MapFrom(src => (CourtsOfGeneralJurisdictionResults?)src.Result ?? null)
            );
    }
}
