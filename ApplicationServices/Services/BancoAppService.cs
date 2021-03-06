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
    public class BancoAppService : AppServiceBase<BANCO>, IBancoAppService
    {
        private readonly IBancoService _baseService;

        public BancoAppService(IBancoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<BANCO> GetAllItens()
        {
            List<BANCO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<BANCO> GetAllItensAdm()
        {
            List<BANCO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public BANCO GetItemById(Int32 id)
        {
            BANCO item = _baseService.GetItemById(id);
            return item;
        }

        public BANCO GetByCodigo(String codigo)
        {
            BANCO item = _baseService.GetByCodigo(codigo);
            return item;
        }

        public Int32 ExecuteFilter(String codigo, String nome, out List<BANCO> objeto)
        {
            try
            {
                objeto = new List<BANCO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(codigo, nome);
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

        public Int32 ValidateCreate(BANCO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.GetByCodigo(item.BANC_SG_CODIGO) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.BANCO_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddBANC",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<BANCO>(item)
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

        public Int32 ValidateEdit(BANCO item, BANCO itemAntes, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                //if (_baseService.GetByCodigo(item.BANC_SG_CODIGO) != null)
                //{
                //    return 1;
                //}

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditBANC",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<BANCO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<BANCO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(BANCO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.CONTA_BANCARIA.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.BANCO_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelBANC",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<BANCO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(BANCO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.BANCO_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatBANC",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<BANCO>(item)
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
