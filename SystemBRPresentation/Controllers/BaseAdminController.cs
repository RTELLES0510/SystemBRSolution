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
    public class BaseAdminController : Controller
    {
        private readonly IUsuarioAppService baseApp;
        private String msg;
        private Exception exception;
        USUARIO objeto = new USUARIO();
        USUARIO objetoAntes = new USUARIO();
        List<USUARIO> listaMaster = new List<USUARIO>();

        public BaseAdminController(IUsuarioAppService baseApps)
        {
            baseApp = baseApps;
        }

        public ActionResult CarregarAdmin()
        {
            USUARIO usu = SessionMocks.UserCredentials;
            UsuarioViewModel vm = Mapper.Map<USUARIO, UsuarioViewModel>(usu);
            ViewBag.NovasNotificacoes = baseApp.GetNotificacaoNovas(usu.USUA_CD_ID).Count;
            ViewBag.Noticias = baseApp.GetAllNoticias();
            ViewBag.NoticiasNumero = baseApp.GetAllNoticias().Count;
            return View(vm);
        }
    }
}