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
        private String msg;
        private Exception exception;
        USUARIO objeto = new USUARIO();
        USUARIO objetoAntes = new USUARIO();
        List<USUARIO> listaMaster = new List<USUARIO>();

        public AdministracaoController(IUsuarioAppService baseApps)
        {
            baseApp = baseApps;
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
            //SessionMocks.IdAssinante = 2;
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
            USUARIO usuario = SessionMocks.UserCredentials;
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
                    listaMaster = new List<USUARIO>();
                    return RedirectToAction("MontarTelaUsuario");
                }

                // Sucesso
                listaMaster = listaObj;
                SessionMocks.listaUsuario = listaObj;
                return RedirectToAction("MontarTelausuario");
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
    }
}