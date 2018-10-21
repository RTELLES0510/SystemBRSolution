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
    public class GestaoComercialController : Controller
    {
        private readonly IContratoAppService baseApp;
        private readonly ILogAppService logApp;
        private readonly IMatrizAppService matrizApp;
        private readonly IContratoSolicitacaoAprovacaoAppService solApp;
        private String msg;
        private Exception exception;
        CONTRATO objetoContrato = new CONTRATO();
        CONTRATO objetoContratoAntes = new CONTRATO();
        List<CONTRATO> listaContratoMaster = new List<CONTRATO>();
        LOG objLog = new LOG();
        LOG objLogAntes = new LOG();
        List<LOG> listaMasterLog = new List<LOG>();
        String extensao = String.Empty;

        public GestaoComercialController(IContratoAppService baseApps, ILogAppService logApps, IMatrizAppService matrizApps, IContratoSolicitacaoAprovacaoAppService solApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            matrizApp = matrizApps;
            solApp = solApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            CONTRATO item = new CONTRATO();
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            return View(vm);
        }

        public ActionResult Voltar()
        {
            listaContratoMaster = new List<CONTRATO>();
            SessionMocks.listaContrato = null;
            return RedirectToAction("CarregarComercial", "BaseComercial");
        }

        [HttpGet]
        public ActionResult MontarTelaContrato()
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
            if (SessionMocks.listaContrato == null)
            {
                listaContratoMaster = baseApp.GetAllItens();
                SessionMocks.listaContrato = listaContratoMaster;
            }
            ViewBag.Listas = SessionMocks.listaContrato;
            ViewBag.Title = "Contratos";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "TICT_CD_ID", "TICT_NM_NOME");
            ViewBag.Categorias = new SelectList(baseApp.GetAllCategorias(), "CACT_CD_ID", "CACT_NM_NOME");
            ViewBag.Status = new SelectList(baseApp.GetAllStatus(), "STCT_CD_ID", "STCT_NM_NOME");

            // Indicadores
            ViewBag.Contratos = baseApp.GetAllItens().Count;
            ViewBag.ContratosOperacao = baseApp.GetAllItensOperacao().Count;

            // Abre view
            objetoContrato = new CONTRATO();
            if (SessionMocks.filtroContrato != null)
            {
                objetoContrato = SessionMocks.filtroContrato;
            }
            SessionMocks.voltaContrato = 1;
            return View(objetoContrato);
        }

        public ActionResult RetirarFiltroContrato()
        {
            SessionMocks.listaContrato = null;
            SessionMocks.filtroContrato = null;
            return RedirectToAction("MontarTelaContrato");
        }

        public ActionResult MostrarTudoContrato()
        {
            listaContratoMaster = baseApp.GetAllItensAdm();
            SessionMocks.filtroContrato = null;
            SessionMocks.listaContrato = listaContratoMaster;
            return RedirectToAction("MontarTelaContrato");
        }

        [HttpPost]
        public ActionResult FiltrarContrato(CONTRATO item)
        {
            try
            {
                // Executa a operação
                List<CONTRATO> listaObj = new List<CONTRATO>();
                SessionMocks.filtroContrato = item;
                Int32 volta = baseApp.ExecuteFilter(item.CACT_CD_ID, item.TICT_CD_ID, item.STCT_CD_ID, item.CONT_NM_NOME, item.CONT_DS_DESCRICAO, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaContratoMaster = listaObj;
                SessionMocks.listaContrato = listaObj;
                return RedirectToAction("MontarTelaContrato");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaContrato");
            }
        }

        public ActionResult VoltarBaseContrato()
        {
            if (SessionMocks.voltaContrato == 1)
            {
                return RedirectToAction("MontarTelaContrato");
            }
            return RedirectToAction("WorkflowContrato", new { id = SessionMocks.idVolta });
        }

        public ActionResult VoltarContrato()
        {
            return RedirectToAction("MontarTelaContrato");
        }

        [HttpGet]
        public ActionResult IncluirContrato()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "TICT_CD_ID", "TICT_NM_NOME");
            ViewBag.Categorias = new SelectList(baseApp.GetAllCategorias(), "CACT_CD_ID", "CACT_NM_NOME");
            ViewBag.Centros = new SelectList(baseApp.GetAllCentros(), "CECU_CD_ID", "CECU_NM_NOME");
            ViewBag.Clientes = new SelectList(baseApp.GetAllClientes(), "CLIE_CD_ID", "CLIE_NM_NOME");
            ViewBag.Formas = new SelectList(baseApp.GetAllForma(), "FOPA_CD_ID", "FOPA_NM_NOME");
            ViewBag.Nomenclaturas = new SelectList(baseApp.GetAllNomenclatura(), "NBSE_CD_ID", "NBSE_NM_NOME");
            ViewBag.Periodicidades = new SelectList(baseApp.GetAllPeriodicidades(), "PERI_CD_ID", "PERI_NM_NOME");
            ViewBag.Planos = new SelectList(baseApp.GetAllPlanoConta(), "PLCO_CD_ID", "PLCO_NM_CONTA");
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Vendedores = new SelectList(baseApp.GetAllVendedores(), "COLA_CD_ID", "COLA_NM_NOME");
            ViewBag.Status = new SelectList(baseApp.GetAllStatus(), "STCT_CD_ID", "STCT_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");

            List<SelectListItem> periodoCobranca = new List<SelectListItem>();
            periodoCobranca.Add(new SelectListItem() { Text = "Nenhum", Value = "1" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Atual", Value = "2" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Anterior", Value = "2" });
            ViewBag.PeriodoCobranca = new SelectList(periodoCobranca, "Value", "Text");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            CONTRATO item = new CONTRATO();
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            vm.ASSI_CD_ID = SessionMocks.IdAssinante.Value;
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            vm.CONT_IN_ATIVO = 1;
            vm.CONT_DT_INICIO = DateTime.Today;
            vm.CONT_DT_FINAL = DateTime.Today.AddDays(365);
            vm.CONT_IN_STATUS = 1;
            return View(vm);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult IncluirContrato(ContratoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "TICT_CD_ID", "TICT_NM_NOME");
            ViewBag.Categorias = new SelectList(baseApp.GetAllCategorias(), "CACT_CD_ID", "CACT_NM_NOME");
            ViewBag.Centros = new SelectList(baseApp.GetAllCentros(), "CECU_CD_ID", "CECU_NM_NOME");
            ViewBag.Clientes = new SelectList(baseApp.GetAllClientes(), "CLIE_CD_ID", "CLIE_NM_NOME");
            ViewBag.Formas = new SelectList(baseApp.GetAllForma(), "FOPA_CD_ID", "FOPA_NM_NOME");
            ViewBag.Nomenclaturas = new SelectList(baseApp.GetAllNomenclatura(), "NBSE_CD_ID", "NBSE_NM_NOME");
            ViewBag.Periodicidades = new SelectList(baseApp.GetAllPeriodicidades(), "PERI_CD_ID", "PERI_NM_NOME");
            ViewBag.Planos = new SelectList(baseApp.GetAllPlanoConta(), "PLCO_CD_ID", "PLCO_NM_CONTA");
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Vendedores = new SelectList(baseApp.GetAllVendedores(), "COLA_CD_ID", "COLA_NM_NOME");
            ViewBag.Status = new SelectList(baseApp.GetAllStatus(), "STCT_CD_ID", "STCT_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");
            List<SelectListItem> periodoCobranca = new List<SelectListItem>();
            periodoCobranca.Add(new SelectListItem() { Text = "Nenhum", Value = "1" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Atual", Value = "2" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Anterior", Value = "2" });
            ViewBag.PeriodoCobranca = new SelectList(periodoCobranca, "Value", "Text");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    CONTRATO item = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = baseApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0037", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Cria pastas
                    String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Contratos/" + item.CONT_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));

                    // Sucesso
                    listaContratoMaster = new List<CONTRATO>();
                    SessionMocks.listaContrato = null;
                    return RedirectToAction("MontarTelaContrato");
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
        public ActionResult EditarContrato(Int32 id)
        {
            // Prepara view
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "TICT_CD_ID", "TICT_NM_NOME");
            ViewBag.Categorias = new SelectList(baseApp.GetAllCategorias(), "CACT_CD_ID", "CACT_NM_NOME");
            ViewBag.Centros = new SelectList(baseApp.GetAllCentros(), "CECU_CD_ID", "CECU_NM_NOME");
            ViewBag.Clientes = new SelectList(baseApp.GetAllClientes(), "CLIE_CD_ID", "CLIE_NM_NOME");
            ViewBag.Formas = new SelectList(baseApp.GetAllForma(), "FOPA_CD_ID", "FOPA_NM_NOME");
            ViewBag.Nomenclaturas = new SelectList(baseApp.GetAllNomenclatura(), "NBSE_CD_ID", "NBSE_NM_NOME");
            ViewBag.Periodicidades = new SelectList(baseApp.GetAllPeriodicidades(), "PERI_CD_ID", "PERI_NM_NOME");
            ViewBag.Planos = new SelectList(baseApp.GetAllPlanoConta(), "PLCO_CD_ID", "PLCO_NM_CONTA");
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Vendedores = new SelectList(baseApp.GetAllVendedores(), "COLA_CD_ID", "COLA_NM_NOME");
            ViewBag.Status = new SelectList(baseApp.GetAllStatus(), "STCT_CD_ID", "STCT_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");

            List<SelectListItem> periodoCobranca = new List<SelectListItem>();
            periodoCobranca.Add(new SelectListItem() { Text = "Nenhum", Value = "1" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Atual", Value = "2" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Anterior", Value = "2" });
            ViewBag.PeriodoCobranca = new SelectList(periodoCobranca, "Value", "Text");

            CONTRATO item = baseApp.GetItemById(id);
            objetoContratoAntes = item;
            SessionMocks.contrato = item;
            SessionMocks.idVolta = id;
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarContrato(ContratoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "TICT_CD_ID", "TICT_NM_NOME");
            ViewBag.Categorias = new SelectList(baseApp.GetAllCategorias(), "CACT_CD_ID", "CACT_NM_NOME");
            ViewBag.Centros = new SelectList(baseApp.GetAllCentros(), "CECU_CD_ID", "CECU_NM_NOME");
            ViewBag.Clientes = new SelectList(baseApp.GetAllClientes(), "CLIE_CD_ID", "CLIE_NM_NOME");
            ViewBag.Formas = new SelectList(baseApp.GetAllForma(), "FOPA_CD_ID", "FOPA_NM_NOME");
            ViewBag.Nomenclaturas = new SelectList(baseApp.GetAllNomenclatura(), "NBSE_CD_ID", "NBSE_NM_NOME");
            ViewBag.Periodicidades = new SelectList(baseApp.GetAllPeriodicidades(), "PERI_CD_ID", "PERI_NM_NOME");
            ViewBag.Planos = new SelectList(baseApp.GetAllPlanoConta(), "PLCO_CD_ID", "PLCO_NM_CONTA");
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Vendedores = new SelectList(baseApp.GetAllVendedores(), "COLA_CD_ID", "COLA_NM_NOME");
            ViewBag.Status = new SelectList(baseApp.GetAllStatus(), "STCT_CD_ID", "STCT_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");
            List<SelectListItem> periodoCobranca = new List<SelectListItem>();
            periodoCobranca.Add(new SelectListItem() { Text = "Nenhum", Value = "1" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Atual", Value = "2" });
            periodoCobranca.Add(new SelectListItem() { Text = "Mês Anterior", Value = "2" });
            ViewBag.PeriodoCobranca = new SelectList(periodoCobranca, "Value", "Text");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CONTRATO item = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                    Int32 volta = baseApp.ValidateEdit(item, objetoContratoAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaContratoMaster = new List<CONTRATO>();
                    SessionMocks.listaContrato = null;
                    return RedirectToAction("MontarTelaContrato");
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
        public ActionResult ExcluirContrato(Int32 id)
        {
            // Prepara view
            CONTRATO item = baseApp.GetItemById(id);
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirContrato(ContratoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CONTRATO item = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                Int32 volta = baseApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0038", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaContratoMaster = new List<CONTRATO>();
                SessionMocks.listaContrato = null;
                return RedirectToAction("MontarTelaContrato");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult ReativarContrato(Int32 id)
        {
            // Prepara view
            CONTRATO item = baseApp.GetItemById(id);
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarContrato(ContratoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CONTRATO item = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                Int32 volta = baseApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaContratoMaster = new List<CONTRATO>();
                SessionMocks.listaContrato = null;
                return RedirectToAction("MontarTelaContrato");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult VerAnexoContrato(Int32 id)
        {
            // Prepara view
            CONTRATO_ANEXO item = baseApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult VoltarAnexoContrato()
        {
            return RedirectToAction("EditarContrato", new { id = SessionMocks.idVolta });
        }

        public ActionResult VoltarWorkflowContrato()
        {
            return RedirectToAction("WorkflowContrato", new { id = SessionMocks.idVolta });
        }

        public FileResult DownloadContrato(Int32 id)
        {
            CONTRATO_ANEXO item = baseApp.GetAnexoById(id);
            String arquivo = item.COAN_AQ_ARQUIVO;
            Int32 pos = arquivo.LastIndexOf("/") + 1;
            String nomeDownload = arquivo.Substring(pos);
            String contentType = string.Empty;
            if (arquivo.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (arquivo.Contains(".jpg"))
            {
                contentType = "image/jpg";
            }
            else if (arquivo.Contains(".png"))
            {
                contentType = "image/png";
            }
            return File(arquivo, contentType, nomeDownload);
        }

        [HttpPost]
        public ActionResult UploadFileContrato(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            CONTRATO item = baseApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Contratos/" + item.CONT_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            CONTRATO_ANEXO foto = new CONTRATO_ANEXO();
            foto.COAN_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.COAN_DT_ANEXO = DateTime.Today;
            foto.COAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            foto.COAN_IN_TIPO = tipo;
            foto.COAN_NM_TITULO = fileName;
            foto.CONT_CD_ID = item.CONT_CD_ID;

            item.CONTRATO_ANEXO.Add(foto);
            objetoContratoAntes = item;
            Int32 volta = baseApp.ValidateEdit(item, objetoContratoAntes);
            return RedirectToAction("VoltarAnexoContrato");
        }

        [HttpGet]
        public ActionResult VisualizarContrato(Int32 id)
        {
            // Prepara view
            CONTRATO item = baseApp.GetItemById(id);
            objetoContratoAntes = item;
            SessionMocks.contrato = item;
            SessionMocks.idVolta = id;
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            return View(vm);
        }

        [HttpGet]
        public ActionResult WorkflowContrato(Int32 id)
        {
            // Prepara view
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");
            SessionMocks.voltaContrato = 2;
            ViewBag.Usuario = SessionMocks.UserCredentials;

            CONTRATO item = baseApp.GetItemById(id);
            objetoContratoAntes = item;
            SessionMocks.contrato = item;
            SessionMocks.idVolta = id;
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);

            // Ajustes
            if (vm.CONT_IN_WORKFLOW == 1)
            {
                if (vm.CONT_IN_ENVIO_APROVACAO == 0)
                {
                    vm.CONT_DT_SOLICITACAO_APROVACAO = DateTime.Today;
                    vm.CONT_DS_APROVACAO = baseApp.GetTextoAprovacao();
                }
                else
                {
                    vm.CONT_NM_RESPONSAVEL = baseApp.GetResponsavelById(vm.CONT_CD_RESPONSAVEL.Value).COLA_NM_NOME;
                }
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult SolicitarAprovacaoContrato()
        {
            // Prepara view
            ViewBag.Templates = new SelectList(baseApp.GetAllTemplates(), "TEMP_CD_ID", "TEMP_NM_NOME");
            ViewBag.Responsaveis = new SelectList(baseApp.GetAllResponsaveis(), "COLA_CD_ID", "COLA_NM_NOME");

            CONTRATO item = baseApp.GetItemById(SessionMocks.idVolta);
            objetoContratoAntes = item;
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            vm.CONT_DT_SOLICITACAO_APROVACAO = DateTime.Today;
            vm.CONT_DS_APROVACAO = baseApp.GetTextoAprovacao();
            return View(vm);
        }

        [HttpPost]
        public ActionResult EnviarAprovacaoContrato(ContratoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CONTRATO cont = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                    Int32 volta = baseApp.EmitirAprovacaoContrato(cont, usuarioLogado);

                    // Exibe mensagem
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0039", CultureInfo.CurrentCulture);

                    // Sucesso
                    return RedirectToAction("VoltarWorkflowContrato");
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
        public ActionResult CancelarContrato()
        {
            // Prepara view
            CONTRATO item = baseApp.GetItemById(SessionMocks.idVolta);
            objetoContratoAntes = item;
            ContratoViewModel vm = Mapper.Map<CONTRATO, ContratoViewModel>(item);
            vm.CONT_DT_CANCELAMENTO = DateTime.Today;
            return View(vm);
        }

        [HttpPost]
        public ActionResult CancelarContrato(ContratoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CONTRATO cont = Mapper.Map<ContratoViewModel, CONTRATO>(vm);
                    Int32 volta = baseApp.CancelarContrato(cont, usuarioLogado);

                    // Exibe mensagem
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0040", CultureInfo.CurrentCulture);

                    // Sucesso
                    return RedirectToAction("VoltarWorkflowContrato");
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
        public ActionResult GerarRetornoAprovacaoContrato()
        {
            // Prepara view
            CONTRATO item = baseApp.GetItemById(SessionMocks.idVolta);
            objetoContratoAntes = item;
            List<SelectListItem> statusResposta = new List<SelectListItem>();
            statusResposta.Add(new SelectListItem() { Text = "Aprovado", Value = "1" });
            statusResposta.Add(new SelectListItem() { Text = "Não Aprovado", Value = "0" });
            ViewBag.StatusResposta = new SelectList(statusResposta, "Value", "Text");

            ContratoSolicitacaoAprovacaoViewModel vm = new ContratoSolicitacaoAprovacaoViewModel();
            vm.CONT_CD_ID = item.CONT_CD_ID;
            vm.CONTRATO = item;
            vm.CTSA_DT_DATA = DateTime.Today;
            vm.CTSA_IN_ATIVO = 1;
            vm.CTSA_IN_STATUS = 0;
            return View(vm);
        }

        [HttpPost]
        public ActionResult GerarRetornoAprovacaoContrato(ContratoSolicitacaoAprovacaoViewModel vm)
        {
            List<SelectListItem> statusResposta = new List<SelectListItem>();
            statusResposta.Add(new SelectListItem() { Text = "Aprovado", Value = "1" });
            statusResposta.Add(new SelectListItem() { Text = "Não Aprovado", Value = "0" });
            ViewBag.StatusResposta = new SelectList(statusResposta, "Value", "Text");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CONTRATO_SOLICITACAO_APROVACAO cont = Mapper.Map<ContratoSolicitacaoAprovacaoViewModel, CONTRATO_SOLICITACAO_APROVACAO>(vm);

                    CONTRATO item = baseApp.GetItemById(SessionMocks.idVolta);
                    objetoContratoAntes = item;
                    Int32 volta = baseApp.ValidateRespostaAprovacao(item, objetoContratoAntes, cont, usuarioLogado);

                    // Exibe mensagem
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0041", CultureInfo.CurrentCulture);

                    // Sucesso
                    return RedirectToAction("VoltarWorkflowContrato");
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
        public ActionResult VerRetornoAprovacaoContrato(Int32 id)
        {
            // Prepara view
            CONTRATO_SOLICITACAO_APROVACAO item = solApp.GetItemById(id);
            ContratoSolicitacaoAprovacaoViewModel vm = Mapper.Map<CONTRATO_SOLICITACAO_APROVACAO, ContratoSolicitacaoAprovacaoViewModel>(item);
            vm.CTSA_NM_STATUS = vm.CTSA_IN_STATUS == 1 ? "Aprovado" : "Não Aprovado";
            return View(vm);
        }
    }
}