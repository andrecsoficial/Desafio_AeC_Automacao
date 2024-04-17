using OpenQA.Selenium.Chrome;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Worker.Jobs
{
  
    public class AluraJob : JobBase
    {

        protected IDriverFactoryService _driverFactory { get; init; }
        private INavigator _navigator { get; init; }
        private IConfiguration _configuration { get; init; }

        public AluraJob(ILogger<AluraJob> logger
                        ,IDriverFactoryService driverFactoryService
                        ,INavigator navigator
                        ,IConfiguration configuration) : base(logger) 
        { 
            _driverFactory = driverFactoryService;
            _navigator = navigator;    
            _configuration = configuration;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Inicializando aplicação...");

                SetupDriver();

                //Navegação
                string url = _configuration["Alura:URLs:Principal"];
                string searchWord = _configuration["Alura:Words:SearchWord"];
                var navigationResult = await _navigator.NavigationAlura(url, searchWord);

                _driverFactory.Quit();

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
