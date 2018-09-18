using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IMateriaPrimaService : IServiceBase<MATERIA_PRIMA>
    {
        Int32 Create(MATERIA_PRIMA perfil, LOG log, MOVIMENTO_ESTOQUE_MATERIA_PRIMA movto);
        Int32 Create(MATERIA_PRIMA perfil);
        Int32 Edit(MATERIA_PRIMA perfil, LOG log);
        Int32 Edit(MATERIA_PRIMA perfil);
        Int32 Delete(MATERIA_PRIMA perfil, LOG log);
        MATERIA_PRIMA CheckExist(MATERIA_PRIMA conta);
        MATERIA_PRIMA GetItemById(Int32 id);
        MATERIA_PRIMA GetByNome(String nome);
        List<MATERIA_PRIMA> GetAllItens();
        List<MATERIA_PRIMA> GetAllItensAdm();
        List<CATEGORIA_MATERIA> GetAllTipos();
        List<UNIDADE> GetAllUnidades();
        List<FILIAL> GetAllFilial();
        MATERIA_PRIMA_ANEXO GetAnexoById(Int32 id);
        List<MATERIA_PRIMA> ExecuteFilter(Int32? catId, String nome, String descricao, Int32? filiId);
    }
}
