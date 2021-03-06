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
    public class FornecedorRepository : RepositoryBase<FORNECEDOR>, IFornecedorRepository
    {
        public FORNECEDOR CheckExist(FORNECEDOR conta)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR;
            query = query.Where(p => p.FORN_NM_NOME == conta.FORN_NM_NOME);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            return query.FirstOrDefault();
        }

        public FORNECEDOR GetByEmail(String email)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR.Where(p => p.FORN_IN_ATIVO == 1);
            query = query.Where(p => p.FORN_NM_EMAIL == email);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public FORNECEDOR GetItemById(Int32 id)
        {
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR;
            query = query.Where(p => p.FORN_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.FirstOrDefault();
        }

        public List<FORNECEDOR> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR.Where(p => p.FORN_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<FORNECEDOR> GetAllItensAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.MATRIZ);
            query = query.Include(p => p.FILIAL);
            return query.ToList();
        }

        public List<FORNECEDOR> ExecuteFilter(Int32? catId, String nome, String cpf, String cnpj, String email, String cidade, String uf, String rede)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<FORNECEDOR> lista = new List<FORNECEDOR>();
            IQueryable<FORNECEDOR> query = Db.FORNECEDOR;
            if (catId != null)
            {
                query = query.Where(p => p.CATEGORIA_FORNECEDOR.CAFO_CD_ID == catId);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.FORN_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(cpf))
            {
                cpf = ValidarNumerosDocumentos.RemoveNaoNumericos(cpf);
                query = query.Where(p => p.FORN_NR_CPF == cpf);
            }
            if (!String.IsNullOrEmpty(cnpj))
            {
                cnpj = ValidarNumerosDocumentos.RemoveNaoNumericos(cnpj);
                query = query.Where(p => p.FORN_NR_CNPJ == cnpj);
            }
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.FORN_NM_EMAIL.Contains(email));
            }
            if (!String.IsNullOrEmpty(cidade))
            {
                query = query.Where(p => p.FORN_NM_CIDADE.Contains(cidade));
            }
            if (!String.IsNullOrEmpty(uf))
            {
                query = query.Where(p => p.FORN_SG_UF ==uf);
            }
            if (!String.IsNullOrEmpty(rede))
            {
                query = query.Where(p => p.FORN_NM_REDES_SOCIAIS.Contains(rede));
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.FORN_NM_NOME);
                lista = query.ToList<FORNECEDOR>();
            }
            return lista;
        }
    }
}
