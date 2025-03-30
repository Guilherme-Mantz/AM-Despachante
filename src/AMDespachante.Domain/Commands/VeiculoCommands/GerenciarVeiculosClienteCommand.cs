using AMDespachante.Domain.Core.Message;

namespace AMDespachante.Domain.Commands.VeiculoCommands
{
    public class GerenciarVeiculosClienteCommand : Command
    {
        public Guid ClienteId { get; private set; }
        public ICollection<AtualizarVeiculoCommand> Veiculos { get; private set; }

        public GerenciarVeiculosClienteCommand(Guid clienteId, ICollection<AtualizarVeiculoCommand> veiculos)
        {
            ClienteId = clienteId;
            Veiculos = veiculos;
        }
    }
}
