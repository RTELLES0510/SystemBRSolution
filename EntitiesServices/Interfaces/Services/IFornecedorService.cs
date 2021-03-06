using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IFornecedorService : IServiceBase<FORNECEDOR>
    {
        Int32 Create(FORNECEDOR perfil, LOG log);
        Int32 Create(FORNECEDOR perfil);
        Int32 Edit(FORNECEDOR perfil, LOG log);
        Int32 Edit(FORNECEDOR perfil);
        Int32 Delete(FORNECEDOR perfil, LOG log);
        FORNECEDOR CheckExist(FORNECEDOR conta);
        FORNECEDOR GetItemById(Int32 id);
        FORNECEDOR GetByEmail(String email);
        List<FORNECEDOR> GetAllItens();
        List<FORNECEDOR> GetAllItensAdm();
        List<CATEGORIA_FORNECEDOR> GetAllTipos();
        List<FILIAL> GetAllFilial();
        FORNECEDOR_ANEXO GetAnexoById(Int32 id);
        List<FORNECEDOR> ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede);
        List<TIPO_PESSOA> GetAllTiposPessoa();
        FORNECEDOR_CONTATO GetContatoById(Int32 id);
        Int32 EditContato(FORNECEDOR_CONTATO item);
        Int32 CreateContato(FORNECEDOR_CONTATO item);
    }
}
