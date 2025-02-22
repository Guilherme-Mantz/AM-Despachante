using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.RecursoCommands;
using AMDespachante.Domain.Models;
using AutoMapper;

namespace AMDespachante.Application.AutoMapper
{
    public class AutomaperConfig : Profile
    {
        public AutomaperConfig()
        {
            CreateMap<RecursoViewModel, Recurso>().ReverseMap();

            // Commands
            CreateMap<RecursoViewModel, NovoRecursoCommand>();
            CreateMap<RecursoViewModel, AtualizarRecursoCommand>();
        }
    }
}
