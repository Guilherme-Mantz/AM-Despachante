using AMDespachante.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMDespachante.Application.ViewModels;
public class RecursoViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Informe o Nome")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe o E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe o CPF")]
    public string Cpf { get; set; }
    public bool PrimeiroAcesso { get; set; }
    public bool Ativo { get; set; }
    public CargoEnum Cargo { get; set; }

    public string CriadoPor { get; set; }
    public DateTime Criado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime Modificado { get; set; }
}

