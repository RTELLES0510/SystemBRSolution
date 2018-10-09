using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IProdutoService : IServiceBase<PRODUTO>
    {
        Int32 Create(PRODUTO perfil, LOG log, MOVIMENTO_ESTOQUE_PRODUTO movto);
        Int32 Create(PRODUTO perfil);
        Int32 Edit(PRODUTO perfil, LOG log);
        Int32 Edit(PRODUTO perfil);
        Int32 Delete(PRODUTO perfil, LOG log);
        PRODUTO CheckExist(PRODUTO conta);
        PRODUTO GetItemById(Int32 id);
        PRODUTO GetByNome(String nome);
        List<PRODUTO> GetAllItens();
        List<PRODUTO> GetAllItensAdm();
        List<CATEGORIA_PRODUTO> GetAllTipos();
        List<UNIDADE> GetAllUnidades();
        List<FILIAL> GetAllFilial();
        PRODUTO_ANEXO GetAnexoById(Int32 id);
        List<PRODUTO> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId);
        List<SUBCATEGORIA_PRODUTO> GetAllSubcategorias(Int32 cat);
        PRODUTO_FORNECEDOR GetFornecedorById(Int32 id);
        PRODUTO_GRADE GetGradeById(Int32 id);
        Int32 EditFornecedor(PRODUTO_FORNECEDOR item);
        Int32 CreateFornecedor(PRODUTO_FORNECEDOR item);
        Int32 EditGrade(PRODUTO_GRADE item);
        Int32 CreateGrade(PRODUTO_GRADE item);
        List<TAMANHO> GetAllTamanhos();
    }
}
