using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AMDespachante.Domain.Enums
{
    public enum StatusAtendimentoEnum
    {
        [Description("Pendente")]
        [Display(Name = "Pendente")]
        Pendente = 0,

        [Description("Pago")]
        [Display(Name = "Pago")]
        Pago = 1,

        [Description("Concluído")]
        [Display(Name = "Concluído")]
        Concluido = 2
    }
}
