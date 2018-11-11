using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class UsuarioAppService : AppServiceBase<USUARIO>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService): base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public USUARIO GetByEmail(String email)
        {
            return _usuarioService.GetByEmail(email);
        }

        public List<USUARIO> GetAllUsuariosAdm()
        {
            return _usuarioService.GetAllUsuariosAdm();
        }

        public USUARIO GetItemById(Int32 id)
        {
            return _usuarioService.GetItemById(id);
        }

        public List<USUARIO> GetAllUsuarios()
        {
            return _usuarioService.GetAllUsuarios();
        }

        public List<USUARIO> GetAllItens()
        {
            return _usuarioService.GetAllItens();
        }

        public Int32 ValidateCreate(USUARIO usuario, USUARIO usuarioLogado)
        {
            try
            {
                // Verifica senhas
                if (usuario.USUA_NM_SENHA != usuario.USUA_NM_SENHA_CONFIRMA)
                {
                    return 1;
                }

                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 2;
                }

                // Verifica existencia prévia
                if (_usuarioService.GetByEmail(usuario.USUA_NM_EMAIL) != null)
                {
                    return 3;
                }

                //Acerta campos de usuários
                usuario.USUA_IN_BLOQUEADO = 0;
                usuario.USUA_IN_PROVISORIA = 0;
                usuario.USUA_IN_LOGIN_PROVISORIO = 0;
                usuario.USUA_NR_ACESSOS = 0;
                usuario.USUA_NR_FALHAS = 0;
                usuario.USUA_DT_ALTERACAO = null;
                usuario.USUA_DT_BLOQUEIO = null;
                usuario.USUA_DT_TROCA_SENHA = null;
                usuario.USUA_DT_ACESSO = DateTime.Now;
                usuario.USUA_DT_CADASTRO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;
                usuario.USUA_DT_ULTIMA_FALHA = null;
                usuario.ASSI_CD_ID = SessionMocks.IdAssinante.Value;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "AddUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_IN_ATIVO = 1
                };


                // Persiste
                Int32 volta = _usuarioService.CreateUser(usuario, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(USUARIO usuario, USUARIO usuarioAntes, USUARIO usuarioLogado)
        {
            try
            {
                // Verifica Email
                if (!ValidarItensDiversos.IsValidEmail(usuario.USUA_NM_EMAIL))
                {
                    return 1;
                }

                // Verifica existencia prévia
                USUARIO usu = _usuarioService.GetByEmail(usuario.USUA_NM_EMAIL);
                if (usu != null)
                {
                    if (usu.USUA_CD_ID != usuario.USUA_CD_ID)
                    {
                        return 2;
                    }
                }

                //Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "EditUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<USUARIO>(usuarioAntes),
                    LOG_IN_ATIVO = 1
                };


                // Persiste
                Int32 volta = _usuarioService.EditUser(usuario);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(USUARIO usuario, USUARIO usuarioLogado)
        {
            try
            {
                // Verifica integridade

                // Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "DelUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(USUARIO usuario, USUARIO usuarioLogado)
        {
            try
            {
                // Verifica integridade

                // Acerta campos de usuários
                usuario.USUA_DT_ALTERACAO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "ReatUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateBloqueio(USUARIO usuario, USUARIO usuarioLogado)
        {
            try
            {
                //Acerta campos de usuários
                usuario.USUA_DT_BLOQUEIO = DateTime.Now;
                usuario.USUA_IN_BLOQUEADO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "BlqUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDesbloqueio(USUARIO usuario, USUARIO usuarioLogado)
        {
            try
            {
                //Acerta campos de usuários
                usuario.USUA_DT_BLOQUEIO = DateTime.Now;
                usuario.USUA_IN_BLOQUEADO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuarioLogado.USUA_CD_ID,
                    LOG_NM_OPERACAO = "DbqUSUA",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                    LOG_IN_ATIVO = 1
                };

                // Persiste
                return _usuarioService.EditUser(usuario, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateLogin(String email, String senha, out USUARIO usuario)
        {
            try
            {
                usuario = new USUARIO();
                // Checa e-mail
                if (!ValidarItensDiversos.IsValidEmail(email))
                {
                    return 1;
                }
                usuario = _usuarioService.RetriveUserByEmail(email);
                if (usuario == null)
                {
                    usuario = new USUARIO();
                    return 2;
                }

                // Verifica se está ativo
                if (usuario.USUA_IN_ATIVO != 1)
                {
                    return 3;
                }

                // Verifica se está bloqueado
                if (usuario.USUA_IN_BLOQUEADO == 1)
                {
                    return 4;
                }

                // verifica senha proviória
                if (usuario.USUA_IN_PROVISORIA == 1)
                {
                    if (usuario.USUA_IN_LOGIN_PROVISORIO == 0)
                    {
                        usuario.USUA_IN_LOGIN_PROVISORIO = 1;
                    }
                    else
                    {
                        return 5;
                    }
                }

                // Verifica senha
                Boolean retorno = _usuarioService.VerificarCredenciais(senha, usuario);
                if (!retorno)
                {
                    if (usuario.USUA_NR_FALHAS <= _usuarioService.CarregaConfiguracao().CONF_NR_FALHAS_DIA)
                    {
                        if (usuario.USUA_DT_ULTIMA_FALHA != DateTime.Now.Date)
                        {
                            usuario.USUA_DT_ULTIMA_FALHA = DateTime.Now.Date;
                            usuario.USUA_NR_FALHAS = 1;
                        }
                        else
                        {
                            usuario.USUA_NR_FALHAS++;
                        }
                    }
                    else if (usuario.USUA_NR_FALHAS > _usuarioService.CarregaConfiguracao().CONF_NR_FALHAS_DIA)
                    {
                        usuario.USUA_NR_ACESSOS = ++usuario.USUA_NR_ACESSOS;
                        usuario.USUA_DT_ACESSO = DateTime.Now.Date;
                        usuario.USUA_DT_BLOQUEIO = DateTime.Now.Date;
                        usuario.USUA_IN_BLOQUEADO = 1;
                        usuario.USUA_NR_FALHAS = 0;
                        usuario.USUA_DT_ULTIMA_FALHA = DateTime.Now.Date;
                        Int32 voltaBloqueio = _usuarioService.EditUser(usuario);
                        return 6;
                    }
                    return 7;
                }

                // Atualiza acessos e data do acesso
                usuario.USUA_NR_ACESSOS = ++usuario.USUA_NR_ACESSOS;
                usuario.USUA_DT_ACESSO = DateTime.Now.Date;
                Int32 voltaAcesso = _usuarioService.EditUser(usuario);
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateChangePassword(USUARIO usuario)
        {
            try
            {
                // Verifica se senha igual a anterior
                if (usuario.USUA_NM_SENHA == usuario.USUA_NM_NOVA_SENHA)
                {
                    return 1;
                }

                // Verifica se senha foi confirmada
                if (usuario.USUA_NM_NOVA_SENHA != usuario.USUA_NM_SENHA_CONFIRMA)
                {
                    return 2;
                }

                //Acerta campos 
                //usuario.USUA_NM_SENHA = Cryptography.Encode(usuario.USUA_NM_NOVA_SENHA);
                usuario.USUA_NM_SENHA = usuario.USUA_NM_NOVA_SENHA;
                usuario.USUA_DT_TROCA_SENHA = DateTime.Now.Date;
                usuario.USUA_IN_BLOQUEADO = 0;
                usuario.USUA_IN_PROVISORIA = 0;
                usuario.USUA_IN_LOGIN_PROVISORIO = 0;
                usuario.USUA_NR_ACESSOS = 0;
                usuario.USUA_NR_FALHAS = 0;
                usuario.USUA_DT_ALTERACAO = null;
                usuario.USUA_DT_BLOQUEIO = null;
                usuario.USUA_DT_TROCA_SENHA = null;
                usuario.USUA_DT_ACESSO = DateTime.Now;
                usuario.USUA_DT_CADASTRO = DateTime.Now;
                usuario.USUA_IN_ATIVO = 1;
                usuario.USUA_DT_ULTIMA_FALHA = null;

                //Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    LOG_NM_OPERACAO = "ChangePWD",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<USUARIO>(usuario),
                };

                // Persiste
                return _usuarioService.EditUser(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 GenerateNewPassword(String email)
        {
            // Checa email
            if (!ValidarItensDiversos.IsValidEmail(email))
            {
                return 1;
            }
            USUARIO usuario = _usuarioService.RetriveUserByEmail(email);
            if (usuario == null)
            {
                return 2;
            }

            // Verifica se usuário está ativo
            if (usuario.USUA_IN_ATIVO == 0)
            {
                return 3;
            }

            // Verifica se usuário não está bloqueado
            if (usuario.USUA_IN_BLOQUEADO == 1)
            {
                return 4;
            }

            // Gera nova senha
            String senha = Cryptography.GenerateRandomPassword(6);

            // Atauliza objeto
            //usuario.USUA_NM_SENHA = Cryptography.Encode(senha);
            usuario.USUA_NM_SENHA = senha;
            usuario.USUA_IN_PROVISORIA = 1;
            usuario.USUA_DT_ALTERACAO = DateTime.Now;
            usuario.USUA_DT_TROCA_SENHA = DateTime.Now;
            usuario.USUA_IN_LOGIN_PROVISORIO = 0;

            // Monta log
            LOG log = new LOG();
            log.LOG_DT_DATA = DateTime.Now;
            log.LOG_NM_OPERACAO = "NewPWD";
            log.LOG_TX_REGISTRO = senha;
            log.LOG_IN_ATIVO = 1;

            // Atualiza usuario
            Int32 volta = _usuarioService.EditUser(usuario);

            // Recupera template e-mail
            String body = _usuarioService.GetTemplate("NovaPWD").TEMP_TX_CONTEUDO;

            // Prepara corpo do e-mail
            body = body.Replace("{UserName}", usuario.ASSINANTE.ASSI_NM_EMAIL);
            body = body.Replace("{Senha}", senha);

            // Envia e-mail
            CONFIGURACAO conf = _usuarioService.CarregaConfiguracao();
            Email emailEnvio = new Email();
            emailEnvio.ASSUNTO = "Geração de Nova Senha";
            emailEnvio.CORPO = body;
            emailEnvio.DEFAULT_CREDENTIALS = false;
            emailEnvio.EMAIL_DESTINO = usuario.USUA_NM_EMAIL;
            emailEnvio.EMAIL_EMISSOR = conf.CONF_NM_EMAIL_EMISSOR;
            emailEnvio.ENABLE_SSL = false;
            emailEnvio.NOME_EMISSOR = conf.CONF_NM_EMAIL_EMISSOR;
            emailEnvio.PORTA = conf.CONF_NM_PORTA_SMTP;
            emailEnvio.PRIORIDADE = System.Net.Mail.MailPriority.Normal;
            emailEnvio.SENHA_EMISSOR = conf.CONF_NM_SENHA_EMISSOR;
            emailEnvio.SMTP = conf.CONF_NM_HOST_SMTP;
            Int32 voltaMail = CommunicationPackage.SendEmail(emailEnvio);
            if (voltaMail != 0)
            {
                return 5;
            }

            // Retorna sucesso
            return 0;
        }

        public Int32 ExecuteFilter(Int32? perfilId, String nome, String cpf, String email, out List<USUARIO> objeto)
        {
            try
            {
                objeto = new List<USUARIO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _usuarioService.ExecuteFilter(perfilId, nome, cpf, email);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PERFIL> GetAllPerfis()
        {
            List<PERFIL> lista = _usuarioService.GetAllPerfis();
            return lista;
        }

        public List<NOTIFICACAO> GetAllItensUser(Int32 id)
        {
            return _usuarioService.GetAllItensUser(id);
        }

        public List<NOTIFICACAO> GetNotificacaoNovas(Int32 id)
        {
            return _usuarioService.GetNotificacaoNovas(id);
        }

        public List<NOTICIA> GetAllNoticias()
        {
            return _usuarioService.GetAllNoticias();
        }
    }
}
