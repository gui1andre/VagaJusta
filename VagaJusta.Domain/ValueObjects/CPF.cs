using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.Domain.ValueObjects
{
    public class CPF
    {
        public string Numero { get; }

        public CPF(string numero) 
        {
            var cpfLimpo = numero?.Replace(".", "").Replace("-", "").Trim() ?? string.Empty;
            if (!Validar(cpfLimpo))
                throw new DomainException($"CPF {numero} é inválido.");
            Numero = cpfLimpo;
        }

        private static bool Validar(string cpfLimpo)
        {
            if (string.IsNullOrWhiteSpace(cpfLimpo))
                return false;

            cpfLimpo = new string(cpfLimpo.Where(char.IsDigit).ToArray());

            if (cpfLimpo.Length != 11)
                return false;

            if (cpfLimpo.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpfLimpo.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = (resto < 2) ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cpfLimpo.EndsWith(digito);

        }
    }
}
