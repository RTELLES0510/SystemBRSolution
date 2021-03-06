using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using Correios.Net;
using ModelServices.Interfaces.ExternalServices;


namespace ExternalServices
{
    /// <summary>
    /// The ECT services class.
    /// </summary>
    public class ECT_Services : IECT_Services
    {
        /// <summary>
        /// Gets the adress cep.
        /// </summary>
        /// <param name="CEP">The cep.</param>
        /// <returns></returns>
        public Address GetAdressCEP(string CEP)
        {
            Address endereco = SearchZip.GetAddress(CEP, 10000);
            return endereco;
        }

        /// <summary>
        /// Gets the adress cep service.
        /// </summary>
        /// <param name="CEP">The cep.</param>
        /// <returns></returns>
        public Endereco GetAdressCEPService(string CEP)
        {
            Endereco endereco = null;
            try
            {
                var ws = new WSCoreios.AtendeClienteClient();
                var resposta = ws.consultaCEP(CEP);
                endereco = new Endereco();
                endereco.ENDERECO = resposta.end;
                endereco.NUMERO = resposta.complemento;
                endereco.COMPLEMENTO = resposta.complemento2;
                endereco.BAIRRO = resposta.bairro;
                endereco.CIDADE = resposta.cidade;
                endereco.UF = resposta.uf;
                endereco.CEP = CEP;
                return endereco;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
