namespace AMDespachante.Application.ViewModels
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Modelo { get; set; }
        public string AnoFabricacao { get; set; }
        public string AnoModelo { get; set; }
        public Guid ClienteId { get; set; }
        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
