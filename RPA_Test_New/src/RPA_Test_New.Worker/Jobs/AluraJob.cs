
using OpenQA.Selenium.Chrome;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Worker.Jobs
{
  
    public class AluraJob : JobBase
    {

        protected IDriverFactoryService _driverFactory { get; init; }

        public AluraJob(ILogger<AluraJob> logger
                        ,IDriverFactoryService driverFactoryService) : base(logger) 
        { 
            _driverFactory = driverFactoryService;
        }

        public override async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Teste da solution");

                SetupDriver();

                Console.WriteLine("Espera...");
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
