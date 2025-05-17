using AMDespachante.Domain.Enums;
using AMDespachante.Domain.Interfaces;
using AMDespachante.Domain.Interfaces.Services;
using AMDespachante.Domain.Models;

namespace AMDespachante.Domain.Services
{
    public class LicencaValidacaoService(IVeiculoRepository veiculoRepository) : ILicencaValidacaoService
    {
        private readonly IVeiculoRepository _veiculoRepository = veiculoRepository;

        public bool EhPeriodoDeValidacao(Veiculo veiculo, DateTime dataAtual)
        {
            var mesAtual = dataAtual.Month;
            var finalPlaca = veiculo.ObterFinalPlaca();

            if (finalPlaca == -1)
                return false;

            return veiculo.TipoVeiculo switch
            {
                TipoVeiculoEnum.Carro or TipoVeiculoEnum.Moto =>
                    (mesAtual == 7 && (finalPlaca == 1 || finalPlaca == 2)) ||
                    (mesAtual == 8 && (finalPlaca == 3 || finalPlaca == 4)) ||
                    (mesAtual == 9 && (finalPlaca == 5 || finalPlaca == 6)) ||
                    (mesAtual == 10 && (finalPlaca == 7 || finalPlaca == 8)) ||
                    (mesAtual == 11 && finalPlaca == 9) ||
                    (mesAtual == 12 && finalPlaca == 0),

                TipoVeiculoEnum.Caminhao =>
                    (mesAtual == 9 && (finalPlaca == 1 || finalPlaca == 2)) ||
                    (mesAtual == 10 && (finalPlaca == 3 || finalPlaca == 4 || finalPlaca == 5)) ||
                    (mesAtual == 11 && (finalPlaca == 6 || finalPlaca == 7 || finalPlaca == 8)) ||
                    (mesAtual == 12 && (finalPlaca == 9 || finalPlaca == 0)),

                _ => false,
            };
        }

        /// <summary>
        /// Atualiza o status de validação do veículo
        /// </summary>
        public ValidacaoStatusEnum UpdateValidationStatus(Veiculo veiculo, DateTime dataAtual)
        {
            bool ehPeriodo = EhPeriodoDeValidacao(veiculo, dataAtual);
            bool validado = veiculo.DataUltimaValidacao?.Year == dataAtual.Year;

            if (!ehPeriodo)
            {
                return ValidacaoStatusEnum.NAO_NECESSARIO;
            }
            if (validado)
            {
                return ValidacaoStatusEnum.VALIDADO;
            }

            return ValidacaoStatusEnum.NECESSARIO;
        }

        
        public async Task ProcessarValidacaoVeiculos()
        {
            var veiculos = await _veiculoRepository.GetAll();
            var dataAtual = DateTime.Now;

            foreach (var veiculo in veiculos)
            {
                veiculo.Status = UpdateValidationStatus(veiculo, dataAtual);
                veiculo.AlertaLicenciamento = veiculo.Status == ValidacaoStatusEnum.NECESSARIO;
                _veiculoRepository.Update(veiculo);
            }

            await _veiculoRepository.UnitOfWork.Commit(true);
        }
    }
}
