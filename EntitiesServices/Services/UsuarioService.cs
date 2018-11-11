using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class UsuarioService : ServiceBase<USUARIO>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfRepository;
        private readonly ITemplateRepository _tempRepository;
        private readonly ILogRepository _logRepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;
        private readonly INotificacaoRepository _notRepository;
        private readonly INoticiaRepository _ntcRepository;
        protected SystemBRDatabaseEntities Db = new SystemBRDatabaseEntities();

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogRepository logRepository, IConfiguracaoRepository configuracaoRepository, IPerfilRepository perfRepository, ITemplateRepository tempRepository, INotificacaoRepository notRepository, INoticiaRepository ntcRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _logRepository = logRepository;
            _configuracaoRepository = configuracaoRepository;
            _perfRepository = perfRepository;
            _tempRepository = tempRepository;
            _notRepository = notRepository;
            _ntcRepository = ntcRepository;
        }

        public USUARIO RetriveUserByEmail(String email)
        {
            USUARIO usuario = _usuarioRepository.GetByEmail(email);
            return usuario;
        }

        public Boolean VerificarCredenciais (String senha, USUARIO usuario)
        {
            // Criptografa senha informada
            //String senhaCrip = Cryptography.Encode(senha);
            string senhaCrip = senha;

            // verifica senha
            if (usuario.USUA_NM_SENHA.Trim() != senhaCrip.Trim())
            {
                return false;
            }
            return true;
        }

        public USUARIO GetByEmail(String email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        public USUARIO GetItemById(Int32 id)
        {
            return _usuarioRepository.GetItemById(id);
        }

        public TEMPLATE GetTemplate(String code)
        {
            return _tempRepository.GetByCode(code);
        }

        public List<USUARIO> GetAllUsuariosAdm()
        {
            return _usuarioRepository.GetAllUsuariosAdm();
        }

        public List<USUARIO> GetAllUsuarios()
        {
            return _usuarioRepository.GetAllUsuarios();
        }

        public List<USUARIO> GetAllItens()
        {
            return _usuarioRepository.GetAllItens();
        }

        public Int32 CreateUser(USUARIO usuario, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _logRepository.Add(log);
                    _usuarioRepository.Add(usuario);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 CreateUser(USUARIO usuario)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _usuarioRepository.Add(usuario);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 EditUser(USUARIO usuario, LOG log)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    USUARIO obj = _usuarioRepository.GetById(usuario.USUA_CD_ID);
                    _usuarioRepository.Detach(obj);
                    _logRepository.Add(log);
                    _usuarioRepository.Update(usuario);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 EditUser(USUARIO usuario)
        {
            try
            {
                USUARIO obj = _usuarioRepository.GetById(usuario.USUA_CD_ID);
                _usuarioRepository.Detach(obj);
                _usuarioRepository.Update(usuario);
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int32 VerifyUserSubscription(USUARIO usuario)
        {
            return 0;
        }

        public Endereco GetAdressCEP(string CEP)
        {
            Endereco endereco = null;
            return endereco;
        }

        public CONFIGURACAO CarregaConfiguracao()
        {
            CONFIGURACAO conf = _configuracaoRepository.GetById(1);
            return conf;
        }

        public List<USUARIO> ExecuteFilter(Int32? perfilId, String nome, String cpf, String email)
        {
            List<USUARIO> lista = _usuarioRepository.ExecuteFilter(perfilId, nome, cpf, email);
            return lista;
        }

        public List<PERFIL> GetAllPerfis()
        {
            List<PERFIL> lista = _perfRepository.GetAll().ToList();
            return lista;
        }

        public List<NOTIFICACAO> GetAllItensUser(Int32 id)
        {
            return _notRepository.GetAllItensUser(id);
        }

        public List<NOTIFICACAO> GetNotificacaoNovas(Int32 id)
        {
            return _notRepository.GetNotificacaoNovas(id);
        }

        public List<NOTICIA> GetAllNoticias()
        {
            return _ntcRepository.GetAllItens();
        }

    }
}
