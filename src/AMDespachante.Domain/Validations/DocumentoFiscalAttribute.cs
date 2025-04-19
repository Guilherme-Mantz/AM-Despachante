using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AMDespachante.Domain.Utilities;

namespace AMDespachante.Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DocumentoFiscalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Documento fiscal é obrigatório");

            var documento = value.ToString();
            var documentoSemMascara = Regex.Replace(documento, @"\D", "");

            if (DocumentoFiscalUtils.EhCPF(documentoSemMascara))
            {
                if (!DocumentoFiscalUtils.ValidarCPF(documentoSemMascara))
                    return new ValidationResult("CPF inválido");
            }
            else if (DocumentoFiscalUtils.EhCNPJ(documentoSemMascara))
            {
                if (!DocumentoFiscalUtils.ValidarCNPJ(documentoSemMascara))
                    return new ValidationResult("CNPJ inválido");
            }
            else
            {
                return new ValidationResult("Documento fiscal deve ter 11 (CPF) ou 14 (CNPJ) dígitos");
            }

            return ValidationResult.Success;
        }
    }
}