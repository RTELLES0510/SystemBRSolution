using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationServices.Interfaces;
using EntitiesServices.Model;
using System.Globalization;
using SystemBRPresentation.App_Start;
using EntitiesServices.Work_Classes;
using AutoMapper;
using SystemBRPresentation.ViewModels;
using System.IO;

namespace SystemBRPresentation.Controllers
{
    public class AdministracaoController : Controller
    {
        private readonly IUsuarioAppService baseApp;
        private readonly ILogAppService logApp;
        private String msg;
        private Exception exception;
        USUARIO objeto = new USUARIO();
        USUARIO objetoAntes = new USUARIO();
        List<USUARIO> listaMaster = new List<USUARIO>();
        LOG objLog = new LOG();
        LOG objLogAntes = new LOG();
        List<LOG> listaMasterLog = new List<LOG>();

        public AdministracaoController(IUsuarioAppService baseApps, ILogAppService logApps)
        {
            baseApp = baseApps;
            logApp = logApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            USUARIO item = new USUARIO();
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpGet]
        public ActionResult MontarTelaUsuario()
        {
            // Verifica se tem usuario logado
            USUARIO usuario = new USUARIO();
            if (SessionMocks.UserCredentials != null)
            {
                usuario = SessionMocks.UserCredentials;

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA != "ADM")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Carrega listas
            ViewBag.Perfis = new SelectList(baseApp.GetAllPerfis(), "PERF_CD_ID", "PERF_NM_NOME");
            if (SessionMocks.listaUsuario == null)
            {
                listaMaster = baseApp.GetAllItens();
                SessionMocks.listaUsuario = listaMaster;
            }
            ViewBag.Listas = SessionMocks.listaUsuario;
            ViewBag.Title = "Usuários";

            // Abre view
            objeto = new USUARIO();
            return View(objeto);
        }

        public ActionResult Voltar()
        {
            return RedirectToAction("CarregarAdmin", "BaseAdmin");
        }

        public ActionResult RetirarFiltro()
        {
            SessionMocks.listaUsuario = null;
            return RedirectToAction("MontarTelaUsuario");
        }

        public ActionResult MostrarTudo()
        {
            listaMaster = baseApp.GetAllUsuariosAdm();
            SessionMocks.listaUsuario = listaMaster;
            return RedirectToAction("MontarTelaUsuario");
        }

        [HttpPost]
        public ActionResult FiltrarUsuario(USUARIO item)
        {
            try
            {
                // Executa a operação
                List<USUARIO> listaObj = new List<USUARIO>();
                Int32 volta = baseApp.ExecuteFilter(item.PERF_CD_ID, item.COLABORADOR.COLA_NM_NOME, item.COLABORADOR.COLA_NR_CPF, item.USUA_NM_EMAIL, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                    //listaMaster = new List<USUARIO>();
                    //return RedirectToAction("MontarTelaUsuario");
                }

                // Sucesso
                listaMaster = listaObj;
                SessionMocks.listaUsuario = listaObj;
                return RedirectToAction("MontarTelaUsuario");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaUsuario");
            }
        }

        [HttpGet]
        public ActionResult VerUsuario(Int32 id)
        {
            // Prepara view
            USUARIO item = baseApp.GetItemById(id);
            objetoAntes = item;
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        public ActionResult VoltarBase()
        {
            listaMaster = new List<USUARIO>();
            SessionMocks.listaUsuario = null;
            return RedirectToAction("MontarTelaUsuario");
        }
        
        [HttpGet]
        public ActionResult BloquearUsuario(Int32 id)
        {
            // Prepara view
            USUARIO item = baseApp.GetById(id);
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult BloquearUsuario(UsuarioViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                USUARIO item = Mapper.Map<UsuarioViewModel, USUARIO>(vm);
                Int32 volta = 0;
                if (item.USUA_IN_BLOQUEADO == 1)
                {
                    volta = baseApp.ValidateDesbloqueio(item, usuarioLogado);
                }
                else
                {
                    volta = baseApp.ValidateBloqueio(item, usuarioLogado);
                }

                // Sucesso
                listaMaster = new List<USUARIO>();
                SessionMocks.listaUsuario = null;
                return RedirectToAction("MontarTelaUsuario");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult DesativarUsuario(Int32 id)
        {
            // Prepara view
            USUARIO item = baseApp.GetById(id);
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult DesativarUsuario(UsuarioViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                USUARIO item = Mapper.Map<UsuarioViewModel, USUARIO>(vm);
                Int32 volta = 0;
                if (item.USUA_IN_ATIVO == 1)
                {
                    volta = baseApp.ValidateDelete(item, usuarioLogado);
                }
                else
                {
                    volta = baseApp.ValidateReativar(item, usuarioLogado);
                }

                // Sucesso
                listaMaster = new List<USUARIO>();
                SessionMocks.listaUsuario = null;
                return RedirectToAction("MontarTelaUsuario");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult MontarTelaLog()
        {
            // Verifica se tem usuario logado
            USUARIO usuario = new USUARIO();
            if (SessionMocks.UserCredentials != null)
            {
                usuario = SessionMocks.UserCredentials;

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA != "ADM")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Carrega listas
            ViewBag.Usuarios = new SelectList(baseApp.GetAllUsuarios(), "USUA_CD_ID", "USUA_NM_EMAIL");
            if (SessionMocks.listaLog == null)
            {
                listaMasterLog = logApp.GetAllItens();
                SessionMocks.listaLog = listaMasterLog;
            }
            ViewBag.Listas = SessionMocks.listaLog;
            ViewBag.Title = "Auditoria";

            // Abre view
            objLog = new LOG();
            if (SessionMocks.filtroLog == null)
            {
                objLog.LOG_DT_DATA = DateTime.Today;
            }
            else
            {
                objLog = SessionMocks.filtroLog;    
            }
            return View(objLog);
        }

        public ActionResult RetirarFiltroLog()
        {
            SessionMocks.listaLog = null;
            SessionMocks.filtroLog = null;
            return RedirectToAction("MontarTelaLog");
        }

        [HttpPost]
        public ActionResult FiltrarLog(LOG item)
        {
            try
            {
                // Executa a operação
                List<LOG> listaObj = new List<LOG>();
                SessionMocks.filtroLog = item;
                Int32 volta = logApp.ExecuteFilter(item.USUA_CD_ID, item.LOG_DT_DATA, item.LOG_NM_OPERACAO, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaMasterLog = listaObj;
                SessionMocks.listaLog = listaMasterLog;
                return RedirectToAction("MontarTelaLog");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaLog");
            }
        }

        [HttpGet]
        public ActionResult VerLog(Int32 id)
        {
            // Prepara view
            LOG item = logApp.GetById(id);
            objLogAntes = item;
            LogViewModel vm = Mapper.Map<LOG, LogViewModel>(item);
            return View(vm);
        }

        public ActionResult VoltarBaseLog()
        {
            //listaMasterLog = new List<LOG>();
            //SessionMocks.listaLog = null;
            return RedirectToAction("MontarTelaLog");
        }

        public ActionResult VoltarLog()
        {
            listaMasterLog = new List<LOG>();
            SessionMocks.listaLog = null;
            SessionMocks.filtroLog = null;
            return RedirectToAction("CarregarAdmin", "BaseAdmin");
        }

        [HttpGet]
        public ActionResult MontarTelaLogGerencia()
        {
            // Verifica se tem usuario logado
            USUARIO usuario = new USUARIO();
            if (SessionMocks.UserCredentials != null)
            {
                usuario = SessionMocks.UserCredentials;

                // Verfifica permissão
                if (usuario.PERFIL.PERF_SG_SIGLA != "ADM")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Carrega listas
            //SessionMocks.IdAssinante = 2;
            ViewBag.Usuarios = new SelectList(baseApp.GetAllUsuarios(), "USUA_CD_ID", "USUA_NM_EMAIL");
            if (SessionMocks.listaLog == null)
            {
                listaMasterLog = logApp.GetAllItens();
                SessionMocks.listaLog = listaMasterLog;
            }
            ViewBag.Listas = SessionMocks.listaLog;
            ViewBag.Title = "Auditoria";

            // Abre view
            objLog = new LOG();
            objLog.LOG_DT_DATA = DateTime.Today;
            return View();
        }
    }
}