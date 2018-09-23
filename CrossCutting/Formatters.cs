using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public static class Formatters
    {
        public static String CPFFormatter(String CPF)
        {
            if (String.IsNullOrEmpty(CPF))
            {
                return CPF;
            }

            CPF = new String(CPF.Where(c => char.IsDigit(c)).ToArray());
            String cpf = String.Format(@"{0:\000\.000\.000\-00}", Convert.ToInt64(CPF));
            return cpf;
        }

        public static String CNPJFormatter(String CNPJ)
        {
            if (String.IsNullOrEmpty(CNPJ))
            {
                return CNPJ;
            }
            CNPJ = new String(CNPJ.Where(c => char.IsDigit(c)).ToArray());
            String cnpj = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(CNPJ));
            return cnpj;
        }

        public static String CEPFormatter(String CEP)
        {
            if (String.IsNullOrEmpty(CEP))
            {
                return CEP;
            }
            Decimal valorOriginal = 0;
            string valorFormatado = valorOriginal.ToString("#,0.00", new CultureInfo("pt-BR"));

            CEP = new String(CEP.Where(c => char.IsDigit(c)).ToArray());
            String cep = String.Format(@"{0:00000\-000}", Convert.ToInt64(CEP));
            return cep;
        }

        public static String DecimalFormatter(Decimal number)
        {
            String valorFormatado = number.ToString("#,0.00", new CultureInfo("pt-BR"));
            return valorFormatado;
        }
    }
}
