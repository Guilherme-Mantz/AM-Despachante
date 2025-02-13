using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AMDespachante.Domain.Enums;

public enum CargoEnum
{
    [Description("Administrador")]
    [Display(Name = "Administrador")]
    ADMIN = 0,

    [Description("Funcionário")]
    [Display(Name = "Funcionário")]
    FUNCIONARIO = 1
}
