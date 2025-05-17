using AMDespachante.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AMDespachante.Application.ViewModels
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "A placa deve ter entre 7 e 8 caracteres")]
        [RegularExpression(@"^[A-Z]{3}[0-9][0-9A-Z][0-9]{2}$|^[A-Z]{3}-[0-9]{4}$",
        ErrorMessage = "Formato de placa inválido. Use o formato AAA0000 ou AAA-0000 para placas antigas ou AAA0A00 para placas Mercosul")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O Renavam é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O Renavam deve ter 11 dígitos")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "O Renavam deve conter apenas números")]
        public string Renavam { get; set; }

        [Required(ErrorMessage = "O tipo do veículo é obrigatório")]
        [Display(Name = "Tipo do veículo")]
        public TipoVeiculoEnum TipoVeiculo { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O modelo deve ter entre 2 e 50 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O ano de fabricação é obrigatório")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "O ano de fabricação deve ser um ano válido entre 1900 e 2099")]
        public string AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O ano do modelo é obrigatório")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "O ano do modelo deve ser um ano válido entre 1900 e 2099")]
        public string AnoModelo { get; set; }

        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public bool? AlertaLicenciamento { get; set; }

        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }
    }
}
