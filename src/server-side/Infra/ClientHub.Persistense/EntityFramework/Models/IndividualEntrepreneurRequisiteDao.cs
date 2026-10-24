using AutoMapper;
using ClientHub.Domain.IndividualEntrepreneurs;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class IndividualEntrepreneurRequisiteProfile : Profile
{
    public IndividualEntrepreneurRequisiteProfile()
    {
        CreateMap<IndividualEntrepreneurRequisite, IndividualEntrepreneurRequisiteDao>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ReverseMap();
    }
}

public sealed class IndividualEntrepreneurRequisiteDao
{
    public Guid Id { get; set; }
    public string Kind { get; set; } = string.Empty;
    public string Registrator { get; set; } = string.Empty;
    public string TaxAuthority { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime TaxRegistrationDate { get; set; }
}
