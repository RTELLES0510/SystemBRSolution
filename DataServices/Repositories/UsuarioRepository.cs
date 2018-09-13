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
    public class UsuarioRepository : RepositoryBase<USUARIO>, IUsuarioRepository
    {
        public USUARIO GetByEmail(String email)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<USUARIO> query = Db.USUARIO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_NM_EMAIL == email);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.COLABORADOR);
            query = query.Include(p => p.COLABORADOR.CARGO);
            query = query.Include(p => p.PERFIL);
            query = query.Include(p => p.LOG);
            return query.FirstOrDefault();
        }

        public USUARIO GetItemById(Int32 id)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<USUARIO> query = Db.USUARIO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_CD_ID == id);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.COLABORADOR);
            query = query.Include(p => p.COLABORADOR.CARGO);
            query = query.Include(p => p.PERFIL);
            query = query.Include(p => p.LOG);
            return query.FirstOrDefault();
        }

        public List<USUARIO> GetAllItens()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<USUARIO> query = Db.USUARIO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.USUA_IN_BLOQUEADO == 0);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.COLABORADOR);
            query = query.Include(p => p.COLABORADOR.CARGO);
            query = query.Include(p => p.PERFIL);
            return query.ToList();
        }

        public List<USUARIO> GetAllUsuariosAdm()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<USUARIO> query = Db.USUARIO.Where(p => p.USUA_IN_ATIVO == 1);
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.COLABORADOR);
            query = query.Include(p => p.COLABORADOR.CARGO);
            query = query.Include(p => p.PERFIL);
            return query.ToList();
        }

        public List<USUARIO> GetAllUsuarios()
        {
            Int32? idAss = SessionMocks.IdAssinante;
            IQueryable<USUARIO> query = Db.USUARIO;
            query = query.Where(p => p.ASSI_CD_ID == idAss);
            query = query.Include(p => p.ASSINANTE);
            query = query.Include(p => p.COLABORADOR);
            query = query.Include(p => p.COLABORADOR.CARGO);
            query = query.Include(p => p.PERFIL);
            return query.ToList();
        }

        public List<USUARIO> ExecuteFilter(Int32? perfilId, String nome, String cpf, String email)
        {
            Int32? idAss = SessionMocks.IdAssinante;
            List<USUARIO> lista = new List<USUARIO>();
            IQueryable<USUARIO> query = Db.USUARIO;
            if (!String.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.USUA_NM_EMAIL == email);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.COLABORADOR.COLA_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(cpf))
            {
                cpf = ValidarNumerosDocumentos.RemoveNaoNumericos(cpf);
                query = query.Where(p => p.COLABORADOR.COLA_NR_CPF == cpf);
            }
            if (perfilId != 0)
            {
                query = query.Where(p => p.PERFIL.PERF_CD_ID == perfilId);
            }
            if (query != null)
            {
                query = query.Where(p => p.ASSI_CD_ID == idAss);
                query = query.OrderBy(a => a.USUA_NM_EMAIL);
                lista = query.ToList<USUARIO>();
            }
            return lista;
        }
    }
}
