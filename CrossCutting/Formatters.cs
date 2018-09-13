using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public static class Formatters
    {
        /// <summary>
        /// CPFs the formatter.
        /// </summary>
        /// <param name="CPF">The CPF.</param>
        /// <returns></returns>
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

        /// <summary>
        /// CNPJs the formatter.
        /// </summary>
        /// <param name="CNPJ">The CNPJ.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Ceps the formatter.
        /// </summary>
        /// <param name="CEP">The cep.</param>
        /// <returns></returns>
        public static String CEPFormatter(String CEP)
        {
            if (String.IsNullOrEmpty(CEP))
            {
                return CEP;
            }
            CEP = new String(CEP.Where(c => char.IsDigit(c)).ToArray());
            String cep = String.Format(@"{0:00000\-000}", Convert.ToInt64(CEP));
            return cep;
        }

    }
}
