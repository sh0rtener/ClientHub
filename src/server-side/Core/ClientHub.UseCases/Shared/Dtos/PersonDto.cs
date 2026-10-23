using AutoMapper;
using ClientHub.Domain.Clients;

namespace ClientHub.UseCases.Shared.Dtos;

public sealed class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<PersonDto, Person>()
            .ForCtorParam("firstname", opt => opt.MapFrom(src => src.FirstName))
            .ForCtorParam("lastname", opt => opt.MapFrom(src => src.LastName))
            .ForCtorParam("patronymic", opt => opt.MapFrom(src => src.Patronymic))
            .ForCtorParam("birthdate", opt => opt.MapFrom(src => src.BirthDate))
            .ForCtorParam("gender", opt => opt.MapFrom(src => (PersonGender)src.Gender))
            .ForCtorParam("regioncode", opt => opt.MapFrom(src => src.RegionCode));
    }
}

public sealed class PersonDto
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
