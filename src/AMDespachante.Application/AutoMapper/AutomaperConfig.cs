using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Commands.AtendimentoCommands;
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
            CreateMap<Atendimento, AtendimentoViewModel>().ReverseMap();

            CreateMap<RecursoViewModel, Recurso>().ReverseMap();
            CreateMap<ClienteViewModel, Cliente>().ReverseMap();

            CreateMap<VeiculoViewModel, Veiculo>();

            CreateMap<Veiculo, VeiculoViewModel>()
                .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome));

            // Commands
            CreateMap<AtendimentoViewModel, NovoAtendimentoCommand>();
            CreateMap<AtendimentoViewModel, AtualizarAtendimentoCommand>();

            CreateMap<RecursoViewModel, NovoRecursoCommand>();
            CreateMap<RecursoViewModel, AtualizarRecursoCommand>();

            CreateMap<ClienteViewModel, NovoClienteCommand>();
            CreateMap<ClienteViewModel, AtualizarClienteCommand>();

            CreateMap<VeiculoViewModel, NovoVeiculoCommand>();
            CreateMap<VeiculoViewModel, AtualizarVeiculoCommand>();
        }
    }
}
