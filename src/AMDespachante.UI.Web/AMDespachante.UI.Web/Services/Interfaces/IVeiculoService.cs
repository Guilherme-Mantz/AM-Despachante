using AMDespachante.Application.ViewModels;

namespace AMDespachante.UI.Web.Services.Interfaces
{
    public interface IVeiculoService
    {
        /// <summary>
        /// Valida e formata a placa de um veículo
        /// </summary>
        /// <returns>Tuple com (bool isValid, string message, string formattedPlaca)</returns>
        Task<(bool isValid, string message, string formattedPlaca)> ValidarPlaca(Guid id, string placa);

        /// <summary>
        /// Valida e formata o renavam
        /// </summary>
        /// <returns>Tuple com (bool isValid, string message, string formattedRenavam)</returns>
        Task<(bool isValid, string message, string formattedRenavam)> ValidarRenavam(string renavam);

        /// <summary>
        /// Valida anos de fabricação e modelo
        /// </summary>
        /// <returns>Dicionário de erros por campo (vazio se válido)</returns>
        Task<Dictionary<string, List<string>>> ValidarAnos(int? anoFabricacao, int? anoModelo);

        /// <summary>
        /// Valida um veículo completo
        /// </summary>
        /// <returns>Dicionário de erros por campo (vazio se válido)</returns>
        Task<Dictionary<string, List<string>>> ValidarVeiculo(VeiculoViewModel veiculo, int? anoFabricacaoNumerico, int? anoModeloNumerico);
    }

}
