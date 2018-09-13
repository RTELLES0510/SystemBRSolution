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
    public class ContaBancariaAppService : AppServiceBase<CONTA_BANCARIA>, IContaBancariaAppService
    {
        private readonly IContaBancariaService _baseService;

        public ContaBancariaAppService(IContaBancariaService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<CONTA_BANCARIA> GetAllItens()
        {
            List<CONTA_BANCARIA> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<CONTA_BANCARIA> GetAllItensAdm()
        {
            List<CONTA_BANCARIA> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public CONTA_BANCARIA GetItemById(Int32 id)
        {
            CONTA_BANCARIA item = _baseService.GetItemById(id);
            return item;
        }

        public CONTA_BANCARIA CheckExist(CONTA_BANCARIA conta)
        {
            CONTA_BANCARIA item = _baseService.CheckExist(conta);
            return item;
        }

        public List<TIPO_CONTA> GetAllTipos()
        {
            List<TIPO_CONTA> lista = _baseService.GetAllTipos();
            return lista;
        }

        public Int32 ValidateCreate(CONTA_BANCARIA item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.COBA_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddCOBA",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTA_BANCARIA>(item)
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

        public Int32 ValidateEdit(CONTA_BANCARIA item, CONTA_BANCARIA itemAntes, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                //if (_baseService.CheckExist(item) != null)
                //{
                //    return 1;
                //}

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditCOBA",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTA_BANCARIA>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CONTA_BANCARIA>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(CONTA_BANCARIA item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.CONTA_PAGAR.Count > 0)
                {
                    return 1;
                }
                if (item.CONTA_RECEBER.Count > 0)
                {
                    return 2;
                }

                // Acerta campos
                item.COBA_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelCOBA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTA_BANCARIA>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(CONTA_BANCARIA item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.COBA_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatCOBA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTA_BANCARIA>(item)
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
