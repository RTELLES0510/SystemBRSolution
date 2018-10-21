using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IContratoAppService : IAppServiceBase<CONTRATO>
    {
        Int32 ValidateCreate(CONTRATO item, USUARIO usuario);
        Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes, USUARIO usuario);
        Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes);
        Int32 ValidateDelete(CONTRATO item, USUARIO usuario);
        Int32 ValidateReativar(CONTRATO item, USUARIO usuario);
        List<CONTRATO> GetAllItens();
        List<CONTRATO> GetAllItensOperacao();
        List<CONTRATO> GetAllItensAdm();
        CONTRATO GetItemById(Int32 id);
        CONTRATO GetByNome(String nome);
        CONTRATO CheckExist(CONTRATO item);
        CONTRATO_ANEXO GetAnexoById(Int32 id);
        List<CATEGORIA_CONTRATO> GetAllCategorias();
        List<TIPO_CONTRATO> GetAllTipos();
        List<TEMPLATE> GetAllTemplates();
        List<PERIODICIDADE> GetAllPeriodicidades();
        List<FORMA_PAGAMENTO> GetAllForma();
        List<PLANO_CONTA> GetAllPlanoConta();
        List<CENTRO_CUSTO> GetAllCentros();
        List<COLABORADOR> GetAllVendedores();
        List<COLABORADOR> GetAllResponsaveis();
        List<NOMENCLATURA_BRAS_SERVICOS> GetAllNomenclatura();
        List<STATUS_CONTRATO> GetAllStatus();
        List<CLIENTE> GetAllClientes();
        Int32 ExecuteFilter(Int32? catId, Int32? tipoId, Int32? statId, String nome, String descricao, out List<CONTRATO> objeto);
        String GetTextoAprovacao();
        COLABORADOR GetResponsavelById(Int32 id);
        Int32 EmitirAprovacaoContrato(CONTRATO item, USUARIO usuario);
        Int32 CancelarContrato(CONTRATO item, USUARIO usuario);
        Int32 ValidateRespostaAprovacao(CONTRATO item, CONTRATO itemAntes, CONTRATO_SOLICITACAO_APROVACAO resposta, USUARIO usuario);
    }
}
