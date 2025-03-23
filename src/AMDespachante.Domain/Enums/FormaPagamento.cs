using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AMDespachante.Domain.Enums;

public enum FormaPagamento
{
    [Description("Dinheiro")]
    [Display(Name = "Dinheiro")]
    Dinheiro = 0,

    [Description("Cartão de Crédito")]
    [Display(Name = "Cartão de Crédito")]
    CartaoCredito = 1,

    [Description("Cartão de Débito")]
    [Display(Name = "Cartão de Débito")]
    CartaoDebito = 2,

    [Description("PIX")]
    [Display(Name = "PIX")]
    Pix = 3,

    [Description("Boleto")]
    [Display(Name = "Boleto")]
    Boleto = 4,

    [Description("Transferência")]
    [Display(Name = "Transferência")]
    Transferencia = 5,

    [Description("Mensalidade Fixa")]
    [Display(Name = "Mensalidade Fixa")]
    MensalidadeFixa = 6
}