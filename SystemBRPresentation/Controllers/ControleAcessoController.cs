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
    public class ControleAcessoController : Controller
    {
        private readonly IUsuarioAppService baseApp;
        private String msg;
        private Exception exception;
        USUARIO objeto = new USUARIO();
        USUARIO objetoAntes = new USUARIO();
        List<USUARIO> listaMaster = new List<USUARIO>();

        public ControleAcessoController(IUsuarioAppService baseApps)
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
        public ActionResult Login()
        {
            USUARIO item = new USUARIO();
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UsuarioViewModel vm)
        {
            try
            {
                // Checa credenciais e atualiza acessos
                USUARIO usuario;
                SessionMocks.UserCredentials = null;
                ViewBag.Usuario = null;
                USUARIO login = Mapper.Map<UsuarioViewModel, USUARIO>(vm);
                Int32 volta = baseApp.ValidateLogin(login.USUA_NM_EMAIL, login.USUA_NM_SENHA, out usuario);
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0001", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 2)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0002", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 3)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0003", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 5)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0005", CultureInfo.CurrentCulture);
                    return RedirectToAction("GerarSenha", "GerarSenha");
                }
                if (volta == 4)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0004", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 6)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0006", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 7)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0007", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Armazena credenciais para autorização
                SessionMocks.UserCredentials = usuario;
                SessionMocks.Assinante = usuario.ASSINANTE;
                SessionMocks.IdAssinante = usuario.ASSINANTE.ASSI_CD_ID;
                SessionMocks.Colaborador = usuario.COLABORADOR;

                // Atualiza view
                String frase = String.Empty;
                String nome = usuario.COLABORADOR.COLA_NM_NOME.Substring(1, usuario.COLABORADOR.COLA_NM_NOME.IndexOf(" ") - 1);
                if (DateTime.Now.Hour <= 12)
                {
                    frase = "Bom dia, " + nome;
                }
                else if (DateTime.Now.Hour > 12 & DateTime.Now.Hour <= 18)
                {
                    frase = "Boa tarde, " + nome;
                }
                else
                {
                    frase = "Boa noite, " + nome;
                }
                ViewBag.Greeting = frase;
                ViewBag.Nome = usuario.COLABORADOR.COLA_NM_NOME;
                ViewBag.Cargo = usuario.COLABORADOR.CARGO.CARG_NM_NOME;
                ViewBag.Foto = usuario.USUA_AQ_FOTO;

                // Route
                if (SessionMocks.voltaLogin != null)
                {
                    if (SessionMocks.UserCredentials.PERFIL.PERF_SG_SIGLA == "ADM")
                    {
                        return RedirectToAction("CarregarAdmin", "BaseAdmin");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        public ActionResult Logout()
        {
            SessionMocks.UserCredentials = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Cancelar()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult TrocarSenha()
        {
            // Verifica se tem usuario logado
            USUARIO usuario = new USUARIO();
            if (SessionMocks.UserCredentials != null)
            {
                usuario = SessionMocks.UserCredentials;
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }

            // Reseta senhas
            usuario.USUA_NM_NOVA_SENHA = null;
            usuario.USUA_NM_SENHA_CONFIRMA = null;
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(usuario);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocarSenha(UsuarioViewModel vm)
        {
            try
            {
                // Checa credenciais e atualiza acessos
                USUARIO usuario = SessionMocks.UserCredentials;
                USUARIO item = Mapper.Map<UsuarioViewModel, USUARIO>(vm);
                Int32 volta = baseApp.ValidateChangePassword(item);
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0008", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 2)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0009", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                SessionMocks.UserCredentials = null;
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult GerarSenha()
        {
            USUARIO item = new USUARIO();
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GerarSenha(UsuarioViewModel vm)
        {
            try
            {
                // Processa
                SessionMocks.UserCredentials = null;
                USUARIO item = Mapper.Map<UsuarioViewModel, USUARIO>(vm);
                Int32 volta = baseApp.GenerateNewPassword(item.USUA_NM_EMAIL);
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0001", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 2)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0002", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 3)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0003", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 4)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0004", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }
    }
}