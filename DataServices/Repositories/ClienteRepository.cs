using System;
using System.Collections.Generic;
using EntitiesServices.Model;  
using ModelServices.Interfaces.Repositories;
using System.Linq;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using CrossCutting;

namespace DataServices.Repositories
{
    public class ClienteRepository : RepositoryBase<CLIENTE>, IClienteRepository
    {
        public CLIENTE CheckExist(CLIENTE conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CLIENTE> query = Db.CLIENTE;
            query = query.Where(p => p.CLIE_NM_NOME == conta.CLIE_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public CLIENTE GetByEmail(String email)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CLIENTE> query = Db.CLIENTE.Where(p => p.CLIE_IN_ATIVO == 1);
            query = query.Where(p => p.CLIE_NM_EMAIL == email);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public CLIENTE GetItemById(Int32 id)
        {
            IQueryable<CLIENTE> query = Db.CLIENTE;
            query = query.Where(p => p.CLIE_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<CLIENTE> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CLIENTE> query = Db.CLIENTE.Where(p => p.CLIE_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<CLIENTE> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<CLIENTE> query = Db.CLIENTE;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<CLIENTE> ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<CLIENTE> lista = new List<CLIENTE>();
            IQueryable<CLIENTE> query = Db.CLIENTE;
            if (catId != 0)
            {
                query = query.Where(p => p.CATEGORIA_CLIENTE.CACL_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.CLIE_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(cpf))
            {
                cpf = ValidarNumerosDocumentos.RemoveNaoNumericos(cpf);
                query = query.Where(p => p.CLIE_NR_CPF == cpf);
            }
            if (!String.IsNullOrEmpty(cnpj))
            {
                cnpj = ValidarNumerosDocumentos.RemoveNaoNumericos(cnpj);
                query = query.Where(p => p.CLIE_NR_CNPJ == cnpj);
            }
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.CLIE_NM_EMAIL.Contains(email));
            }
            if (!String.IsNullOrEmpty(cidade))
            {
                query = query.Where(p => p.CLIE_NM_CIDADE.Contains(cidade));
            }
            if (!String.IsNullOrEmpty(uf))
            {
                query = query.Where(p => p.CLIE_SG_UF ==uf);
            }
            if (!String.IsNullOrEmpty(rede))
            {
                query = query.Where(p => p.CLIE_NM_REDES_SOCIAIS.Contains(rede));
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.CLIE_NM_NOME);
                lista = query.ToList<CLIENTE>();
            }
            return lista;
        }
    }
}
