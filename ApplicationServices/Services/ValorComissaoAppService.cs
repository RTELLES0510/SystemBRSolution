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
    public class ValorComissaoAppService : AppServiceBase<VALOR_COMISSAO>, IValorComissaoAppService
    {
        private readonly IValorComissaoService _baseService;

        public ValorComissaoAppService(IValorComissaoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<VALOR_COMISSAO> GetAllItens()
        {
            List<VALOR_COMISSAO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<VALOR_COMISSAO> GetAllItensAdm()
        {
            List<VALOR_COMISSAO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public VALOR_COMISSAO GetItemById(Int32 id)
        {
            VALOR_COMISSAO item = _baseService.GetItemById(id);
            return item;
        }

        public VALOR_COMISSAO CheckExist(VALOR_COMISSAO conta)
        {
            VALOR_COMISSAO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<TIPO_COMISSAO> GetAllTipos()
        {
            List<TIPO_COMISSAO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<CATEGORIA_PRODUTO> GetAllCategorias()
        {
            List<CATEGORIA_PRODUTO> lista = _baseService.GetAllCategorias();
            return lista;
        }

        public List<FILIAL> GetAllFiliais()
        {
            List<FILIAL> lista = _baseService.GetAllFiliais();
            return lista;
        }

        public Int32 ExecuteFilter(Int32? categoria, Int32? tipo, String nome, out List<VALOR_COMISSAO> objeto)
        {
            try
            {
                objeto = new List<VALOR_COMISSAO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(categoria, tipo, nome);
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


        public Int32 ValidateCreate(VALOR_COMISSAO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.VACO_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddVACO",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<VALOR_COMISSAO>(item)
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

        public Int32 ValidateEdit(VALOR_COMISSAO item, VALOR_COMISSAO itemAntes, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditVACO",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<VALOR_COMISSAO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<VALOR_COMISSAO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(VALOR_COMISSAO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.COMISSAO_VENDA.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.VACO_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelVACO",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<VALOR_COMISSAO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(VALOR_COMISSAO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.VACO_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatVACO",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<VALOR_COMISSAO>(item)
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
