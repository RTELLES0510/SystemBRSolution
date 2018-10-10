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
    public class FilialAppService : AppServiceBase<FILIAL>, IFilialAppService
    {
        private readonly IFilialService _baseService;

        public FilialAppService(IFilialService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<FILIAL> GetAllItens()
        {
            List<FILIAL> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<FILIAL> GetAllItensAdm()
        {
            List<FILIAL> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public FILIAL GetItemById(Int32 id)
        {
            FILIAL item = _baseService.GetItemById(id);
            return item;
        }

        public FILIAL CheckExist(FILIAL filial)
        {
            FILIAL item = _baseService.CheckExist(filial);
            return item;
        }

        public Int32 ValidateCreate(FILIAL item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.FILI_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddFILI",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<FILIAL>(item)
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

        public Int32 ValidateEdit(FILIAL item, FILIAL itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditFILI",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<FILIAL>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<FILIAL>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(FILIAL item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.AGENDA.Count > 0)
                {
                    return 1;
                }
                if (item.CLIENTE.Count > 0)
                {
                    return 1;
                }
                if (item.COLABORADOR.Count > 0)
                {
                    return 1;
                }
                if (item.CONTA_PAGAR.Count > 0)
                {
                    return 1;
                }
                if (item.CONTA_RECEBER.Count > 0)
                {
                    return 1;
                }
                if (item.EQUIPAMENTO.Count > 0)
                {
                    return 1;
                }
                if (item.FORNECEDOR.Count > 0)
                {
                    return 1;
                }
                if (item.INVENTARIO.Count > 0)
                {
                    return 1;
                }
                if (item.MATERIA_PRIMA.Count > 0)
                {
                    return 1;
                }
                if (item.MOVIMENTO_ESTOQUE_MATERIA_PRIMA.Count > 0)
                {
                    return 1;
                }
                if (item.MOVIMENTO_ESTOQUE_PRODUTO.Count > 0)
                {
                    return 1;
                }
                if (item.OPORTUNIDADE_NEGOCIO.Count > 0)
                {
                    return 1;
                }
                if (item.PATRIMONIO.Count > 0)
                {
                    return 1;
                }
                if (item.PEDIDO_COMPRA.Count > 0)
                {
                    return 1;
                }
                if (item.PEDIDO_SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.PEDIDO_VENDA.Count > 0)
                {
                    return 1;
                }
                if (item.PRECO_PRODUTO.Count > 0)
                {
                    return 1;
                }
                if (item.PRECO_SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.PRODUTO.Count > 0)
                {
                    return 1;
                }
                if (item.PROPOSTA_SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.PROPOSTA_VENDA.Count > 0)
                {
                    return 1;
                }
                if (item.SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.TAREFA.Count > 0)
                {
                    return 1;
                }
                if (item.TICKET_ATENDIMENTO.Count > 0)
                {
                    return 1;
                }
                if (item.TRANSPORTADORA.Count > 0)
                {
                    return 1;
                }
                if (item.VALOR_COMISSAO.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.FILI_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelFILI",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<FILIAL>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(FILIAL item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.FILI_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatFILI",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<FILIAL>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
