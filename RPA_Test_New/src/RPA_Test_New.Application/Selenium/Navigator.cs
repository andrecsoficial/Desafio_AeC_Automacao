using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RPA_Test_New.Application.Selenium.Controllers;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;


namespace RPA_Test_New.Application.Selenium
{
    public class Navigator : INavigator
    {
        private ILogger<Navigator> _logger { get; init; }
        private IConfiguration _configuration { get; init; }
        private AluraController _aluraController { get; init; }

        public Navigator(ILogger<Navigator> logger
                         ,IConfiguration configuration
                         ,AluraController aluraController)
        {
            _logger = logger;
            _configuration = configuration;
            _aluraController = aluraController;
        }

        public async Task<ResultProcess> NavigationAlura(string url, string searchWord)
        {
            _logger.LogInformation("Acessando URL");
            if (_aluraController.Home(url) is null)
                return new(false, "Erro", "Falha ao acessar URL");

            _logger.LogInformation("Efetua pesquisa");
            if (_aluraController.Search(searchWord) is null)
                return new(false, "Erro", "Falha ao efetuar pesquisa");

            _logger.LogInformation("Acessa primeiro item da pesquisa");
            if (_aluraController.Details() is null)
                return new(false, "Erro", "Falha ao exibir detalhes");

            _logger.LogInformation("Executa a extração dos dados");
            if (_aluraController.Extraction() is null)
            {

            }

            return new(true, "Concluído", "Navegação concluída");
        }

    }
}
