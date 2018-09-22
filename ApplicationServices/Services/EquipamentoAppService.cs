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
    public class EquipamentoAppService : AppServiceBase<EQUIPAMENTO>, IEquipamentoAppService
    {
        private readonly IEquipamentoService _baseService;

        public EquipamentoAppService(IEquipamentoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<EQUIPAMENTO> GetAllItens()
        {
            List<EQUIPAMENTO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<EQUIPAMENTO> GetAllItensAdm()
        {
            List<EQUIPAMENTO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public EQUIPAMENTO GetItemById(Int32 id)
        {
            EQUIPAMENTO item = _baseService.GetItemById(id);
            return item;
        }

        public EQUIPAMENTO GetByNumero(String numero)
        {
            EQUIPAMENTO item = _baseService.GetByNumero(numero);
            return item;
        }

        public EQUIPAMENTO CheckExist(EQUIPAMENTO conta)
        {
            EQUIPAMENTO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_EQUIPAMENTO> GetAllTipos()
        {
            List<CATEGORIA_EQUIPAMENTO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<FILIAL> GetAllFilial()
        {
            List<FILIAL> lista = _baseService.GetAllFilial();
            return lista;
        }

        public EQUIPAMENTO_ANEXO GetAnexoById(Int32 id)
        {
            EQUIPAMENTO_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, String nome, String numero, Int32? filiId, out List<EQUIPAMENTO> objeto)
        {
            try
            {
                objeto = new List<EQUIPAMENTO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, nome, numero, filiId);
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

        public Int32 ValidateCreate(EQUIPAMENTO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.EQUI_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddEQUI",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<EQUIPAMENTO>(item)
                };

                // Persiste patrimonio
                Int32 volta = _baseService.Create(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(EQUIPAMENTO item, EQUIPAMENTO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditEQUI",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<EQUIPAMENTO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<EQUIPAMENTO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(EQUIPAMENTO item, EQUIPAMENTO itemAntes)
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

        public Int32 ValidateDelete(EQUIPAMENTO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.EQUI_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelEQUI",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<EQUIPAMENTO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(EQUIPAMENTO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.EQUI_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatEQUI",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<EQUIPAMENTO>(item)
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
