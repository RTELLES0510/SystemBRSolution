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
    public class BancoController : Controller
    {
        private readonly IBancoAppService baseApp;
        private readonly ILogAppService logApp;
        private readonly IContaBancariaAppService contaApp;
        private readonly IMatrizAppService matrizApp;
        private readonly IFilialAppService filialApp;
        private String msg;
        private Exception exception;
        BANCO objeto = new BANCO();
        BANCO objetoAntes = new BANCO();
        List<BANCO> listaMaster = new List<BANCO>();
        LOG objLog = new LOG();
        LOG objLogAntes = new LOG();
        List<LOG> listaMasterLog = new List<LOG>();
        CONTA_BANCARIA objConta = new CONTA_BANCARIA();
        CONTA_BANCARIA objContaAntes = new CONTA_BANCARIA();
        List<CONTA_BANCARIA> listaMasterConta = new List<CONTA_BANCARIA>();
        MATRIZ objMatriz = new MATRIZ();
        MATRIZ objMatrizAntes = new MATRIZ();
        FILIAL objFilial = new FILIAL();
        FILIAL objFilialAntes = new FILIAL();
        List<FILIAL> listaMasterFilial = new List<FILIAL>();

        public BancoController(IBancoAppService baseApps, ILogAppService logApps, IContaBancariaAppService contaApps, IMatrizAppService matrizApps, IFilialAppService filialApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            contaApp = contaApps;
            matrizApp = matrizApps;
            filialApp = filialApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            BANCO item = new BANCO();
            BancoViewModel vm = Mapper.Map<BANCO, BancoViewModel>(item);
            return View(vm);
        }

        [HttpGet]
        public ActionResult MontarTelaBanco()
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
            if (SessionMocks.listaBanco == null)
            {
                listaMaster = baseApp.GetAllItens();
                SessionMocks.listaBanco = listaMaster;
            }
            ViewBag.Listas = SessionMocks.listaBanco;
            ViewBag.Title = "Bancos";

            // Indicadores
            ViewBag.Bancos = listaMaster.Count;
            ViewBag.Contas = contaApp.GetAllItens().Count;

            // Abre view
            objeto = new BANCO();
            return View(objeto);
        }

        public ActionResult Voltar()
        {
            return RedirectToAction("CarregarCadastroBasico", "BaseCadastroBasico");
        }

        public ActionResult RetirarFiltro()
        {
            SessionMocks.listaBanco = null;
            return RedirectToAction("MontarTelaBanco");
        }

        public ActionResult MostrarTudo()
        {
            listaMaster = baseApp.GetAllItensAdm();
            SessionMocks.listaBanco = listaMaster;
            return RedirectToAction("MontarTelaBanco");
        }

        [HttpPost]
        public ActionResult FiltrarBanco(BANCO item)
        {
            try
            {
                // Executa a operação
                List<BANCO> listaObj = new List<BANCO>();
                Int32 volta = baseApp.ExecuteFilter(item.BANC_SG_CODIGO, item.BANC_NM_NOME, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                    listaMaster = new List<BANCO>();
                    return RedirectToAction("MontarTelaBanco");
                }

                // Sucesso
                listaMaster = listaObj;
                SessionMocks.listaBanco = listaObj;
                return RedirectToAction("MontarTelaBanco");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaBanco");
            }
        }

        public ActionResult VoltarBase()
        {
            listaMaster = new List<BANCO>();
            SessionMocks.listaBanco = null;
            return RedirectToAction("MontarTelaBanco");
        }

        [HttpGet]
        public ActionResult Incluir()
        {
            // Prepara listas

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            BANCO item = new BANCO();
            BancoViewModel vm = Mapper.Map<BANCO, BancoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir(BancoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    BANCO item = Mapper.Map<BancoViewModel, BANCO>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = baseApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0011", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Sucesso
                    listaMaster = new List<BANCO>();
                    SessionMocks.listaBanco = null;
                    return RedirectToAction("MontarTelaBanco");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult Editar(Int32 id)
        {
            // Prepara view
            BANCO item = baseApp.GetItemById(id);
            objetoAntes = item;
            SessionMocks.banco = item;
            BancoViewModel vm = Mapper.Map<BANCO, BancoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(BancoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    BANCO item = Mapper.Map<BancoViewModel, BANCO>(vm);
                    Int32 volta = baseApp.ValidateEdit(item, objetoAntes, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0011", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Sucesso
                    listaMaster = new List<BANCO>();
                    SessionMocks.listaBanco = null;
                    return RedirectToAction("MontarTelaBanco");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult Excluir(Int32 id)
        {
            // Prepara view
            BANCO item = baseApp.GetItemById(id);
            BancoViewModel vm = Mapper.Map<BANCO, BancoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Excluir(BancoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                BANCO item = Mapper.Map<BancoViewModel, BANCO>(vm);
                Int32 volta = baseApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0012", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMaster = new List<BANCO>();
                SessionMocks.listaBanco = null;
                return RedirectToAction("MontarTelaBanco");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult Reativar(Int32 id)
        {
            // Prepara view
            BANCO item = baseApp.GetItemById(id);
            BancoViewModel vm = Mapper.Map<BANCO, BancoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Reativar(BancoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                BANCO item = Mapper.Map<BancoViewModel, BANCO>(vm);
                Int32 volta = baseApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMaster = new List<BANCO>();
                SessionMocks.listaBanco = null;
                return RedirectToAction("MontarTelaBanco");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult IncluirConta()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(contaApp.GetAllTipos(), "TICO_CD_ID", "TICO_NM_NOME");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            CONTA_BANCARIA item = new CONTA_BANCARIA();
            ContaBancariaViewModel vm = Mapper.Map<CONTA_BANCARIA, ContaBancariaViewModel>(item);
            vm.ASSI_CD_ID = usuario.ASSI_CD_ID;
            //vm.ASSI_CD_ID = 2;
            vm.BANC_CD_ID = SessionMocks.banco.BANC_CD_ID;
            vm.COBA_DT_ABERTURA = DateTime.Today;
            vm.COBA_VL_SALDO_INICIAL = 0;
            vm.COBA_IN_ATIVO = 1;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirConta(ContaBancariaViewModel vm)
        {
            ViewBag.Tipos = new SelectList(contaApp.GetAllTipos(), "TICO_CD_ID", "TICO_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    CONTA_BANCARIA item = Mapper.Map<ContaBancariaViewModel, CONTA_BANCARIA>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = contaApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0013", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Sucesso
                    listaMasterConta = new List<CONTA_BANCARIA>();
                    SessionMocks.listaContaBancaria = null;
                    return RedirectToAction("Editar", new { id = SessionMocks.banco.BANC_CD_ID });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarConta(Int32 id)
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(contaApp.GetAllTipos(), "TICO_CD_ID", "TICO_NM_NOME");
            
            // Prepara view
            CONTA_BANCARIA item = contaApp.GetItemById(id);
            objContaAntes = item;
            ContaBancariaViewModel vm = Mapper.Map<CONTA_BANCARIA, ContaBancariaViewModel>(item);
            return View(vm); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarConta(ContaBancariaViewModel vm)
        {
            ViewBag.Tipos = new SelectList(contaApp.GetAllTipos(), "TICO_CD_ID", "TICO_NM_NOME");
            //if (ModelState.IsValid)
            //{
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CONTA_BANCARIA item = Mapper.Map<ContaBancariaViewModel, CONTA_BANCARIA>(vm);
                    Int32 volta = contaApp.ValidateEdit(item, objContaAntes, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0013", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Sucesso
                    listaMasterConta = new List<CONTA_BANCARIA>();
                    SessionMocks.listaContaBancaria = null;
                    return RedirectToAction("Editar", new { id = SessionMocks.banco.BANC_CD_ID });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            //}
            //else
            //{
            //    return View(vm);
            //}
        }

        [HttpGet]
        public ActionResult ExcluirConta(Int32 id)
        {
            // Prepara view
            CONTA_BANCARIA item = contaApp.GetItemById(id);
            ContaBancariaViewModel vm = Mapper.Map<CONTA_BANCARIA, ContaBancariaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirConta(ContaBancariaViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CONTA_BANCARIA item = Mapper.Map<ContaBancariaViewModel, CONTA_BANCARIA>(vm);
                Int32 volta = contaApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0014", CultureInfo.CurrentCulture);
                    return View(vm);
                }
                if (volta == 2)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0015", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMasterConta = new List<CONTA_BANCARIA>();
                SessionMocks.listaContaBancaria = null;
                return RedirectToAction("Editar", new { id = SessionMocks.banco.BANC_CD_ID });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarConta(Int32 id)
        {
            // Prepara view
            CONTA_BANCARIA item = contaApp.GetItemById(id);
            ContaBancariaViewModel vm = Mapper.Map<CONTA_BANCARIA, ContaBancariaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarConta(ContaBancariaViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CONTA_BANCARIA item = Mapper.Map<ContaBancariaViewModel, CONTA_BANCARIA>(vm);
                Int32 volta = contaApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMasterConta = new List<CONTA_BANCARIA>();
                SessionMocks.listaContaBancaria = null;
                return RedirectToAction("Editar", new { id = SessionMocks.banco.BANC_CD_ID });
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VoltarBaseConta()
        {
            listaMasterConta = new List<CONTA_BANCARIA>();
            SessionMocks.listaContaBancaria = null;
            return RedirectToAction("Editar", new { id = SessionMocks.banco.BANC_CD_ID });
        }

        [HttpGet]
        public ActionResult MontarTelaMatriz()
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
            if (SessionMocks.Matriz == null)
            {
                objMatriz = matrizApp.GetAllItens().FirstOrDefault();
                SessionMocks.Matriz = objMatriz;
            }
            ViewBag.Title = "Matriz";

            // Abre view
            MatrizViewModel vm = Mapper.Map<MATRIZ, MatrizViewModel>(objMatriz);
            objMatrizAntes = objMatriz;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MontarTelaMatriz(MatrizViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    MATRIZ item = Mapper.Map<MatrizViewModel, MATRIZ>(vm);
                    Int32 volta = matrizApp.ValidateEdit(item, objMatrizAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    objMatriz = new MATRIZ();
                    SessionMocks.Matriz = null;
                    return RedirectToAction("MontarTelaMatriz");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult IncluirFilial()
        {
            // Prepara listas

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            FILIAL item = new FILIAL();
            FilialViewModel vm = Mapper.Map<FILIAL, FilialViewModel>(item);
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            //vm.ASSI_CD_ID = 2;
            vm.FILI_IN_ATIVO = 1;
            vm.FILI_DT_CADASTRO = DateTime.Today;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirFilial(FilialViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    FILIAL item = Mapper.Map<FilialViewModel, FILIAL>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = filialApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0016", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Sucesso
                    objMatriz = new MATRIZ();
                    SessionMocks.Matriz = null;
                    return RedirectToAction("MontarTelaMatriz");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditarFilial(Int32 id)
        {
            // Prepara listas
            
            // Prepara view
            FILIAL item = filialApp.GetItemById(id);
            objFilialAntes = item;
            FilialViewModel vm = Mapper.Map<FILIAL, FilialViewModel>(item);
            return View(vm); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFilial(FilialViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    FILIAL item = Mapper.Map<FilialViewModel, FILIAL>(vm);
                    Int32 volta = filialApp.ValidateEdit(item, objFilialAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    objMatriz = new MATRIZ();
                    SessionMocks.Matriz = null;
                    return RedirectToAction("MontarTelaMatriz");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ExcluirFilial(Int32 id)
        {
            // Prepara view
            FILIAL item = filialApp.GetItemById(id);
            FilialViewModel vm = Mapper.Map<FILIAL, FilialViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirFilial(FilialViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                FILIAL item = Mapper.Map<FilialViewModel, FILIAL>(vm);
                Int32 volta = filialApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0017", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                objMatriz = new MATRIZ();
                SessionMocks.Matriz = null;
                return RedirectToAction("MontarTelaMatriz");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarFilial(Int32 id)
        {
            // Prepara view
            FILIAL item = filialApp.GetItemById(id);
            FilialViewModel vm = Mapper.Map<FILIAL, FilialViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarFilial(FilialViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                FILIAL item = Mapper.Map<FilialViewModel, FILIAL>(vm);
                Int32 volta = filialApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                objMatriz = new MATRIZ();
                SessionMocks.Matriz = null;
                return RedirectToAction("MontarTelaMatriz");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VoltarBaseFilial()
        {
            objMatriz = new MATRIZ();
            SessionMocks.Matriz = null;
            return RedirectToAction("MontarTelaMatriz");
        }
    }
}