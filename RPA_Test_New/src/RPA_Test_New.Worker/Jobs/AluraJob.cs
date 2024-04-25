using OpenQA.Selenium.Chrome;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Worker.Jobs
{
  
    public class AluraJob : JobBase
    {

        protected IDriverFactoryService _driverFactory { get; init; }
        private INavigator _navigator { get; init; }
        private IConfiguration _configuration { get; init; }
        private IRpaRepository _rpaRepository { get; set; }

        public AluraJob(ILogger<AluraJob> logger
                        ,IDriverFactoryService driverFactoryService
                        ,INavigator navigator
                        ,IConfiguration configuration
                        ,IRpaRepository rpaRepository) : base(logger) 
        { 
            _driverFactory = driverFactoryService;
            _navigator = navigator;    
            _configuration = configuration;
            _rpaRepository = rpaRepository;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Inicializando aplicação...");

                //Coleta credencial de acesso
                var aluraCredential = await _rpaRepository.GetCredential();
                if (aluraCredential is null)
                {
                    _logger.LogWarning($"Não há usuário disponível para login! Verificar credenciais no banco de dados");
                    return;
                }

                SetupDriver();

                //Navegação
                string url = _configuration["Alura:URLs:Principal"];
                string searchWord = _configuration["Alura:Words:SearchWord"];
                var navigationResult = await _navigator.NavigationAlura(url, searchWord, aluraCredential);
                if (!navigationResult.sucess)
                    _logger.LogInformation(navigationResult.obs.ToString());

                _driverFactory.Quit();

                Thread.Sleep(10000);

                _logger.LogInformation("Encerrando aplicação...");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha... {ex.Message}");
                throw;
            }
        }

        private void SetupDriver()
        {
            try
            {
                var opts = new ChromeOptions();
                _driverFactory.StartDriver(opts: opts);
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogError($"Ocorreu um erro ao instanciar o driver  - Operação inválida: {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro genérico ao instanciar o driver: {ex.Message}");
            }
        }

        
    }
}
