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
    public class ServicoAppService : AppServiceBase<SERVICO>, IServicoAppService
    {
        private readonly IServicoService _baseService;

        public ServicoAppService(IServicoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<SERVICO> GetAllItens()
        {
            List<SERVICO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<SERVICO> GetAllItensAdm()
        {
            List<SERVICO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public SERVICO GetItemById(Int32 id)
        {
            SERVICO item = _baseService.GetItemById(id);
            return item;
        }

        public SERVICO GetByNome(String nome)
        {
            SERVICO item = _baseService.GetByNome(nome);
            return item;
        }

        public SERVICO CheckExist(SERVICO conta)
        {
            SERVICO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_SERVICO> GetAllTipos()
        {
            List<CATEGORIA_SERVICO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<FILIAL> GetAllFilial()
        {
            List<FILIAL> lista = _baseService.GetAllFilial();
            return lista;
        }

        public SERVICO_ANEXO GetAnexoById(Int32 id)
        {
            SERVICO_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId, out List<SERVICO> objeto)
        {
            try
            {
                objeto = new List<SERVICO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, nome, descricao, filiId);
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

        public Int32 ValidateCreate(SERVICO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.SERV_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddSERV",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<SERVICO>(item)
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

        public Int32 ValidateEdit(SERVICO item, SERVICO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditSERV",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<SERVICO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<SERVICO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(SERVICO item, SERVICO itemAntes)
        {
            try
            {
                // Persiste
                return _baseService.Edit(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(SERVICO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.ITEM_PEDIDO_SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.ITEM_PROPOSTA_SERVICO.Count > 0)
                {
                    return 1;
                }
                if (item.PRECO_SERVICO.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.SERV_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelSERV",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<SERVICO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(SERVICO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.SERV_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatSERV",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<SERVICO>(item)
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
