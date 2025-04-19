using System.Text.RegularExpressions;

namespace AMDespachante.Domain.Utilities
{
    public static class DocumentoFiscalUtils
    {
        public static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
                return false;

            bool todosDigitosIguais = true;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    todosDigitosIguais = false;
                    break;
                }
            }
            if (todosDigitosIguais)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digitoVerificador2;
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || cnpj.Length != 14)
                return false;

            bool todosDigitosIguais = true;
            for (int i = 1; i < cnpj.Length; i++)
            {
                if (cnpj[i] != cnpj[0])
                {
                    todosDigitosIguais = false;
                    break;
                }
            }
            if (todosDigitosIguais)
                return false;

            int[] multiplicadores1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cnpj[12].ToString()) != digitoVerificador1)
                return false;

            int[] multiplicadores2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cnpj[13].ToString()) == digitoVerificador2;
        }

        public static bool ValidarDocumentoFiscal(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return false;

            // Remove non-numeric characters
            var documentoSemMascara = Regex.Replace(documento, @"\D", "");

            // Determine type based on length
            if (documentoSemMascara.Length == 11)
                return ValidarCPF(documentoSemMascara);
            else if (documentoSemMascara.Length == 14)
                return ValidarCNPJ(documentoSemMascara);

            return false;
        }

        public static bool EhCPF(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return false;

            var documentoSemMascara = Regex.Replace(documento, @"\D", "");
            return documentoSemMascara.Length == 11;
        }

        public static bool EhCNPJ(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento))
                return false;

            var documentoSemMascara = Regex.Replace(documento, @"\D", "");
            return documentoSemMascara.Length == 14;
        }
    }
}