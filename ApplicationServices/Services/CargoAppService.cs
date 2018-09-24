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
    public class CargoAppService : AppServiceBase<CARGO>, ICargoAppService
    {
        private readonly ICargoService _baseService;

        public CargoAppService(ICargoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<CARGO> GetAllItens()
        {
            List<CARGO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<CARGO> GetAllItensAdm()
        {
            List<CARGO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public List<VALOR_COMISSAO> GetAllValores()
        {
            List<VALOR_COMISSAO> lista = _baseService.GetAllValores();
            return lista;
        }

        public CARGO GetItemById(Int32 id)
        {
            CARGO item = _baseService.GetItemById(id);
            return item;
        }

        public CARGO GetByNome(String nome)
        {
            CARGO item = _baseService.GetByNome(nome);
            return item;
        }

        public Int32 ExecuteFilter(String nome, out List<CARGO> objeto)
        {
            try
            {
                objeto = new List<CARGO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(nome);
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

        public Int32 ValidateCreate(CARGO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.GetByNome(item.CARG_NM_NOME) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.CARG_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddCARG",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CARGO>(item)
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

        public Int32 ValidateEdit(CARGO item, CARGO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditCARG",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CARGO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CARGO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(CARGO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.COLABORADOR.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.CARG_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelCARG",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CARGO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(CARGO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CARG_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatCARG",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CARGO>(item)
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
