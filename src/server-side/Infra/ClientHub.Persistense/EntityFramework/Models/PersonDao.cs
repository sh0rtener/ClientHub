using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.Persistense.EntityFramework.Models;

public sealed class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDao>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<PersonDao, Person>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (PersonGender)src.Gender));
    }
}

public sealed class PersonDao
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public string RegionCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
