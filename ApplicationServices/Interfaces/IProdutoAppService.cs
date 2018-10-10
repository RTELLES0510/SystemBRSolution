using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IProdutoAppService : IAppServiceBase<PRODUTO>
    {
        Int32 ValidateCreate(PRODUTO perfil, USUARIO usuario);
        Int32 ValidateCreateLeve(PRODUTO item, USUARIO usuario);
        Int32 ValidateEdit(PRODUTO perfil, PRODUTO perfilAntes, USUARIO usuario);
        Int32 ValidateEdit(PRODUTO item, PRODUTO itemAntes);
        Int32 ValidateDelete(PRODUTO perfil, USUARIO usuario);
        Int32 ValidateReativar(PRODUTO perfil, USUARIO usuario);
        List<PRODUTO> GetAllItens();
        List<PRODUTO> GetAllItensAdm();
        PRODUTO GetItemById(Int32 id);
        PRODUTO GetByNome(String nome);
        PRODUTO CheckExist(PRODUTO conta);
        List<CATEGORIA_PRODUTO> GetAllTipos();
        List<UNIDADE> GetAllUnidades();
        List<FILIAL> GetAllFilial();
        PRODUTO_ANEXO GetAnexoById(Int32 id);
        Int32 ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId, out List<PRODUTO> objeto);
        List<SUBCATEGORIA_PRODUTO> GetAllSubcategorias(Int32 cat);
        PRODUTO_FORNECEDOR GetFornecedorById(Int32 id);
        PRODUTO_GRADE GetGradeById(Int32 id);
        Int32 ValidateEditFornecedor(PRODUTO_FORNECEDOR item);
        Int32 ValidateCreateFornecedor(PRODUTO_FORNECEDOR item);
        Int32 ValidateEditGrade(PRODUTO_GRADE item);
        Int32 ValidateCreateGrade(PRODUTO_GRADE item);
        List<TAMANHO> GetAllTamanhos();
    }
}
