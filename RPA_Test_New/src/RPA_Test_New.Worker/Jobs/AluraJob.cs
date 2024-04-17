using OpenQA.Selenium.Chrome;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Worker.Jobs
{
  
    public class AluraJob : JobBase
    {

        protected IDriverFactoryService _driverFactory { get; init; }
        private INavigator _navigator { get; init; }

        public AluraJob(ILogger<AluraJob> logger
                        ,IDriverFactoryService driverFactoryService
                        ,INavigator navigator) : base(logger) 
        { 
            _driverFactory = driverFactoryService;
            _navigator = navigator;    
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Inicializando aplicação...");

                SetupDriver();

                //Navegação
                var navigationResult = await _navigator.NavigationAlura("https://www.alura.com.br/");

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
