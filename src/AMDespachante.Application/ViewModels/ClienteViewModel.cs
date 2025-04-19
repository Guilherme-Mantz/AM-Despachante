using AMDespachante.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace AMDespachante.Application.ViewModels
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [StringLength(255, ErrorMessage = "O nome deve ter no máximo 255 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o (CPF/CNPJ)")]
        [Display(Name = "CPF/CNPJ")]
        [DocumentoFiscal]
        public string DocumentoFiscal { get; set; }

        [Required(ErrorMessage = "Informe o Telefone")]
        [RegularExpression(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres")]
        public string Email { get; set; }
        public bool EhEstacionamento { get; set; }
        public bool PagaMensalidade { get; set; }
        public decimal ValorMensalidade { get; set; }
        public DateTime? DataProximoVencimento { get; set; }
        
        public string CriadoPor { get; set; }
        public DateTime Criado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime Modificado { get; set; }

        public IList<VeiculoViewModel>Veiculos { get; set; }
        public IEnumerable<MensalidadeViewModel> Mensalidades { get; set; }
        public IEnumerable<AtendimentoViewModel> Atendimentos { get; set; }
    }
}
