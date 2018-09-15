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
    public class CadastrosController : Controller
    {
        private readonly IClienteAppService baseApp;
        private readonly IMatrizAppService matrizApp;
        private readonly ILogAppService logApp;
        private readonly IFornecedorAppService fornApp;
        private String msg;
        private Exception exception;
        CLIENTE objeto = new CLIENTE();
        CLIENTE objetoAntes = new CLIENTE();
        List<CLIENTE> listaMaster = new List<CLIENTE>();
        LOG objLog = new LOG();
        LOG objLogAntes = new LOG();
        List<LOG> listaMasterLog = new List<LOG>();
        String extensao = String.Empty;
        FORNECEDOR objetoForn = new FORNECEDOR();
        FORNECEDOR objetoFornAntes = new FORNECEDOR();
        List<FORNECEDOR> listaMasterForn = new List<FORNECEDOR>();

        public CadastrosController(IClienteAppService baseApps, ILogAppService logApps, IMatrizAppService matrizApps, IFornecedorAppService fornApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            matrizApp = matrizApps;
            fornApp = fornApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            CLIENTE item = new CLIENTE();
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            return View(vm);
        }
        
        [HttpGet]
        public ActionResult MontarTelaCliente()
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
            if (SessionMocks.listaCliente == null)
            {
                listaMaster = baseApp.GetAllItens();
                SessionMocks.listaCliente = listaMaster;
            }
            ViewBag.Listas = SessionMocks.listaCliente;
            ViewBag.Title = "Clientes";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Indicadores
            ViewBag.Clientes = baseApp.GetAllItens().Count;
            
            // Abre view
            objeto = new CLIENTE();
            SessionMocks.voltaCliente = 1;
            return View(objeto);
        }

        public ActionResult RetirarFiltroCliente()
        {
            SessionMocks.listaCliente = null;
            if (SessionMocks.voltaCliente == 2)
            {
                return RedirectToAction("VerCardsCliente");
            }
            return RedirectToAction("MontarTelaCliente");
        }

        public ActionResult MostrarTudoCliente()
        {
            listaMaster = baseApp.GetAllItensAdm();
            SessionMocks.listaCliente = listaMaster;
            if (SessionMocks.voltaCliente == 2)
            {
                return RedirectToAction("VerCardsCliente");
            }
            return RedirectToAction("MontarTelaCliente");
        }

        [HttpPost]
        public ActionResult FiltrarCliente(CLIENTE item)
        {
            try
            {
                // Executa a operação
                List<CLIENTE> listaObj = new List<CLIENTE>();
                Int32 volta = baseApp.ExecuteFilter(item.CACL_CD_ID, item.CLIE_NM_NOME, item.CLIE_NR_CPF, item.CLIE_NR_CNPJ, item.CLIE_NM_EMAIL, item.CLIE_NM_CIDADE, item.CLIE_SG_UF, item.CLIE_NM_REDES_SOCIAIS, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaMaster = listaObj;
                SessionMocks.listaCliente = listaObj;
                if (SessionMocks.voltaCliente == 2)
                {
                    return RedirectToAction("VerCardsCliente");
                }
                return RedirectToAction("MontarTelaCliente");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaCliente");
            }
        }

        public ActionResult VoltarBaseCliente()
        {
            listaMaster = new List<CLIENTE>();
            SessionMocks.listaCliente = null;
            return RedirectToAction("MontarTelaCliente");
        }

       [HttpGet]
        public ActionResult IncluirCliente()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            CLIENTE item = new CLIENTE();
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            vm.ASSI_CD_ID = SessionMocks.IdAssinante.Value;
            vm.CLIE_DT_CADASTRO = DateTime.Today;
            vm.CLIE_IN_ATIVO = 1;
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            vm.FILI_CD_ID = usuario.COLABORADOR.FILI_CD_ID;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirCliente(ClienteViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    CLIENTE item = Mapper.Map<ClienteViewModel, CLIENTE>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = baseApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0018", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Carrega foto e processa alteracao
                    item.CLIE_AQ_FOTO = "~/Imagens/Base/FotoBase.jpg";
                    volta = baseApp.ValidateEdit(item, item, usuarioLogado);

                    // Cria pastas
                    String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Clientes/" + item.CLIE_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Clientes/" + item.CLIE_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    
                    // Sucesso
                    listaMaster = new List<CLIENTE>();
                    SessionMocks.listaCliente = null;
                    if (SessionMocks.voltaCliente == 2)
                    {
                        return RedirectToAction("VerCardsCliente");
                    }
                    return RedirectToAction("MontarTelaCliente");
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
        public ActionResult EditarCliente(Int32 id)
        {
            // Prepara view
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            CLIENTE item = baseApp.GetItemById(id);
            objetoAntes = item;
            SessionMocks.cliente = item;
            SessionMocks.idVolta = id;
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCliente(ClienteViewModel vm)
        {
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    CLIENTE item = Mapper.Map<ClienteViewModel, CLIENTE>(vm);
                    Int32 volta = baseApp.ValidateEdit(item, objetoAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaMaster = new List<CLIENTE>();
                    SessionMocks.listaCliente = null;
                    if (SessionMocks.voltaCliente == 2)
                    {
                        return RedirectToAction("VerCardsCliente");
                    }
                    return RedirectToAction("MontarTelaCliente");
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
        public ActionResult ExcluirCliente(Int32 id)
        {
            // Prepara view
            CLIENTE item = baseApp.GetItemById(id);
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirCliente(ClienteViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CLIENTE item = Mapper.Map<ClienteViewModel, CLIENTE>(vm);
                Int32 volta = baseApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0019", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMaster = new List<CLIENTE>();
                SessionMocks.listaCliente = null;
                return RedirectToAction("MontarTelaCliente");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarCliente(Int32 id)
        {
            // Prepara view
            CLIENTE item = baseApp.GetItemById(id);
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarCliente(ClienteViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                CLIENTE item = Mapper.Map<ClienteViewModel, CLIENTE>(vm);
                Int32 volta = baseApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMaster = new List<CLIENTE>();
                SessionMocks.listaCliente = null;
                return RedirectToAction("MontarTelaCliente");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VerCardsCliente()
        {
            // Carrega listas
            if (SessionMocks.listaCliente == null)
            {
                listaMaster = baseApp.GetAllItens();
                SessionMocks.listaCliente = listaMaster;
            }
            ViewBag.Listas = SessionMocks.listaCliente;
            ViewBag.Title = "Clientes";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(baseApp.GetAllTipos(), "CACL_CD_ID", "CACL_NM_NOME");
            ViewBag.Filiais = new SelectList(baseApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Indicadores
            ViewBag.Clientes = baseApp.GetAllItens().Count;

            // Abre view
            objeto = new CLIENTE();
            SessionMocks.voltaCliente = 2;
            return View(objeto);
        }

        [HttpGet]
        public ActionResult VerAnexoCliente(Int32 id)
        {
            // Prepara view
            CLIENTE_ANEXO item = baseApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult ShowImage()
        {
            byte[] imgbytes = System.IO.File.ReadAllBytes(SessionMocks.arquivo);
            return File(imgbytes, "image/jpeg");
        }

        public ActionResult VoltarAnexoCliente()
        {
            return RedirectToAction("EditarCliente", new { id = SessionMocks.idVolta });
        }

        public FileResult DownloadCliente(Int32 id)
        {
            CLIENTE_ANEXO item = baseApp.GetAnexoById(id);
            String arquivo = item.CLAN_AQ_ARQUIVO;
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
        public ActionResult UploadFileCliente(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            CLIENTE item = baseApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Clientes/" + item.CLIE_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            CLIENTE_ANEXO foto = new CLIENTE_ANEXO();
            foto.CLAN_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.CLAN_DT_ANEXO = DateTime.Today;
            foto.CLAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            foto.CLAN_IN_TIPO = tipo;
            foto.CLAN_NM_TITULO = fileName;
            foto.CLIE_CD_ID = item.CLIE_CD_ID;

            item.CLIENTE_ANEXO.Add(foto);
            objetoAntes = item;
            Int32 volta = baseApp.ValidateEdit(item, objetoAntes);
            return RedirectToAction("VoltarAnexoCliente");
        }

        [HttpPost]
        public ActionResult UploadFotoCliente(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            CLIENTE item = baseApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Clientes/" + item.CLIE_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.CLIE_AQ_FOTO = "~" + caminho + fileName;
            objetoAntes = item;
            Int32 volta = baseApp.ValidateEdit(item, objetoAntes);
            return RedirectToAction("VoltarAnexoCliente");
        }

        public ActionResult BuscarCEPCliente(CLIENTE item)
        {
            return RedirectToAction("IncluirClienteEspecial", new { objeto = item});
        }

       [HttpGet]
        public ActionResult MontarTelaFornecedor()
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
            if (SessionMocks.listaFornecedor == null)
            {
                listaMasterForn = fornApp.GetAllItens();
                SessionMocks.listaFornecedor = listaMasterForn;
            }
            ViewBag.Listas = SessionMocks.listaFornecedor;
            ViewBag.Title = "Fornecedores";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Indicadores
            ViewBag.Fornecedores = fornApp.GetAllItens().Count;
            
            // Abre view
            objetoForn = new FORNECEDOR();
            SessionMocks.voltaFornecedor = 1;
            return View(objetoForn);
        }

        public ActionResult RetirarFiltroFornecedor()
        {
            SessionMocks.listaFornecedor = null;
            if (SessionMocks.voltaFornecedor == 2)
            {
                return RedirectToAction("VerCardsFornecedor");
            }
            return RedirectToAction("MontarTelaFornecedor");
        }

        public ActionResult MostrarTudoFornecedor()
        {
            listaMasterForn = fornApp.GetAllItensAdm();
            SessionMocks.listaFornecedor = listaMasterForn;
            if (SessionMocks.voltaFornecedor == 2)
            {
                return RedirectToAction("VerCardsFornecedor");
            }
            return RedirectToAction("MontarTelaFornecedor");
        }

        [HttpPost]
        public ActionResult FiltrarFornecedor(FORNECEDOR item)
        {
            try
            {
                // Executa a operação
                List<FORNECEDOR> listaObj = new List<FORNECEDOR>();
                Int32 volta = fornApp.ExecuteFilter(item.CAFO_CD_ID, item.FORN_NM_NOME, item.FORN_NR_CPF, item.FORN_NR_CNPJ, item.FORN_NM_EMAIL, item.FORN_NM_CIDADE, item.FORN_SG_UF, item.FORN_NM_REDES_SOCIAIS, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaMasterForn = listaObj;
                SessionMocks.listaFornecedor = listaObj;
                if (SessionMocks.voltaFornecedor == 2)
                {
                    return RedirectToAction("VerCardsFornecedor");
                }
                return RedirectToAction("MontarTelaFornecedor");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaFornecedor");
            }
        }

        public ActionResult VoltarBaseFornecedor()
        {
            listaMasterForn = new List<FORNECEDOR>();
            SessionMocks.listaFornecedor = null;
            return RedirectToAction("MontarTelaFornecedor");
        }

       [HttpGet]
        public ActionResult IncluirFornecedor()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            FORNECEDOR item = new FORNECEDOR();
            FornecedorViewModel vm = Mapper.Map<FORNECEDOR, FornecedorViewModel>(item);
            vm.ASSI_CD_ID = SessionMocks.IdAssinante.Value;
            vm.FORN_DT_CADASTRO = DateTime.Today;
            vm.FORN_IN_ATIVO = 1;
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            vm.FILI_CD_ID = usuario.COLABORADOR.FILI_CD_ID;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirFornecedor(FornecedorViewModel vm)
        {
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    FORNECEDOR item = Mapper.Map<FornecedorViewModel, FORNECEDOR>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = fornApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0020", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Carrega foto e processa alteracao
                    item.FORN_AQ_FOTO = "~/Imagens/Base/FotoBase.jpg";
                    volta = fornApp.ValidateEdit(item, item, usuarioLogado);

                    // Cria pastas
                    String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Fornecedores/" + item.FORN_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Fornecedores/" + item.FORN_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    
                    // Sucesso
                    listaMasterForn = new List<FORNECEDOR>();
                    SessionMocks.listaFornecedor = null;
                    if (SessionMocks.voltaFornecedor == 2)
                    {
                        return RedirectToAction("VerCardsFornecedor");
                    }
                    return RedirectToAction("MontarTelaFornecedor");
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
        public ActionResult EditarFornecedor(Int32 id)
        {
            // Prepara view
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            FORNECEDOR item = fornApp.GetItemById(id);
            objetoFornAntes = item;
            SessionMocks.fornecedor = item;
            SessionMocks.idVolta = id;
            FornecedorViewModel vm = Mapper.Map<FORNECEDOR, FornecedorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFornecedor(FornecedorViewModel vm)
        {
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    FORNECEDOR item = Mapper.Map<FornecedorViewModel, FORNECEDOR>(vm);
                    Int32 volta = fornApp.ValidateEdit(item, objetoFornAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaMasterForn = new List<FORNECEDOR>();
                    SessionMocks.listaFornecedor = null;
                    if (SessionMocks.voltaFornecedor == 2)
                    {
                        return RedirectToAction("VerCardsFornecedor");
                    }
                    return RedirectToAction("MontarTelaFornecedor");
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
        public ActionResult ExcluirFornecedor(Int32 id)
        {
            // Prepara view
            FORNECEDOR item = fornApp.GetItemById(id);
            FornecedorViewModel vm = Mapper.Map<FORNECEDOR, FornecedorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirFornecedor(FornecedorViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                FORNECEDOR item = Mapper.Map<FornecedorViewModel, FORNECEDOR>(vm);
                Int32 volta = fornApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0021", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMasterForn = new List<FORNECEDOR>();
                SessionMocks.listaFornecedor = null;
                return RedirectToAction("MontarTelaFornecedor");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarFornecedor(Int32 id)
        {
            // Prepara view
            FORNECEDOR item = fornApp.GetItemById(id);
            FornecedorViewModel vm = Mapper.Map<FORNECEDOR, FornecedorViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarFornecedor(FornecedorViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                FORNECEDOR item = Mapper.Map<FornecedorViewModel, FORNECEDOR>(vm);
                Int32 volta = fornApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMasterForn = new List<FORNECEDOR>();
                SessionMocks.listaFornecedor = null;
                return RedirectToAction("MontarTelaFornecedor");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult VerCardsFornecedor()
        {
            // Carrega listas
            if (SessionMocks.listaFornecedor == null)
            {
                listaMasterForn = fornApp.GetAllItens();
                SessionMocks.listaFornecedor = listaMasterForn;
            }
            ViewBag.Listas = SessionMocks.listaFornecedor;
            ViewBag.Title = "Fornecedores";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(fornApp.GetAllTipos(), "CAFO_CD_ID", "CAFO_NM_NOME");
            ViewBag.Filiais = new SelectList(fornApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");

            // Indicadores
            ViewBag.Fornecedores = fornApp.GetAllItens().Count;

            // Abre view
            objetoForn = new FORNECEDOR();
            SessionMocks.voltaFornecedor = 2;
            return View(objetoForn);
        }

        [HttpGet]
        public ActionResult VerAnexoFornecedor(Int32 id)
        {
            // Prepara view
            FORNECEDOR_ANEXO item = fornApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult VoltarAnexoFornecedor()
        {
            return RedirectToAction("EditarFornecedor", new { id = SessionMocks.idVolta });
        }

        public FileResult DownloadFornecedor(Int32 id)
        {
            FORNECEDOR_ANEXO item = fornApp.GetAnexoById(id);
            String arquivo = item.FOAN_AQ_ARQUVO;
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
        public ActionResult UploadFileFornecedor(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            FORNECEDOR item = fornApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Fornecedores/" + item.FORN_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            FORNECEDOR_ANEXO foto = new FORNECEDOR_ANEXO();
            foto.FOAN_AQ_ARQUVO = "~" + caminho + fileName;
            foto.FOAN_DT_ANEXO = DateTime.Today;
            foto.FOAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            foto.FOAN_IN_TIPO = tipo;
            foto.FOAN_NM_TITULO = fileName;
            foto.FORN_CD_ID = item.FORN_CD_ID;

            item.FORNECEDOR_ANEXO.Add(foto);
            objetoFornAntes = item;
            Int32 volta = fornApp.ValidateEdit(item, objetoFornAntes);
            return RedirectToAction("VoltarAnexoFornecedor");
        }

        [HttpPost]
        public ActionResult UploadFotoFornecedor(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            FORNECEDOR item = fornApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Fornecedores/" + item.FORN_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.FORN_AQ_FOTO = "~" + caminho + fileName;
            objetoFornAntes = item;
            Int32 volta = fornApp.ValidateEdit(item, objetoFornAntes);
            return RedirectToAction("VoltarAnexoFornecedor");
        }

        public ActionResult BuscarCEPFornecedor(FORNECEDOR item)
        {
            return RedirectToAction("IncluirFornecedorEspecial", new { objeto = item});
        }
    }
}