using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.ClienteCommands;
using AMDespachante.Domain.Commands.RecursoCommands;
using AMDespachante.Domain.Commands.VeiculoCommands;
using AMDespachante.Domain.Models;
using AutoMapper;

namespace AMDespachante.Application.AutoMapper
{
    public class AutomaperConfig : Profile
    {
        public AutomaperConfig()
        {
            CreateMap<RecursoViewModel, Recurso>().ReverseMap();
            CreateMap<ClienteViewModel, Cliente>().ReverseMap();
            CreateMap<VeiculoViewModel, Veiculo>().ReverseMap();

            // Commands
            CreateMap<RecursoViewModel, NovoRecursoCommand>();
            CreateMap<RecursoViewModel, AtualizarRecursoCommand>();

            CreateMap<ClienteViewModel, NovoClienteCommand>();
            CreateMap<ClienteViewModel, AtualizarClienteCommand>();

            CreateMap<VeiculoViewModel, NovoVeiculoCommand>();
            CreateMap<VeiculoViewModel, AtualizarVeiculoCommand>();
        }
    }
}
