using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IContratoService : IServiceBase<CONTRATO>
    {
        Int32 Create(CONTRATO perfil, LOG log);
        Int32 Create(CONTRATO perfil);
        Int32 Edit(CONTRATO perfil, LOG log);
        Int32 Edit(CONTRATO perfil);
        Int32 Delete(CONTRATO perfil, LOG log);
        CONTRATO CheckExist(CONTRATO conta);
        CONTRATO GetItemById(Int32 id);
        CONTRATO GetByNome(String nome);
        List<CONTRATO> GetAllItens();
        List<CONTRATO> GetAllItensAdm();
        List<CATEGORIA_CONTRATO> GetAllCategorias();
        List<TIPO_CONTRATO> GetAllTipos();
        List<TEMPLATE> GetAllTemplates();
        List<PERIODICIDADE> GetAllPeriodicidades();
        List<FORMA_PAGAMENTO> GetAllForma();
        List<PLANO_CONTA> GetAllPlanoConta();
        List<CENTRO_CUSTO> GetAllCentros();
        List<COLABORADOR> GetAllVendedores();
        List<NOMENCLATURA_BRAS_SERVICOS> GetAllNomenclatura();
        List<CLIENTE> GetAllClientes();
        CONTRATO_ANEXO GetAnexoById(Int32 id);
        List<CONTRATO> ExecuteFilter(Int32? catId, Int32? tipoId, String nome, String descricao);
    }
}
