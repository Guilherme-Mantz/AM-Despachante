namespace AMDespachante.Domain.Interfaces.Services
{
    public interface ILicencaValidacaoService
    {
        /// <summary>
        /// Processa a validação de todos os veículos
        /// </summary>
        Task ProcessarValidacaoVeiculos();
    }
}
