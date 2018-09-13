using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IClienteRepository : IRepositoryBase<CLIENTE>
    {
        CLIENTE CheckExist(CLIENTE item);
        CLIENTE GetByEmail(String email);
        CLIENTE GetItemById(Int32 id);
        List<CLIENTE> GetAllItens();
        List<CLIENTE> GetAllItensAdm();
        List<CLIENTE> ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede);
    }
}
