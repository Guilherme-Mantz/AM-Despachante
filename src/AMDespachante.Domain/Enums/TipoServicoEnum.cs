using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AMDespachante.Domain.Enums;

public enum TipoServicoEnum
{
    [Description("Transferência")]
    [Display(Name = "Transferência")]
    Transferencia = 0,

    [Description("Licenciamento")]
    [Display(Name = "Licenciamento")]
    Licenciamento = 1,

    [Description("Zero Km")]
    [Display(Name = "Zero Km")]
    ZeroKm = 2,

    [Description("Liberação")]
    [Display(Name = "Liberação")]
    Liberacao = 3,

    [Description("Renovação CNH")]
    [Display(Name = "Renovação CNH")]
    RenovacaoCNH = 4,

    [Description("Indicação de Multa")]
    [Display(Name = "Indicação de Multa")]
    IndicacaoMulta = 5,

    [Description("Preenchimento ATPV")]
    [Display(Name = "Preenchimento ATPV")]
    PreenchimentoATPV = 6,

    [Description("Impressão CRLVe")]
    [Display(Name = "Impressão CRLVe")]
    ImpressaoCRLVe = 7
}
