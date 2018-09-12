using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitiesServices.Work_Classes;

namespace SystemBRPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionMocks.IdAssinante = 2;
            if (SessionMocks.UserCredentials != null)
            {
                ViewBag.Usuario = "Olá, " + SessionMocks.NomeLogado;
            }
            else
            {
                return RedirectToAction("Login", "ControleAcesso");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}