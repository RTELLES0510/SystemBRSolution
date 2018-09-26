using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IEquipamentoAppService : IAppServiceBase<EQUIPAMENTO>
    {
        Int32 ValidateCreate(EQUIPAMENTO perfil, USUARIO usuario);
        Int32 ValidateEdit(EQUIPAMENTO perfil, EQUIPAMENTO perfilAntes, USUARIO usuario);
        Int32 ValidateEdit(EQUIPAMENTO item, EQUIPAMENTO itemAntes);
        Int32 ValidateDelete(EQUIPAMENTO perfil, USUARIO usuario);
        Int32 ValidateReativar(EQUIPAMENTO perfil, USUARIO usuario);
        List<EQUIPAMENTO> GetAllItens();
        List<EQUIPAMENTO> GetAllItensAdm();
        EQUIPAMENTO GetItemById(Int32 id);
        EQUIPAMENTO GetByNumero(String numero);
        EQUIPAMENTO CheckExist(EQUIPAMENTO conta);
        List<CATEGORIA_EQUIPAMENTO> GetAllTipos();
        List<FILIAL> GetAllFilial();
        EQUIPAMENTO_ANEXO GetAnexoById(Int32 id);
        Int32 ExecuteFilter(Int32? catId, String nome, String numero, Int32? filiId, out List<EQUIPAMENTO> objeto);
        Int32 CalcularDiasDepreciacao(EQUIPAMENTO item);
        Int32 CalcularManutencaoVencida();
        Int32 CalcularDepreciados();
        Int32 CalcularDiasManutencao(EQUIPAMENTO item);
        List<PERIODICIDADE> GetAllPeriodicidades();
        EQUIPAMENTO_MANUTENCAO GetItemManutencaoById(Int32 id);
    }
}
