using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<USUARIO>
    {
        USUARIO GetByEmail(String email);
        USUARIO GetItemById(Int32 id);
        List<USUARIO> GetAllUsuarios();
        List<USUARIO> GetAllItens();
        List<USUARIO> GetAllUsuariosAdm();
        List<USUARIO> ExecuteFilter(Int32? perfilId, String nome, String cpf, String email);
    }
}
