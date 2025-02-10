using AMDespachante.Domain.Enums;

namespace AMDespachante.Application.ViewModels;
public class RecursoViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public bool PrimeiroAcesso { get; set; }
    public bool Ativo { get; set; }
    public CargoEnum Cargo { get; set; }

    public string CriadoPor { get; set; }
    public DateTime Criado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime Modificado { get; set; }
}

