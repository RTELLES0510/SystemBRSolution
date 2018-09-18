using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IMateriaPrimaAppService : IAppServiceBase<MATERIA_PRIMA>
    {
        Int32 ValidateCreate(MATERIA_PRIMA perfil, USUARIO usuario);
        Int32 ValidateEdit(MATERIA_PRIMA perfil, MATERIA_PRIMA perfilAntes, USUARIO usuario);
        Int32 ValidateEdit(MATERIA_PRIMA item, MATERIA_PRIMA itemAntes);
        Int32 ValidateDelete(MATERIA_PRIMA perfil, USUARIO usuario);
        Int32 ValidateReativar(MATERIA_PRIMA perfil, USUARIO usuario);
        List<MATERIA_PRIMA> GetAllItens();
        List<MATERIA_PRIMA> GetAllItensAdm();
        MATERIA_PRIMA GetItemById(Int32 id);
        MATERIA_PRIMA GetByNome(String nome);
        MATERIA_PRIMA CheckExist(MATERIA_PRIMA conta);
        List<CATEGORIA_MATERIA> GetAllTipos();
        List<UNIDADE> GetAllUnidades();
        List<FILIAL> GetAllFilial();
        MATERIA_PRIMA_ANEXO GetAnexoById(Int32 id);
        Int32 ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId, out List<MATERIA_PRIMA> objeto);
    }
}
