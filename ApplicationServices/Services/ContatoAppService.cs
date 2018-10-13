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
    public class ContratoAppService : AppServiceBase<CONTRATO>, IContratoAppService
    {
        private readonly IContratoService _baseService;

        public ContratoAppService(IContratoService baseService): base(baseService)
        {
            _baseService = baseService;
        }

        public List<CONTRATO> GetAllItens()
        {
            List<CONTRATO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<CONTRATO> GetAllItensAdm()
        {
            List<CONTRATO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public CONTRATO GetItemById(Int32 id)
        {
            CONTRATO item = _baseService.GetItemById(id);
            return item;
        }

        public CONTRATO GetByNome(String nome)
        {
            CONTRATO item = _baseService.GetByNome(nome);
            return item;
        }

        public CONTRATO CheckExist(CONTRATO conta)
        {
            CONTRATO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_CONTRATO> GetAllCategorias()
        {
            List<CATEGORIA_CONTRATO> lista = _baseService.GetAllCategorias();
            return lista;
        }

        public List<TIPO_CONTRATO> GetAllTipos()
        {
            List<TIPO_CONTRATO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<STATUS_CONTRATO> GetAllStatus()
        {
            List<STATUS_CONTRATO> lista = _baseService.GetAllStatus();
            return lista;
        }

        public List<TEMPLATE> GetAllTemplates()
        {
            List<TEMPLATE> lista = _baseService.GetAllTemplates();
            return lista;
        }

        public List<PERIODICIDADE> GetAllPeriodicidades()
        {
            List<PERIODICIDADE> lista = _baseService.GetAllPeriodicidades();
            return lista;
        }

        public List<FORMA_PAGAMENTO> GetAllForma()
        {
            List<FORMA_PAGAMENTO> lista = _baseService.GetAllForma();
            return lista;
        }

        public List<PLANO_CONTA> GetAllPlanoConta()
        {
            List<PLANO_CONTA> lista = _baseService.GetAllPlanoConta();
            return lista;
        }

        public List<CENTRO_CUSTO> GetAllCentros()
        {
            List<CENTRO_CUSTO> lista = _baseService.GetAllCentros();
            return lista;
        }

        public List<COLABORADOR> GetAllVendedores()
        {
            List<COLABORADOR> lista = _baseService.GetAllVendedores();
            return lista;
        }

        public List<NOMENCLATURA_BRAS_SERVICOS> GetAllNomenclatura()
        {
            List<NOMENCLATURA_BRAS_SERVICOS> lista = _baseService.GetAllNomenclatura();
            return lista;
        }

        public List<CLIENTE> GetAllClientes()
        {
            List<CLIENTE> lista = _baseService.GetAllClientes();
            return lista;
        }

        public CONTRATO_ANEXO GetAnexoById(Int32 id)
        {
            CONTRATO_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, Int32? tipoId, Int32? statId, String nome, String descricao, out List<CONTRATO> objeto)
        {
            try
            {
                objeto = new List<CONTRATO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, tipoId, statId, nome, descricao);
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

        public Int32 ValidateCreate(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.CONT_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
                };

                // Persiste produto
                Int32 volta = _baseService.Create(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CONTRATO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes)
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

        public Int32 ValidateDelete(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CONT_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelCONT",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CONT_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatCONT",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
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
