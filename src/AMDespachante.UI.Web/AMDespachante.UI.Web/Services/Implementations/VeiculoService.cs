using AMDespachante.Application.ViewModels;
using AMDespachante.Domain.Interfaces;
using AMDespachante.UI.Web.Services.Interfaces;
using System.Text.RegularExpressions;

namespace AMDespachante.UI.Web.Services.Implementations
{
    public class VeiculoService(IVeiculoRepository veiculoRepository) : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository = veiculoRepository;

        public async Task<(bool isValid, string message, string formattedPlaca)> ValidarPlaca(Guid id, string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                return (false, "A placa é obrigatória", placa);

            var placaNormalizada = placa.Replace("-", "").Replace(" ", "").ToUpper();

            bool placaAntigaValida = Regex.IsMatch(placaNormalizada, @"^[A-Z]{3}[0-9]{4}$");
            bool placaMercosulValida = Regex.IsMatch(placaNormalizada, @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$");

            if (!placaAntigaValida && !placaMercosulValida)
                return (false, "Formato de placa inválido. Use o formato AAA-0000 para placas antigas ou AAA0A00 para placas Mercosul", placa);

            if (_veiculoRepository != null && await _veiculoRepository.PlacaExists(id, placa))
                return (false, $"Já existe um veículo cadastrado com a placa {placa}.", placa);

            string placaFormatada = placaAntigaValida
                ? $"{placaNormalizada.Substring(0, 3)}-{placaNormalizada.Substring(3, 4)}"
                : placaNormalizada;

            return (true, string.Empty, placaFormatada);
        }

        public async Task<(bool isValid, string message, string formattedRenavam)> ValidarRenavam(string renavam)
        {
            if (string.IsNullOrWhiteSpace(renavam))
                return (false, "O Renavam é obrigatório", renavam);

            var renavamNormalizado = Regex.Replace(renavam, @"\D", "");

            if (renavamNormalizado.Length != 11)
                return (false, "O Renavam deve ter exatamente 11 dígitos numéricos", renavam);

            return (true, string.Empty, renavamNormalizado);
        }

        public async Task<Dictionary<string, List<string>>> ValidarAnos(int? anoFabricacao, int? anoModelo)
        {
            var erros = new Dictionary<string, List<string>>();

            if (anoFabricacao == null)
                AddError(erros, "AnoFabricacao", "O ano de fabricação é obrigatório");

            if (anoModelo == null)
                AddError(erros, "AnoModelo", "O ano do modelo é obrigatório");

            if (anoFabricacao.HasValue && anoModelo.HasValue)
            {
                if (anoModelo < anoFabricacao)
                    AddError(erros, "AnoModelo", "O ano do modelo não pode ser anterior ao ano de fabricação");

                if (anoModelo > anoFabricacao + 1)
                    AddError(erros, "AnoModelo", "O ano do modelo não deve ser mais de 1 ano posterior ao ano de fabricação");
            }

            return erros;
        }

        public async Task<Dictionary<string, List<string>>> ValidarVeiculo(VeiculoViewModel veiculo, int? anoFabricacaoNumerico, int? anoModeloNumerico)
        {
            var erros = new Dictionary<string, List<string>>();

            var (placaValida, placaMensagem, placaFormatada) = await ValidarPlaca(veiculo.Id, veiculo.Placa);
            if (!placaValida)
                AddError(erros, nameof(veiculo.Placa), placaMensagem);
            else
                veiculo.Placa = placaFormatada;

            var (renavamValido, renavamMensagem, renavamFormatado) = await ValidarRenavam(veiculo.Renavam);
            if (!renavamValido)
                AddError(erros, nameof(veiculo.Renavam), renavamMensagem);
            else
                veiculo.Renavam = renavamFormatado;

            if (string.IsNullOrWhiteSpace(veiculo.Modelo))
                AddError(erros, nameof(veiculo.Modelo), "O modelo é obrigatório");

            var errosAnos = await ValidarAnos(anoFabricacaoNumerico, anoModeloNumerico);
            foreach (var erro in errosAnos)
            {
                foreach (var mensagem in erro.Value)
                {
                    var campo = erro.Key == "AnoFabricacao" ? nameof(veiculo.AnoFabricacao) : nameof(veiculo.AnoModelo);
                    AddError(erros, campo, mensagem);
                }
            }

            if (anoFabricacaoNumerico.HasValue)
                veiculo.AnoFabricacao = anoFabricacaoNumerico.ToString();

            if (anoModeloNumerico.HasValue)
                veiculo.AnoModelo = anoModeloNumerico.ToString();

            return erros;
        }

        private void AddError(Dictionary<string, List<string>> errors, string field, string message)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(message);
        }
    }
}