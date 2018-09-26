using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using ModelServices.Interfaces.Repositories;
using ApplicationServices.Services;
using ModelServices.EntitiesServices;
using DataServices.Repositories;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Presentation.Start.NinjectWebCommons), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Presentation.Start.NinjectWebCommons), "Stop")]

namespace Presentation.Start
{
    public class NinjectWebCommons
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind<IUsuarioAppService>().To<UsuarioAppService>();
            kernel.Bind<ILogAppService>().To<LogAppService>();
            kernel.Bind<IPerfilAppService>().To<PerfilAppService>();
            kernel.Bind<IConfiguracaoAppService>().To<ConfiguracaoAppService>();
            kernel.Bind<IBancoAppService>().To<BancoAppService>();
            kernel.Bind<IContaBancariaAppService>().To<ContaBancariaAppService>();
            kernel.Bind<IMatrizAppService>().To<MatrizAppService>();
            kernel.Bind<IFilialAppService>().To<FilialAppService>();
            kernel.Bind<IClienteAppService>().To<ClienteAppService>();
            kernel.Bind<IFornecedorAppService>().To<FornecedorAppService>();
            kernel.Bind<IProdutoAppService>().To<ProdutoAppService>();
            kernel.Bind<IMateriaPrimaAppService>().To<MateriaPrimaAppService>();
            kernel.Bind<IServicoAppService>().To<ServicoAppService>();
            kernel.Bind<ITransportadoraAppService>().To<TransportadoraAppService>();
            kernel.Bind<IPatrimonioAppService>().To<PatrimonioAppService>();
            kernel.Bind<IEquipamentoAppService>().To<EquipamentoAppService>();
            kernel.Bind<ICargoAppService>().To<CargoAppService>();
            kernel.Bind<IValorComissaoAppService>().To<ValorComissaoAppService>();

            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind<IUsuarioService>().To<UsuarioService>();
            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IPerfilService>().To<PerfilService>();
            kernel.Bind<IConfiguracaoService>().To<ConfiguracaoService>();
            kernel.Bind<IBancoService>().To<BancoService>();
            kernel.Bind<IContaBancariaService>().To<ContaBancariaService>();
            kernel.Bind<IMatrizService>().To<MatrizService>();
            kernel.Bind<IFilialService>().To<FilialService>();
            kernel.Bind<IClienteService>().To<ClienteService>();
            kernel.Bind<IFornecedorService>().To<FornecedorService>();
            kernel.Bind<IProdutoService>().To<ProdutoService>();
            kernel.Bind<IMovimentoEstoqueProdutoService>().To<MovimentoEstoqueProdutoService>();
            kernel.Bind<IMateriaPrimaService>().To<MateriaPrimaService>();
            kernel.Bind<IMovimentoEstoqueMateriaService>().To<MovimentoEstoqueMateriaService>();
            kernel.Bind<IServicoService>().To<ServicoService>();
            kernel.Bind<ITransportadoraService>().To<TransportadoraService>();
            kernel.Bind<IPatrimonioService>().To<PatrimonioService>();
            kernel.Bind<IEquipamentoService>().To<EquipamentoService>();
            kernel.Bind<ICargoService>().To<CargoService>();
            kernel.Bind<IValorComissaoService>().To<ValorComissaoService>();

            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));
            kernel.Bind<IConfiguracaoRepository>().To<ConfiguracaoRepository>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>();
            kernel.Bind<ILogRepository>().To<LogRepository>();
            kernel.Bind<IPerfilRepository>().To<PerfilRepository>();
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>();
            kernel.Bind<IBancoRepository>().To<BancoRepository>();
            kernel.Bind<IContaBancariaRepository>().To<ContaBancariaRepository>();
            kernel.Bind<ITipoContaRepository>().To<TipoContaRepository>();
            kernel.Bind<IMatrizRepository>().To<MatrizRepository>();
            kernel.Bind<IFilialRepository>().To<FilialRepository>();
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<ICategoriaClienteRepository>().To<CategoriaClienteRepository>();
            kernel.Bind<IClienteAnexoRepository>().To<ClienteAnexoRepository>();
            kernel.Bind<IFornecedorRepository>().To<FornecedorRepository>();
            kernel.Bind<IFornecedorAnexoRepository>().To<FornecedorAnexoRepository>();
            kernel.Bind<ICategoriaFornecedorRepository>().To<CategoriaFornecedorRepository>();
            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();
            kernel.Bind<IProdutoAnexoRepository>().To<ProdutoAnexoRepository>();
            kernel.Bind<ICategoriaProdutoRepository>().To<CategoriaProdutoRepository>();
            kernel.Bind<IUnidadeRepository>().To<UnidadeRepository>();
            kernel.Bind<IMovimentoEstoqueProdutoRepository>().To<MovimentoEstoqueProdutoRepository>();
            kernel.Bind<ICategoriaMateriaPrimaRepository>().To<CategoriaMateriaPrimaRepository>();
            kernel.Bind<IMateriaPrimaRepository>().To<MateriaPrimaRepository>();
            kernel.Bind<IMateriaPrimaAnexoRepository>().To<MateriaPrimaAnexoRepository>();
            kernel.Bind<IMovimentoEstoqueMateriaRepository>().To<MovimentoEstoqueMateriaRepository>();
            kernel.Bind<IServicoRepository>().To<ServicoRepository>();
            kernel.Bind<IServicoAnexoRepository>().To<ServicoAnexoRepository>();
            kernel.Bind<ICategoriaServicoRepository>().To<CategoriaServicoRepository>();
            kernel.Bind<ITransportadoraRepository>().To<TransportadoraRepository>();
            kernel.Bind<ITransportadoraAnexoRepository>().To<TransportadoraAnexoRepository>();
            kernel.Bind<ICategoriaEquipamentoRepository>().To<CategoriaEquipamentoRepository>();
            kernel.Bind<ICategoriaPatrimonioRepository>().To<CategoriaPatrimonioRepository>();
            kernel.Bind<IPatrimonioAnexoRepository>().To<PatrimonioAnexoRepository>();
            kernel.Bind<IEquipamentoAnexoRepository>().To<EquipamentoAnexoRepository>();
            kernel.Bind<IPatrimonioRepository>().To<PatrimonioRepository>();
            kernel.Bind<IEquipementoRepository>().To<EquipamentoRepository>();
            kernel.Bind<ICargoRepository>().To<CargoRepository>();
            kernel.Bind<ITipoComissaoRepository>().To<TipoComissaoRepository>();
            kernel.Bind<IValorComissaoRepository>().To<ValorComissaoRepository>();
            kernel.Bind<IPeriodicidadeRepository>().To<PeriodicidadeRepository>();
            kernel.Bind<IEquipamentoManutencaoRepository>().To<EquipamentoManutencaoRepository>();
        }
    }
}