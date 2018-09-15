using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IFornecedorAppService : IAppServiceBase<FORNECEDOR>
    {
        Int32 ValidateCreate(FORNECEDOR perfil, USUARIO usuario);
        Int32 ValidateEdit(FORNECEDOR perfil, FORNECEDOR perfilAntes, USUARIO usuario);
        Int32 ValidateEdit(FORNECEDOR item, FORNECEDOR itemAntes);
        Int32 ValidateDelete(FORNECEDOR perfil, USUARIO usuario);
        Int32 ValidateReativar(FORNECEDOR perfil, USUARIO usuario);
        List<FORNECEDOR> GetAllItens();
        List<FORNECEDOR> GetAllItensAdm();
        FORNECEDOR GetItemById(Int32 id);
        FORNECEDOR GetByEmail(String email);
        FORNECEDOR CheckExist(FORNECEDOR conta);
        List<CATEGORIA_FORNECEDOR> GetAllTipos();
        List<FILIAL> GetAllFilial();
        FORNECEDOR_ANEXO GetAnexoById(Int32 id);
        Int32 ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede, out List<FORNECEDOR> objeto);
    }
}
