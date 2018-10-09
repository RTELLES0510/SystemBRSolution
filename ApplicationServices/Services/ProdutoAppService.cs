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
    public class ProdutoAppService : AppServiceBase<PRODUTO>, IProdutoAppService
    {
        private readonly IProdutoService _baseService;
        private readonly IMovimentoEstoqueProdutoService _movService;

        public ProdutoAppService(IProdutoService baseService, IMovimentoEstoqueProdutoService movService): base(baseService)
        {
            _baseService = baseService;
            _movService = movService;
        }

        public List<PRODUTO> GetAllItens()
        {
            List<PRODUTO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<PRODUTO> GetAllItensAdm()
        {
            List<PRODUTO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public PRODUTO GetItemById(Int32 id)
        {
            PRODUTO item = _baseService.GetItemById(id);
            return item;
        }

        public PRODUTO GetByNome(String nome)
        {
            PRODUTO item = _baseService.GetByNome(nome);
            return item;
        }

        public PRODUTO CheckExist(PRODUTO conta)
        {
            PRODUTO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_PRODUTO> GetAllTipos()
        {
            List<CATEGORIA_PRODUTO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<TAMANHO> GetAllTamanhos()
        {
            List<TAMANHO> lista = _baseService.GetAllTamanhos();
            return lista;
        }

        public List<SUBCATEGORIA_PRODUTO> GetAllSubcategorias(Int32 cat)
        {
            List<SUBCATEGORIA_PRODUTO> lista = _baseService.GetAllSubcategorias(cat);
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

        public PRODUTO_ANEXO GetAnexoById(Int32 id)
        {
            PRODUTO_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public PRODUTO_FORNECEDOR GetFornecedorById(Int32 id)
        {
            PRODUTO_FORNECEDOR lista = _baseService.GetFornecedorById(id);
            return lista;
        }

        public PRODUTO_GRADE GetGradeById(Int32 id)
        {
            PRODUTO_GRADE lista = _baseService.GetGradeById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId, out List<PRODUTO> objeto)
        {
            try
            {
                objeto = new List<PRODUTO>();
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

        public Int32 ValidateCreate(PRODUTO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.PROD_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;
                item.PROD_QN_ESTOQUE = item.PROD_QN_QUANTIDADE_INICIAL;
                item.PROD_DT_ULTIMA_MOVIMENTACAO = DateTime.Today;
                MOVIMENTO_ESTOQUE_PRODUTO movto = new MOVIMENTO_ESTOQUE_PRODUTO();

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddPROD",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRODUTO>(item)
                };

                // Persiste produto
                Int32 volta = _baseService.Create(item, log, movto);

                // Monta movimento estoque
                movto.ASSI_CD_ID = usuario.ASSI_CD_ID;
                movto.FILI_CD_ID = item.FILI_CD_ID;
                movto.MATR_CD_ID = item.MATR_CD_ID;
                movto.MOEP_DT_MOVIMENTO = DateTime.Today;
                movto.MOEP_IN_ATIVO = 1;
                movto.MOEP_IN_CHAVE_ORIGEM = item.PROD_CD_ID;
                movto.MOEP_IN_ORIGEM = "PROD";
                movto.MOEP_IN_TIPO_MOVIMENTO = 1;
                movto.MOEP_QN_QUANTIDADE = item.PROD_QN_QUANTIDADE_INICIAL;
                movto.PROD_CD_ID = item.PROD_CD_ID;
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

        public Int32 ValidateEdit(PRODUTO item, PRODUTO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditPROD",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRODUTO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<PRODUTO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(PRODUTO item, PRODUTO itemAntes)
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

        public Int32 ValidateDelete(PRODUTO item, USUARIO usuario)
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
                    return 2;
                }
                if (item.ITEM_PEDIDO_VENDA.Count > 0)
                {
                    return 2;
                }
                if (item.ITEM_PROPOSTA_VENDA.Count > 0)
                {
                    return 2;
                }
                if (item.MOVIMENTO_ESTOQUE_PRODUTO.Count > 0)
                {
                    return 2;
                }
                if (item.PRECO_PRODUTO.Count > 0)
                {
                    return 2;
                }

                // Acerta campos
                item.PROD_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelPROD",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRODUTO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(PRODUTO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.PROD_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatPROD",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<PRODUTO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEditFornecedor(PRODUTO_FORNECEDOR item)
        {
            try
            {
                // Persiste
                return _baseService.EditFornecedor(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateFornecedor(PRODUTO_FORNECEDOR item)
        {
            try
            {
                // Persiste
                Int32 volta = _baseService.CreateFornecedor(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEditGrade(PRODUTO_GRADE item)
        {
            try
            {
                // Persiste
                return _baseService.EditGrade(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateCreateGrade(PRODUTO_GRADE item)
        {
            try
            {
                // Persiste
                Int32 volta = _baseService.CreateGrade(item);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
