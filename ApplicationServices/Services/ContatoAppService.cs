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
    public class ContratoAppService : AppServiceBase<CONTRATO>, IContratoAppService
    {
        private readonly IContratoService _baseService;
        private readonly INotificacaoService _notiService;

        public ContratoAppService(IContratoService baseService, INotificacaoService notiService): base(baseService)
        {
            _baseService = baseService;
            _notiService = notiService;
        }

        public List<CONTRATO> GetAllItens()
        {
            List<CONTRATO> lista = _baseService.GetAllItens();
            return lista;
        }

        public List<CONTRATO> GetAllItensAdm()
        {
            List<CONTRATO> lista = _baseService.GetAllItensAdm();
            return lista;
        }

        public List<CONTRATO> GetAllItensOperacao()
        {
            List<CONTRATO> lista = _baseService.GetAllItensOperacao();
            return lista;
        }

        public CONTRATO GetItemById(Int32 id)
        {
            CONTRATO item = _baseService.GetItemById(id);
            return item;
        }

        public String GetTextoAprovacao()
        {
            String item = _baseService.GetTextoAprovacao();
            return item;
        }

        public COLABORADOR GetResponsavelById(Int32 id)
        {
            COLABORADOR item = _baseService.GetResponsavelById(id);
            return item;
        }


        public CONTRATO GetByNome(String nome)
        {
            CONTRATO item = _baseService.GetByNome(nome);
            return item;
        }

        public CONTRATO CheckExist(CONTRATO conta)
        {
            CONTRATO item = _baseService.CheckExist(conta);
            return item;
        }

        public List<CATEGORIA_CONTRATO> GetAllCategorias()
        {
            List<CATEGORIA_CONTRATO> lista = _baseService.GetAllCategorias();
            return lista;
        }

        public List<TIPO_CONTRATO> GetAllTipos()
        {
            List<TIPO_CONTRATO> lista = _baseService.GetAllTipos();
            return lista;
        }

        public List<STATUS_CONTRATO> GetAllStatus()
        {
            List<STATUS_CONTRATO> lista = _baseService.GetAllStatus();
            return lista;
        }

        public List<TEMPLATE> GetAllTemplates()
        {
            List<TEMPLATE> lista = _baseService.GetAllTemplates();
            return lista;
        }

        public List<PERIODICIDADE> GetAllPeriodicidades()
        {
            List<PERIODICIDADE> lista = _baseService.GetAllPeriodicidades();
            return lista;
        }

        public List<FORMA_PAGAMENTO> GetAllForma()
        {
            List<FORMA_PAGAMENTO> lista = _baseService.GetAllForma();
            return lista;
        }

        public List<PLANO_CONTA> GetAllPlanoConta()
        {
            List<PLANO_CONTA> lista = _baseService.GetAllPlanoConta();
            return lista;
        }

        public List<CENTRO_CUSTO> GetAllCentros()
        {
            List<CENTRO_CUSTO> lista = _baseService.GetAllCentros();
            return lista;
        }

        public List<COLABORADOR> GetAllVendedores()
        {
            List<COLABORADOR> lista = _baseService.GetAllVendedores();
            return lista;
        }

        public List<COLABORADOR> GetAllResponsaveis()
        {
            List<COLABORADOR> lista = _baseService.GetAllResponsaveis();
            return lista;
        }

        public List<NOMENCLATURA_BRAS_SERVICOS> GetAllNomenclatura()
        {
            List<NOMENCLATURA_BRAS_SERVICOS> lista = _baseService.GetAllNomenclatura();
            return lista;
        }

        public List<CLIENTE> GetAllClientes()
        {
            List<CLIENTE> lista = _baseService.GetAllClientes();
            return lista;
        }

        public CONTRATO_ANEXO GetAnexoById(Int32 id)
        {
            CONTRATO_ANEXO lista = _baseService.GetAnexoById(id);
            return lista;
        }

        public Int32 ExecuteFilter(Int32? catId, Int32? tipoId, Int32? statId, String nome, String descricao, out List<CONTRATO> objeto)
        {
            try
            {
                objeto = new List<CONTRATO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(catId, tipoId, statId, nome, descricao);
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

        public Int32 ValidateCreate(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica existencia prévia
                if (_baseService.CheckExist(item) != null)
                {
                    return 1;
                }

                // Completa objeto
                item.CONT_IN_ATIVO = 1;
                item.ASSI_CD_ID = usuario.ASSI_CD_ID;
                item.CONT_IN_WORKFLOW = 1;
                item.CONT_IN_APROVADO = 0;
                item.CONT_IN_PREPARADO = 0;
                item.CONT_IN_ENVIO_APROVACAO = 0;
                
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AddCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
                };

                // Persiste produto
                Int32 volta = _baseService.Create(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes, USUARIO usuario)
        {
            try
            {
                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "EditCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CONTRATO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateEdit(CONTRATO item, CONTRATO itemAntes)
        {
            try
            {
                // Persiste
                return _baseService.Edit(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateDelete(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CONT_IN_ATIVO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "DelCONT",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateReativar(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Verifica integridade referencial

                // Acerta campos
                item.CONT_IN_ATIVO = 1;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_IN_ATIVO = 1,
                    LOG_NM_OPERACAO = "ReatCONT",
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 EmitirAprovacaoContrato(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Ajustar objeto
                item.CONT_NM_RESPONSAVEL = _baseService.GetResponsavelById(item.CONT_CD_RESPONSAVEL.Value).COLA_NM_NOME;
                item.CONT_IN_ENVIO_APROVACAO = 1;
                item.CONT_IN_WORKFLOW = 1;
                item.CONT_IN_APROVADO = 0;
                item.CONT_IN_ATIVO = 1;
                item.CONT_IN_ENCERRADO = 0;
                item.CONT_IN_PREPARADO = 0;
                item.CONT_IN_CANCELADO = 0;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "AprovCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item),
                };

                // Gera Notificação
                NOTIFICACAO noti = new NOTIFICACAO();
                noti.CANO_CD_ID = 1;
                noti.ASSI_CD_ID = usuario.ASSI_CD_ID;
                noti.NOTI_DT_DATA = DateTime.Today;
                noti.NOTI_IN_ATIVO = 1;
                noti.NOTI_IN_ENVIADA = 1;
                noti.NOTI_IN_STATUS = 1;
                noti.NOTI_IN_TEXTO = "-";
                noti.USUA_CD_ID = _baseService.GetResponsavelById(item.CONT_CD_RESPONSAVEL.Value).USUA_CD_ID;

                // Monta e-mail
                Email mensagem = new Email();
                CONFIGURACAO conf = _baseService.CarregaConfiguracao(SessionMocks.IdAssinante.Value);
                mensagem.ASSUNTO = "Aprovação de Contrato - Solicitação";
                mensagem.CORPO = item.CONT_DS_APROVACAO;
                mensagem.DEFAULT_CREDENTIALS = false;
                mensagem.EMAIL_DESTINO = _baseService.GetResponsavelById(item.CONT_CD_RESPONSAVEL.Value).COLA_NM_NOME;
                mensagem.EMAIL_EMISSOR = conf.CONF_NM_EMAIL_EMISSOR;
                mensagem.ENABLE_SSL = false;
                mensagem.NOME_EMISSOR = usuario.COLABORADOR.COLA_NM_NOME;
                mensagem.PORTA = conf.CONF_NM_PORTA_SMTP;
                mensagem.PRIORIDADE = System.Net.Mail.MailPriority.High;
                mensagem.SENHA_EMISSOR = conf.CONF_NM_SENHA_EMISSOR;
                mensagem.SMTP = conf.CONF_NM_HOST_SMTP;

                // Envia mensagem
                //Int32 voltaMail = CommunicationPackage.SendEmail(mensagem);     

                // Persiste notificação
                Int32 volta = _notiService.Create(noti);

                // Persiste contrato
                volta = _baseService.Edit(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 CancelarContrato(CONTRATO item, USUARIO usuario)
        {
            try
            {
                // Ajustar objeto
                item.CONT_IN_WORKFLOW = 1;
                item.CONT_IN_APROVADO = 0;
                item.CONT_IN_ATIVO = 1;
                item.CONT_IN_ENCERRADO = 0;
                item.CONT_IN_PREPARADO = 0;
                item.CONT_IN_CANCELADO = 1;
                item.CONT_CD_RESP_CANCELAMENTO = usuario.COLABORADOR.COLA_CD_ID;

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "CancCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item),
                };

                // Gera Notificação
                NOTIFICACAO noti = new NOTIFICACAO();
                noti.CANO_CD_ID = 1;
                noti.ASSI_CD_ID = usuario.ASSI_CD_ID;
                noti.NOTI_DT_DATA = DateTime.Today;
                noti.NOTI_IN_ATIVO = 1;
                noti.NOTI_IN_ENVIADA = 1;
                noti.NOTI_IN_STATUS = 1;
                noti.NOTI_IN_TEXTO = "-";
                noti.USUA_CD_ID = _baseService.GetResponsavelById(item.CONT_CD_RESPONSAVEL.Value).USUA_CD_ID;

                // Monta e-mail
                Email mensagem = new Email();
                CONFIGURACAO conf = _baseService.CarregaConfiguracao(SessionMocks.IdAssinante.Value);
                mensagem.ASSUNTO = "Cancelamento de Contrato";
                mensagem.CORPO = item.CONT_DS_JUSTIFICATIVA;
                mensagem.DEFAULT_CREDENTIALS = false;
                mensagem.EMAIL_DESTINO = usuario.USUA_NM_EMAIL;
                mensagem.EMAIL_EMISSOR = conf.CONF_NM_EMAIL_EMISSOR;
                mensagem.ENABLE_SSL = false;
                mensagem.NOME_EMISSOR = usuario.COLABORADOR.COLA_NM_NOME;
                mensagem.PORTA = conf.CONF_NM_PORTA_SMTP;
                mensagem.PRIORIDADE = System.Net.Mail.MailPriority.High;
                mensagem.SENHA_EMISSOR = conf.CONF_NM_SENHA_EMISSOR;
                mensagem.SMTP = conf.CONF_NM_HOST_SMTP;

                // Envia mensagem
                //Int32 voltaMail = CommunicationPackage.SendEmail(mensagem);     

                // Persiste notificação
                Int32 volta = _notiService.Create(noti);

                // Persiste contrato
                volta = _baseService.Edit(item, log);
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Int32 ValidateRespostaAprovacao(CONTRATO item, CONTRATO itemAntes, CONTRATO_SOLICITACAO_APROVACAO resposta, USUARIO usuario)
        {
            try
            {
                // Acerta objeto
                resposta.CTSA_IN_ATIVO = 1;
                resposta.COLA_CD_ID = usuario.COLA_CD_ID.Value;
                resposta.CONT_CD_ID = item.CONT_CD_ID;
                item.CONTRATO_SOLICITACAO_APROVACAO.Add(resposta);

                // Monta Log
                LOG log = new LOG
                {
                    LOG_DT_DATA = DateTime.Now,
                    USUA_CD_ID = usuario.USUA_CD_ID,
                    ASSI_CD_ID = SessionMocks.IdAssinante,
                    LOG_NM_OPERACAO = "RespCONT",
                    LOG_IN_ATIVO = 1,
                    LOG_TX_REGISTRO = Serialization.SerializeJSON<CONTRATO>(item),
                    LOG_TX_REGISTRO_ANTES = Serialization.SerializeJSON<CONTRATO>(itemAntes)
                };

                // Persiste
                return _baseService.Edit(item, log);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
