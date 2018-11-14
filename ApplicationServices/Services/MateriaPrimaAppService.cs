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
    public class MateriaPrimaAppService : AppServiceBase<MATERIA_PRIMA>, IMateriaPrimaAppService
    {
        private readonly IMateriaPrimaService _baseService;
        private readonly IMovimentoEstoqueMateriaService _movService;

        public MateriaPrimaAppService(IMateriaPrimaService baseService, IMovimentoEstoqueMateriaService movService): base(baseService)
        {
            _baseService = baseService;
            _movService = movService;
        }

        public List<MATERIA_PRIMA> GetAllItens()
        {
            List<MATERIA_PRIMA> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<MATERIA_PRIMA> GetAllItensAdm()
        {
            List<MATERIA_PRIMA> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public MATERIA_PRIMA GetItemById(Int32 id)
        {
            MATERIA_PRIMA item = _baseService.GetItemById(id);
            return item;
        }

        public MATERIA_PRIMA GetByNome(String nome)
        {
            MATERIA_PRIMA item = _baseService.GetByNome(nome);
            return item;
        }

        public MATERIA_PRIMA CheckExist(MATERIA_PRIMA conta)
        {
            MATERIA_PRIMA item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_MATERIA> GetAllTipos()
        {
            List<CATEGORIA_MATERIA> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<SUBCATEGORIA_MATERIA> GetAllTiposSub()
        {
            List<SUBCATEGORIA_MATERIA> lista = _baseService.GetAllTiposSub();
            return lista;
        }

        public List<UNIDADE> GetAllUnidades()
        {
            List<UNIDADE> lista = _baseService.GetAllUnidades();
            return lista;
        }

        public List<FILIAL> GetAllFilial()
        {
            List<FILIAL> lista = _baseService.GetAllFilial();
            return lista;
        }

        public MATERIA_PRIMA_ANEXO GetAnexoById(Int32 id)
        {
            MATERIA_PRIMA_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, String nome, String descricao, String codigo, out List<MATERIA_PRIMA> objeto)
        {
            try
            {
                objeto = new List<MATERIA_PRIMA>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, nome, descricao, codigo);
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

        public Int32 ValidateCreate(MATERIA_PRIMA item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.MAPR_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;
                item.MAPR_QN_ESTOQUE = item.MAPR_QN_QUANTIDADE_INICIAL;
                item.MAPR_DT_ULTIMA_MOVIMENTACAO = DateTime.Today;
                MOVIMENTO_ESTOQUE_MATERIA_PRIMA movto = new MOVIMENTO_ESTOQUE_MATERIA_PRIMA();

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddMAPR",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<MATERIA_PRIMA>(item)
                };

                // Persiste insumo
                Int32 volta = _baseService.Create(item, log, movto);

                // Monta movimento estoque
                movto.ASSI_CD_ID = usuario.ASSI_CD_ID;
                movto.FILI_CD_ID = item.FILI_CD_ID;
                movto.MATR_CD_ID = item.MATR_CD_ID;
                movto.MOEM_DT_MOVIMENTO = DateTime.Today;
                movto.MOEM_IN_ATIVO = 1;
                movto.MOEM_IN_CHAVE_ORIGEM = item.MAPR_CD_ID;
                movto.MOEM_NM_ORIGEM = "MAPR";
                movto.MOEM_IN_TIPO_MOVIMENTO = 1;
                movto.MOEM_QN_QUANTIDADE = item.MAPR_QN_QUANTIDADE_INICIAL;
                movto.MAPR_CD_ID = item.MAPR_CD_ID;
                movto.USUA_CD_ID = usuario.USUA_CD_ID;

                // Persiste estoque
                volta = _movService.Create(movto);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(MATERIA_PRIMA item, MATERIA_PRIMA itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditMAPR",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<MATERIA_PRIMA>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<MATERIA_PRIMA>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(MATERIA_PRIMA item, MATERIA_PRIMA itemAntes)
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

        public Int32 ValidateDelete(MATERIA_PRIMA item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial
                if (item.INVENTARIO_ITEM.Count > 0)
                {
                    return 1;
                }
                if (item.ITEM_PEDIDO_COMPRA.Count > 0)
                {
                    return 1;
                }
                if (item.MOVIMENTO_ESTOQUE_MATERIA_PRIMA.Count > 0)
                {
                    return 1;
                }

                // Acerta campos
                item.MAPR_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelMAPR",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<MATERIA_PRIMA>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(MATERIA_PRIMA item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.MAPR_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatMAPR",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<MATERIA_PRIMA>(item)
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
