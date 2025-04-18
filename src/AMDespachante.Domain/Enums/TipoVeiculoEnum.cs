using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AMDespachante.Domain.Enums
{
    public enum TipoVeiculoEnum
    {
        [Description("Carro")]
        [Display(Name = "Carro")]
        Carro = 0,

        [Description("Moto")]
        [Display(Name = "Moto")]
        Moto = 1,

        [Description("Caminhão")]
        [Display(Name = "Caminhão")]
        Caminhao = 2
    }
}
