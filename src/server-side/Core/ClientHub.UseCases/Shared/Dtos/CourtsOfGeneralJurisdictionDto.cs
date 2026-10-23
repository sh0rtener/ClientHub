using AutoMapper;
using ClientHub.Domain.Clients.Arbitrages;
using ClientHub.Domain.Clients.Courts;
using ClientHub.Domain.Clients.CourtValues;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class CourtsOfGeneralJurisdictionDto
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
    public int Result { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LiquidationDate { get; set; }
}

public sealed class CourtsOfGeneralJurisdictionProfile : Profile
{
    public CourtsOfGeneralJurisdictionProfile()
    {
        CreateMap<CourtsOfGeneralJurisdiction, CourtsOfGeneralJurisdictionDto>()
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
                opt => opt.MapFrom(src => src.Result.HasValue ? (int)src.Result.Value : 0)
            );

        CreateMap<CourtsOfGeneralJurisdictionDto, CourtsOfGeneralJurisdiction>()
            .ForCtorParam("caseNumber", opt => opt.MapFrom(src => new CaseNumber(src.CaseNumber)))
            .ForCtorParam("courtName", opt => opt.MapFrom(src => new CourtName(src.CourtName)))
            .ForCtorParam("instance", opt => opt.MapFrom(src => new Instanse(src.Instanse)))
            .ForCtorParam("caseType", opt => opt.MapFrom(src => new CaseType(src.CaseType)))
            .ForCtorParam("partyRole", opt => opt.MapFrom(src => src.PartyRole))
            .ForCtorParam(
                "counterpartyName",
                opt => opt.MapFrom(src => new CountrAgentName(src.CountrAgentName))
            )
            .ForCtorParam("counterpartyInn", opt => opt.MapFrom(src => new Inn(src.CountrAgentInn)))
            .ForCtorParam(
                "claimAmount",
                opt => opt.MapFrom(src => new ClaimAmount(src.ClaimAmount))
            )
            .ForCtorParam("subject", opt => opt.MapFrom(src => new Subject(src.Subject)))
            .ForCtorParam("status", opt => opt.MapFrom(src => (ArbitrageDealStatuses)src.Status))
            .ForCtorParam("result", opt => opt.MapFrom(src => (ArbitrageDealResults)src.Result))
            .ForCtorParam("registrationDate", opt => opt.MapFrom(src => src.RegistrationDate))
            .ForCtorParam("liqudationDate", opt => opt.MapFrom(src => src.LiquidationDate));
    }
}
