using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase<USUARIO>
    {
        USUARIO GetByEmail(String email);
        List<USUARIO> GetAllUsuariosAdm();
        USUARIO GetItemById(Int32 id);
        List<USUARIO> GetAllUsuarios();
        List<USUARIO> GetAllItens();
        Int32 ValidateCreate(USUARIO usuario, USUARIO usuarioLogado);
        Int32 ValidateEdit(USUARIO usuario, USUARIO usuarioAntes, USUARIO usuarioLogado);
        Int32 ValidateLogin(String email, String senha, out USUARIO usuario);
        Int32 ValidateDelete(USUARIO usuario, USUARIO usuarioLogado);
        Int32 ValidateBloqueio(USUARIO usuario, USUARIO usuarioLogado);
        Int32 ValidateDesbloqueio(USUARIO usuario, USUARIO usuarioLogado);
        Int32 ValidateChangePassword(USUARIO usuario);
        Int32 ValidateReativar(USUARIO usuario, USUARIO usuarioLogado);
        Int32 GenerateNewPassword(String email);
        List<PERFIL> GetAllPerfis();
        Int32 ExecuteFilter(Int32? perfilId, String nome, String cpf, String email, out List<USUARIO> objeto);
        List<NOTIFICACAO> GetAllItensUser(Int32 id);
        List<NOTIFICACAO> GetNotificacaoNovas(Int32 id);
        List<NOTICIA> GetAllNoticias();
    }
}
