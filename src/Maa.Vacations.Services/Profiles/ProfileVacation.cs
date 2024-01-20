namespace Maa.Vacations.Services.Profiles;

public class ProfileVacation : Profile
{
    public ProfileVacation()
    {
        CreateMap<CreateVacationDto, Vacation>();
        CreateMap<UpdateVacationDto, Vacation>();
        CreateMap<Vacation, VacationCreatedDto>();
        CreateMap<Vacation, CurrentVacationDto>();
        CreateMap<Vacation, DeletedVacationDto>();
        CreateMap<Vacation, VacationUpdatedDto>();
    }
}