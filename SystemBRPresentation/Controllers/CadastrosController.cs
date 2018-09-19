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
        private readonly IProdutoAppService prodApp;
        private readonly IMateriaPrimaAppService matApp;
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
        PRODUTO objetoProd = new PRODUTO();
        PRODUTO objetoProdAntes = new PRODUTO();
        List<PRODUTO> listaMasterProd = new List<PRODUTO>();
        MATERIA_PRIMA objetoMat = new MATERIA_PRIMA();
        MATERIA_PRIMA objetoMatAntes = new MATERIA_PRIMA();
        List<MATERIA_PRIMA> listaMasterMat = new List<MATERIA_PRIMA>();

        public CadastrosController(IClienteAppService baseApps, ILogAppService logApps, IMatrizAppService matrizApps, IFornecedorAppService fornApps, IProdutoAppService prodApps, IMateriaPrimaAppService matApps)
        {
            baseApp = baseApps;
            logApp = logApps;
            matrizApp = matrizApps;
            fornApp = fornApps;
            prodApp = prodApps;
            matApp = matApps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            CLIENTE item = new CLIENTE();
            ClienteViewModel vm = Mapper.Map<CLIENTE, ClienteViewModel>(item);
            return View(vm);
        }

        public ActionResult Voltar()
        {
            listaMaster = new List<CLIENTE>();
            listaMasterForn = new List<FORNECEDOR>();
            listaMasterProd = new List<PRODUTO>();
            listaMasterMat = new List<MATERIA_PRIMA>();
            SessionMocks.listaCliente = null;
            SessionMocks.listaFornecedor = null;
            SessionMocks.listaProduto = null;
            SessionMocks.listaMateria = null;
            SessionMocks.filtroMateria = null;
            return RedirectToAction("CarregarAdmin", "BaseAdmin");
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
            if (SessionMocks.filtroCliente != null)
            {
                objeto = SessionMocks.filtroCliente;
            }
            SessionMocks.voltaCliente = 1;
            return View(objeto);
        }

        public ActionResult RetirarFiltroCliente()
        {
            SessionMocks.listaCliente = null;
            SessionMocks.filtroCliente = null;
            if (SessionMocks.voltaCliente == 2)
            {
                return RedirectToAction("VerCardsCliente");
            }
            return RedirectToAction("MontarTelaCliente");
        }

        public ActionResult MostrarTudoCliente()
        {
            listaMaster = baseApp.GetAllItensAdm();
            SessionMocks.filtroCliente = null;
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
                SessionMocks.filtroCliente = item;
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
            //listaMaster = new List<CLIENTE>();
            //SessionMocks.listaCliente = null;
            if (SessionMocks.voltaCliente == 2)
            {
                return RedirectToAction("VerCardsCliente");
            }
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
            if (SessionMocks.filtroFornecedor != null)
            {
                objetoForn = SessionMocks.filtroFornecedor;
            }
            return View(objetoForn);
        }

        public ActionResult RetirarFiltroFornecedor()
        {
            SessionMocks.listaFornecedor = null;
            SessionMocks.filtroFornecedor = null;
            if (SessionMocks.voltaFornecedor == 2)
            {
                return RedirectToAction("VerCardsFornecedor");
            }
            return RedirectToAction("MontarTelaFornecedor");
        }

        public ActionResult MostrarTudoFornecedor()
        {
            listaMasterForn = fornApp.GetAllItensAdm();
            SessionMocks.filtroFornecedor = null;
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
                SessionMocks.filtroFornecedor = item;
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
            //listaMasterForn = new List<FORNECEDOR>();
            //SessionMocks.listaFornecedor = null;
            if (SessionMocks.voltaFornecedor == 2)
            {
                return RedirectToAction("VerCardsFornecedor");
            }
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

        [HttpGet]
        public ActionResult MontarTelaProduto()
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
            if (SessionMocks.listaProduto == null)
            {
                listaMasterProd = prodApp.GetAllItens();
                SessionMocks.listaProduto = listaMasterProd;
            }
            ViewBag.Listas = SessionMocks.listaProduto;
            ViewBag.Title = "Produtos";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Indicadores
            ViewBag.Produtos = prodApp.GetAllItens().Count;
            
            // Abre view
            objetoProd = new PRODUTO();
            SessionMocks.voltaProduto = 1;
            if (SessionMocks.filtroProduto != null)
            {
                objetoProd = SessionMocks.filtroProduto;
            }
            return View(objetoProd);
        }

        public ActionResult RetirarFiltroProduto()
        {
            SessionMocks.listaProduto = null;
            SessionMocks.filtroProduto = null;
            if (SessionMocks.voltaProduto == 2)
            {
                return RedirectToAction("VerCardsProduto");
            }
            return RedirectToAction("MontarTelaProduto");
        }

        public ActionResult MostrarTudoProduto()
        {
            listaMasterProd = prodApp.GetAllItensAdm();
            SessionMocks.filtroProduto = null;
            SessionMocks.listaProduto = listaMasterProd;
            if (SessionMocks.voltaProduto == 2)
            {
                return RedirectToAction("VerCardsProduto");
            }
            return RedirectToAction("MontarTelaProduto");
        }

        [HttpPost]
        public ActionResult FiltrarProduto(PRODUTO item)
        {
            try
            {
                // Executa a operação
                List<PRODUTO> listaObj = new List<PRODUTO>();
                SessionMocks.filtroProduto = item;
                Int32 volta = prodApp.ExecuteFilter(item.CAPR_CD_ID, item.PROD_NM_NOME, item.PROD_DS_DESCRICAO, item.FILI_CD_ID, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaMasterProd = listaObj;
                SessionMocks.listaProduto = listaObj;
                if (SessionMocks.voltaProduto == 2)
                {
                    return RedirectToAction("VerCardsProduto");
                }
                return RedirectToAction("MontarTelaProduto");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaProduto");
            }
        }

        public ActionResult VoltarBaseProduto()
        {
            //listaMasterProd = new List<PRODUTO>();
            //SessionMocks.listaProduto = null;
            if (SessionMocks.voltaProduto == 2)
            {
                return RedirectToAction("VerCardsProduto");
            }
            return RedirectToAction("MontarTelaProduto");
        }

        [HttpGet]
        public ActionResult IncluirProduto()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            PRODUTO item = new PRODUTO();
            ProdutoViewModel vm = Mapper.Map<PRODUTO, ProdutoViewModel>(item);
            vm.ASSI_CD_ID = SessionMocks.IdAssinante.Value;
            vm.PROD_DT_CADASTRO = DateTime.Today;
            vm.PROD_IN_ATIVO = 1;
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            vm.FILI_CD_ID = usuario.COLABORADOR.FILI_CD_ID;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirProduto(ProdutoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    PRODUTO item = Mapper.Map<ProdutoViewModel, PRODUTO>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = prodApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0022", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Carrega foto e processa alteracao
                    item.PROD_AQ_FOTO = "~/Imagens/Base/FotoBase.jpg";
                    volta = prodApp.ValidateEdit(item, item, usuarioLogado);

                    // Cria pastas
                    String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Produtos/" + item.PROD_CD_ID.ToString() + "/Fotos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Produtos/" + item.PROD_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    
                    // Sucesso
                    listaMasterProd = new List<PRODUTO>();
                    SessionMocks.listaProduto = null;
                    if (SessionMocks.voltaProduto == 2)
                    {
                        return RedirectToAction("VerCardsProduto");
                    }
                    return RedirectToAction("MontarTelaProduto");
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
        public ActionResult EditarProduto(Int32 id)
        {
            // Prepara view
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            PRODUTO item = prodApp.GetItemById(id);
            objetoProdAntes = item;
            SessionMocks.produto = item;
            SessionMocks.idVolta = id;
            ProdutoViewModel vm = Mapper.Map<PRODUTO, ProdutoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProduto(ProdutoViewModel vm)
        {
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            //if (ModelState.IsValid)
            //{
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    PRODUTO item = Mapper.Map<ProdutoViewModel, PRODUTO>(vm);
                    Int32 volta = prodApp.ValidateEdit(item, objetoProdAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaMasterProd = new List<PRODUTO>();
                    SessionMocks.listaProduto = null;
                    if (SessionMocks.voltaProduto == 2)
                    {
                        return RedirectToAction("VerCardsProduto");
                    }
                    return RedirectToAction("MontarTelaProduto");
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
        public ActionResult ExcluirProduto(Int32 id)
        {
            // Prepara view
            PRODUTO item = prodApp.GetItemById(id);
            ProdutoViewModel vm = Mapper.Map<PRODUTO, ProdutoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirProduto(ProdutoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                PRODUTO item = Mapper.Map<ProdutoViewModel, PRODUTO>(vm);
                Int32 volta = prodApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0023", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMasterProd = new List<PRODUTO>();
                SessionMocks.listaProduto = null;
                return RedirectToAction("MontarTelaProduto");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarProduto(Int32 id)
        {
            // Prepara view
            PRODUTO item = prodApp.GetItemById(id);
            ProdutoViewModel vm = Mapper.Map<PRODUTO, ProdutoViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarProduto(ProdutoViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                PRODUTO item = Mapper.Map<ProdutoViewModel, PRODUTO>(vm);
                Int32 volta = prodApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMasterProd = new List<PRODUTO>();
                SessionMocks.listaProduto = null;
                return RedirectToAction("MontarTelaProduto");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VerCardsProduto()
        {
            // Carrega listas
            if (SessionMocks.listaProduto == null)
            {
                listaMasterProd = prodApp.GetAllItens();
                SessionMocks.listaProduto = listaMasterProd;
            }
            ViewBag.Listas = SessionMocks.listaProduto;
            ViewBag.Title = "Produtos";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAPR_CD_ID", "CAPR_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Indicadores
            ViewBag.Produtos = prodApp.GetAllItens().Count;

            // Abre view
            objetoProd = new PRODUTO();
            SessionMocks.voltaProduto = 2;
            return View(objetoProd);
        }

        [HttpGet]
        public ActionResult VerAnexoProduto(Int32 id)
        {
            // Prepara view
            PRODUTO_ANEXO item = prodApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult VoltarAnexoProduto()
        {
            return RedirectToAction("EditarProduto", new { id = SessionMocks.idVolta });
        }

        public FileResult DownloadProduto(Int32 id)
        {
            PRODUTO_ANEXO item = prodApp.GetAnexoById(id);
            String arquivo = item.PRAN_AQ_ARQUIVO;
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
        public ActionResult UploadFileProduto(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            PRODUTO item = prodApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Produtos/" + item.PROD_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            PRODUTO_ANEXO foto = new PRODUTO_ANEXO();
            foto.PRAN_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.PRAN_DT_ANEXO = DateTime.Today;
            foto.PRAN_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            foto.PRAN_IN_TIPO = tipo;
            foto.PRAN_NM_TITULO = fileName;
            foto.PROD_CD_ID = item.PROD_CD_ID;

            item.PRODUTO_ANEXO.Add(foto);
            objetoProdAntes = item;
            Int32 volta = prodApp.ValidateEdit(item, objetoProdAntes);
            return RedirectToAction("VoltarAnexoProduto");
        }

        [HttpPost]
        public ActionResult UploadFotoProduto(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            PRODUTO item = prodApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Produtos/" + item.PROD_CD_ID.ToString() + "/Fotos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            item.PROD_AQ_FOTO = "~" + caminho + fileName;
            objetoProdAntes = item;
            Int32 volta = prodApp.ValidateEdit(item, objetoProdAntes);
            return RedirectToAction("VoltarAnexoProduto");
        }

        public ActionResult BuscarCEPProduto(PRODUTO item)
        {
            return RedirectToAction("IncluirProdutoEspecial", new { objeto = item});
        }

        public ActionResult VerMovimentacaoEstoqueProduto()
        {
            // Prepara view
            PRODUTO item = prodApp.GetItemById(SessionMocks.idVolta);
            objetoProdAntes = item;
            ProdutoViewModel vm = Mapper.Map<PRODUTO, ProdutoViewModel>(item);
            return View(vm);
        }

       [HttpGet]
        public ActionResult MontarTelaMateria()
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
            if (SessionMocks.listaMateria == null)
            {
                listaMasterMat = matApp.GetAllItens();
                SessionMocks.listaMateria = listaMasterMat;
            }
            ViewBag.Listas = SessionMocks.listaMateria;
            ViewBag.Title = "Matéria-Prima";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(matApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(matApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(matApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Indicadores
            ViewBag.Materias = matApp.GetAllItens().Count;
            
            // Abre view
            objetoMat = new MATERIA_PRIMA();
            SessionMocks.voltaMateria = 1;
            if (SessionMocks.filtroMateria != null)
            {
                objetoMat = SessionMocks.filtroMateria;
            }
            return View(objetoMat);
        }

        public ActionResult RetirarFiltroMateria()
        {
            SessionMocks.listaMateria = null;
            SessionMocks.filtroMateria = null;
            if (SessionMocks.voltaMateria == 2)
            {
                return RedirectToAction("VerCardsMateria");
            }
            return RedirectToAction("MontarTelaMateria");
        }

        public ActionResult MostrarTudoMateria()
        {
            listaMasterMat = matApp.GetAllItensAdm();
            SessionMocks.listaMateria = listaMasterMat;
            SessionMocks.filtroMateria = null;
            if (SessionMocks.voltaMateria == 2)
            {
                return RedirectToAction("VerCardsMateria");
            }
            return RedirectToAction("MontarTelaMateria");
        }

        [HttpPost]
        public ActionResult FiltrarMateria(MATERIA_PRIMA item)
        {
            try
            {
                // Executa a operação
                List<MATERIA_PRIMA> listaObj = new List<MATERIA_PRIMA>();
                SessionMocks.filtroMateria = item;
                Int32 volta = matApp.ExecuteFilter(item.CAMA_CD_ID, item.MAPR_NM_NOME, item.MAPR_DS_DESCRICAO, item.FILI_CD_ID, out listaObj);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0010", CultureInfo.CurrentCulture);
                }

                // Sucesso
                listaMasterMat = listaObj;
                SessionMocks.listaMateria = listaObj;
                if (SessionMocks.voltaMateria == 2)
                {
                    return RedirectToAction("VerCardsMateria");
                }
                return RedirectToAction("MontarTelaMateria");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("MontarTelaProduto");
            }
        }

        public ActionResult VoltarBaseMateria()
        {
            //listaMasterMat = new List<MATERIA_PRIMA>();
            //SessionMocks.listaMateria = null;
            if (SessionMocks.voltaMateria == 2)
            {
                return RedirectToAction("VerCardsMateria");
            }
            return RedirectToAction("MontarTelaMateria");
        }

        [HttpGet]
        public ActionResult IncluirMateria()
        {
            // Prepara listas
            ViewBag.Tipos = new SelectList(matApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(matApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(matApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Prepara view
            USUARIO usuario = SessionMocks.UserCredentials;
            MATERIA_PRIMA item = new MATERIA_PRIMA();
            MateriaPrimaViewModel vm = Mapper.Map<MATERIA_PRIMA, MateriaPrimaViewModel>(item);
            vm.ASSI_CD_ID = SessionMocks.IdAssinante.Value;
            vm.MAPR_DT_CADASTRO = DateTime.Today;
            vm.MAPR_IN_ATIVO = 1;
            vm.MATR_CD_ID = SessionMocks.Matriz.MATR_CD_ID;
            vm.FILI_CD_ID = usuario.COLABORADOR.FILI_CD_ID;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncluirMateria(MateriaPrimaViewModel vm)
        {
            ViewBag.Tipos = new SelectList(matApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(matApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(matApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            if (ModelState.IsValid)
            {
                try
                {
                    // Executa a operação
                    MATERIA_PRIMA item = Mapper.Map<MateriaPrimaViewModel, MATERIA_PRIMA>(vm);
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    Int32 volta = matApp.ValidateCreate(item, usuarioLogado);

                    // Verifica retorno
                    if (volta == 1)
                    {
                        ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0024", CultureInfo.CurrentCulture);
                        return View(vm);
                    }

                    // Cria pastas
                    String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Materia/" + item.MAPR_CD_ID.ToString() + "/Anexos/";
                    Directory.CreateDirectory(Server.MapPath(caminho));
                    
                    // Sucesso
                    listaMasterMat = new List<MATERIA_PRIMA>();
                    SessionMocks.listaMateria = null;
                    if (SessionMocks.voltaMateria == 2)
                    {
                        return RedirectToAction("VerCardsMateria");
                    }
                    return RedirectToAction("MontarTelaMateria");
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
        public ActionResult EditarMateria(Int32 id)
        {
            // Prepara view
            ViewBag.Tipos = new SelectList(matApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(matApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(matApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            MATERIA_PRIMA item = matApp.GetItemById(id);
            objetoMatAntes = item;
            SessionMocks.materiaPrima = item;
            SessionMocks.idVolta = id;
            MateriaPrimaViewModel vm = Mapper.Map<MATERIA_PRIMA, MateriaPrimaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMateria(MateriaPrimaViewModel vm)
        {
            ViewBag.Tipos = new SelectList(matApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(matApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(matApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");
            //if (ModelState.IsValid)
            //{
                try
                {
                    // Executa a operação
                    USUARIO usuarioLogado = SessionMocks.UserCredentials;
                    MATERIA_PRIMA item = Mapper.Map<MateriaPrimaViewModel, MATERIA_PRIMA>(vm);
                    Int32 volta = matApp.ValidateEdit(item, objetoMatAntes, usuarioLogado);

                    // Verifica retorno

                    // Sucesso
                    listaMasterMat = new List<MATERIA_PRIMA>();
                    SessionMocks.listaMateria = null;
                    if (SessionMocks.voltaMateria == 2)
                    {
                        return RedirectToAction("VerCardsMateria");
                    }
                    return RedirectToAction("MontarTelaMateria");
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
        public ActionResult ExcluirMateria(Int32 id)
        {
            // Prepara view
            MATERIA_PRIMA item = matApp.GetItemById(id);
            MateriaPrimaViewModel vm = Mapper.Map<MATERIA_PRIMA, MateriaPrimaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ExcluirMateria(MateriaPrimaViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                MATERIA_PRIMA item = Mapper.Map<MateriaPrimaViewModel, MATERIA_PRIMA>(vm);
                Int32 volta = matApp.ValidateDelete(item, usuarioLogado);

                // Verifica retorno
                if (volta == 1)
                {
                    ViewBag.Message = SystemBR_Resource.ResourceManager.GetString("M0025", CultureInfo.CurrentCulture);
                    return View(vm);
                }

                // Sucesso
                listaMasterMat = new List<MATERIA_PRIMA>();
                SessionMocks.listaMateria = null;
                return RedirectToAction("MontarTelaMateria");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        [HttpGet]
        public ActionResult ReativarMateria(Int32 id)
        {
            // Prepara view
            MATERIA_PRIMA item = matApp.GetItemById(id);
            MateriaPrimaViewModel vm = Mapper.Map<MATERIA_PRIMA, MateriaPrimaViewModel>(item);
            return View(vm);
        }

        [HttpPost]
        public ActionResult ReativarMateria(MateriaPrimaViewModel vm)
        {
            try
            {
                // Executa a operação
                USUARIO usuarioLogado = SessionMocks.UserCredentials;
                MATERIA_PRIMA item = Mapper.Map<MateriaPrimaViewModel, MATERIA_PRIMA>(vm);
                Int32 volta = matApp.ValidateReativar(item, usuarioLogado);

                // Verifica retorno

                // Sucesso
                listaMasterMat = new List<MATERIA_PRIMA>();
                SessionMocks.listaMateria = null;
                return RedirectToAction("MontarTelaMateria");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(objeto);
            }
        }

        public ActionResult VerCardsMateria()
        {
            // Carrega listas
            if (SessionMocks.listaMateria == null)
            {
                listaMasterMat = matApp.GetAllItens();
                SessionMocks.listaMateria = listaMasterMat;
            }
            ViewBag.Listas = SessionMocks.listaMateria;
            ViewBag.Title = "Matéria Prima";
            SessionMocks.Matriz = matrizApp.GetAllItens().FirstOrDefault();
            ViewBag.Tipos = new SelectList(prodApp.GetAllTipos(), "CAMA_CD_ID", "CAMA_NM_NOME");
            ViewBag.Filiais = new SelectList(prodApp.GetAllFilial(), "FILI_CD_ID", "FILI_NM_NOME");
            ViewBag.Unidades = new SelectList(prodApp.GetAllUnidades(), "UNID_CD_ID", "UNID_NM_NOME");

            // Indicadores
            ViewBag.Materias = matApp.GetAllItens().Count;

            // Abre view
            objetoMat = new MATERIA_PRIMA();
            if (SessionMocks.filtroMateria != null)
            {
                objetoMat = SessionMocks.filtroMateria;
            }
            SessionMocks.voltaMateria = 2;
            return View(objetoMat);
        }

        [HttpGet]
        public ActionResult VerAnexoMateria(Int32 id)
        {
            // Prepara view
            MATERIA_PRIMA_ANEXO item = matApp.GetAnexoById(id);
            return View(item);
        }

        public ActionResult VoltarAnexoMateria()
        {
            return RedirectToAction("EditarMateria", new { id = SessionMocks.idVolta });
        }

        public FileResult DownloadMateria(Int32 id)
        {
            MATERIA_PRIMA_ANEXO item = matApp.GetAnexoById(id);
            String arquivo = item.MAPA_AQ_ARQUIVO;
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
        public ActionResult UploadFileMateria(HttpPostedFileBase file)
        {
            if (file == null)
                return Content("Nenhum arquivo selecionado");

            MATERIA_PRIMA item = matApp.GetById(SessionMocks.idVolta);
            USUARIO usu = SessionMocks.UserCredentials;
            var fileName = Path.GetFileName(file.FileName);
            String caminho = "/Imagens/" + SessionMocks.IdAssinante.Value.ToString() + "/Materias/" + item.MAPR_CD_ID.ToString() + "/Anexos/";
            String path = Path.Combine(Server.MapPath(caminho), fileName);
            file.SaveAs(path);

            //Recupera tipo de arquivo
            extensao = Path.GetExtension(fileName);
            String a = extensao;

            // Gravar registro
            MATERIA_PRIMA_ANEXO foto = new MATERIA_PRIMA_ANEXO();
            foto.MAPA_AQ_ARQUIVO = "~" + caminho + fileName;
            foto.MAPA_DT_ANEXO = DateTime.Today;
            foto.MAPA_IN_ATIVO = 1;
            Int32 tipo = 3;
            if (extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".GIF" || extensao.ToUpper() == ".PNG" || extensao.ToUpper() == ".JPEG")
            {
                tipo = 1;
            }
            if (extensao.ToUpper() == ".MP4" || extensao.ToUpper() == ".AVI" || extensao.ToUpper() == ".MPEG")
            {
                tipo = 2;
            }
            foto.MAPA_IN_TIPO = tipo;
            foto.MAPA_NM_TITULO = fileName;
            foto.MAPR_D_ID = item.MAPR_CD_ID;

            item.MATERIA_PRIMA_ANEXO.Add(foto);
            objetoMatAntes = item;
            Int32 volta = matApp.ValidateEdit(item, objetoMatAntes);
            return RedirectToAction("VoltarAnexoMateria");
        }

        public ActionResult VerMovimentacaoEstoqueMateria()
        {
            // Prepara view
            MATERIA_PRIMA item = matApp.GetItemById(SessionMocks.idVolta);
            objetoMatAntes = item;
            MateriaPrimaViewModel vm = Mapper.Map<MATERIA_PRIMA, MateriaPrimaViewModel>(item);
            return View(vm);
        }
    }
}