using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Models;
using AutoMapper;

namespace AMDespachante.Application.AutoMapper
{
    public class AutomaperConfig : Profile
    {
        public AutomaperConfig()
        {
            CreateMap<RecursoViewModel, Recurso>().ReverseMap();
        }
    }
}
