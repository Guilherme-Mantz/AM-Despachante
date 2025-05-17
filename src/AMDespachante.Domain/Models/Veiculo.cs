using AMDespachante.Domain.Core.DomainObjects;
using AMDespachante.Domain.Enums;

namespace AMDespachante.Domain.Models
{
    public class Veiculo : Entity, IAggregateRoot
    {
        public Veiculo() { }

        public Veiculo(string placa, string renavam, TipoVeiculoEnum tipoVeiculo, string modelo, string anoFabricacao, string anoModelo)
        {
            Placa = placa;
            Renavam = renavam;
            TipoVeiculo = tipoVeiculo;
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
        }

        public Veiculo(string placa, string renavam, TipoVeiculoEnum tipoVeiculo, string modelo, string anoFabricacao, string anoModelo, Guid clienteId) : this(placa, renavam, tipoVeiculo, modelo, anoFabricacao, anoModelo)
        {
            ClienteId = clienteId;
        }

        public string Placa { get; set; }
        public string Renavam { get; set; }
        public TipoVeiculoEnum TipoVeiculo { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        public DateTime? DataUltimaValidacao { get; set; }
        public ValidacaoStatusEnum? Status { get; set; }
        public bool? AlertaLicenciamento { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }

        // 1:N com Atendimento
        public virtual ICollection<Atendimento> Atendimentos { get; set; }

        public int ObterFinalPlaca()
        {
            if (string.IsNullOrEmpty(Placa) || Placa.Length < 1)
                return -1;

            char ultimoDigito = Placa[^1];
            if (char.IsDigit(ultimoDigito))
                return int.Parse(ultimoDigito.ToString());

            return -1;
        }
    }
}
