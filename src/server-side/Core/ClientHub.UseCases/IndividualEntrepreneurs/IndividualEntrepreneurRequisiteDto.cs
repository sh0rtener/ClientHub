using AutoMapper;
using ClientHub.Domain.IndividualEntrepreneurs;

namespace ClientHub.UseCases.IndividualEntrepreneurs;

public sealed class IndividualEntrepreneurRequisiteProfile : Profile
{
    public IndividualEntrepreneurRequisiteProfile()
    {
        CreateMap<IndividualEntrepreneurRequisite, IndividualEntrepreneurRequisiteDto>();

        CreateMap<IndividualEntrepreneurRequisiteDto, IndividualEntrepreneurRequisite>()
            .ForCtorParam("kind", opt => opt.MapFrom(src => src.Kind))
            .ForCtorParam("registrator", opt => opt.MapFrom(src => src.Registrator))
            .ForCtorParam("taxAuthority", opt => opt.MapFrom(src => src.TaxAuthority))
            .ForCtorParam("registrationDate", opt => opt.MapFrom(src => src.RegistrationDate))
            .ForCtorParam(
                "taxRegistrationDate",
                opt => opt.MapFrom(src => src.TaxRegistrationDate)
            );
    }
}

public sealed class IndividualEntrepreneurRequisiteDto
{
    public Guid Id { get; set; }
    public string Kind { get; set; } = string.Empty;
    public string Registrator { get; set; } = string.Empty;
    public string TaxAuthority { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime TaxRegistrationDate { get; set; }
}
