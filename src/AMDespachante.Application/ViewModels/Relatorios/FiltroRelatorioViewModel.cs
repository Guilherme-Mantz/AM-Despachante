namespace AMDespachante.Application.ViewModels.Relatorios
{
    public class FiltroRelatorioViewModel
    {
        public DateTime DataInicio { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DataFim { get; set; } = DateTime.Now;
    }
}
