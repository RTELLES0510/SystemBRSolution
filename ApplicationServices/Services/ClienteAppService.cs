using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class ClienteAppService : AppServiceBase<CLIENTE>, IClienteAppService
    {
        private readonly IClienteService _baseService;

        public ClienteAppService(IClienteService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<CLIENTE> GetAllItens()
        {
            List<CLIENTE> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<CLIENTE> GetAllItensAdm()
        {
            List<CLIENTE> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public CLIENTE GetItemById(Int32 id)
        {
            CLIENTE item = _baseService.GetItemById(id);
            return item;
        }

        public CLIENTE GetByEmail(String email)
        {
            CLIENTE item = _baseService.GetByEmail(email);
            return item;
        }

        public CLIENTE CheckExist(CLIENTE conta)
        {
            CLIENTE item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_CLIENTE> GetAllTipos()
        {
            List<CATEGORIA_CLIENTE> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<FILIAL> GetAllFilial()
        {
            List<FILIAL> lista = _baseService.GetAllFilial();
            return lista;
        }

        public List<COLABORADOR> GetAllVendedores()
        {
            List<COLABORADOR> lista = _baseService.GetAllVendedores();
            return lista;
        }

        public List<TIPO_CONTRIBUINTE> GetAllTiposContribuinte()
        {
            List<TIPO_CONTRIBUINTE> lista = _baseService.GetAllTiposContribuinte();
            return lista;
        }

        public List<TIPO_PESSOA> GetAllTiposPessoa()
        {
            List<TIPO_PESSOA> lista = _baseService.GetAllTiposPessoa();
            return lista;
        }

        public CLIENTE_ANEXO GetAnexoById(Int32 id)
        {
            CLIENTE_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public CLIENTE_CONTATO GetContatoById(Int32 id)
        {
            CLIENTE_CONTATO lista = _baseService.GetContatoById(id);
            return lista;
        }

        public CLIENTE_REFERENCIA GetReferenciaById(Int32 id)
        {
            CLIENTE_REFERENCIA lista = _baseService.GetReferenciaById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede, out List<CLIENTE> objeto)
        {
            try
            {
                objeto = new List<CLIENTE>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, nome, cpf, cnpj, email, cidade, uf, rede);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreate(CLIENTE item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.CLIE_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Checa endereço
                if (String.IsNullOrEmpty(item.CLIE_NM_ENDERECO))
                {
                    item.CLIE_NM_ENDERECO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_BAIRRO))
                {
                    item.CLIE_NM_BAIRRO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_CIDADE))
                {
                    item.CLIE_NM_CIDADE = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_SG_UF))
                {
                    item.CLIE_SG_UF = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NR_CEP))
                {
                    item.CLIE_NR_CEP = "-";
                }

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddCLIE",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CLIENTE>(item)
                };

                // Persiste
                Int32 volta = _baseService.Create(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CLIENTE item, CLIENTE itemAntes, USUARIO usuario)
        {
            try
            {
                // Checa endereço
                if (String.IsNullOrEmpty(item.CLIE_NM_ENDERECO))
                {
                    item.CLIE_NM_ENDERECO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_BAIRRO))
                {
                    item.CLIE_NM_BAIRRO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_CIDADE))
                {
                    item.CLIE_NM_CIDADE = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_SG_UF))
                {
                    item.CLIE_SG_UF = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NR_CEP))
                {
                    item.CLIE_NR_CEP = "-";
                }

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditCLIE",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CLIENTE>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CLIENTE>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CLIENTE item, CLIENTE itemAntes)
        {
            try
            {
                // Checa endereço
                if (String.IsNullOrEmpty(item.CLIE_NM_ENDERECO))
                {
                    item.CLIE_NM_ENDERECO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_BAIRRO))
                {
                    item.CLIE_NM_BAIRRO = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NM_CIDADE))
                {
                    item.CLIE_NM_CIDADE = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_SG_UF))
                {
                    item.CLIE_SG_UF = "-";
                }
                if (String.IsNullOrEmpty(item.CLIE_NR_CEP))
                {
                    item.CLIE_NR_CEP = "-";
                }

                // Persiste
                return _baseService.Edit(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(CLIENTE item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.OPORTUNIDADE_NEGOCIO.Count > 0)
                {
                    return 1;
                }
                if (item.PEDIDO_SERVICO.Count > 0)
                {
                    return 2;
                }
                if (item.PEDIDO_VENDA.Count > 0)
                {
                    return 2;
                }
                if (item.PROPOSTA_SERVICO.Count > 0)
                {
                    return 2;
                }
                if (item.PROPOSTA_VENDA.Count > 0)
                {
                    return 2;
                }
                if (item.TICKET_ATENDIMENTO.Count > 0)
                {
                    return 2;
                }

                // Acerta campos
                item.CLIE_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelCLIE",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CLIENTE>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(CLIENTE item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CLIE_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatCLIE",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CLIENTE>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEditContato(CLIENTE_CONTATO item)
        {
            try
            {
                // Persiste
                return _baseService.EditContato(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateContato(CLIENTE_CONTATO item)
        {
            try
            {
                // Persiste
                Int32 volta = _baseService.CreateContato(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEditReferencia(CLIENTE_REFERENCIA item)
        {
            try
            {
                // Persiste
                return _baseService.EditReferencia(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateReferencia(CLIENTE_REFERENCIA item)
        {
            try
            {
                // Persiste
                Int32 volta = _baseService.CreateReferencia(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateTag(CLIENTE_TAG item)
        {
            try
            {
                // Persiste
                Int32 volta = _baseService.CreateTag(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
