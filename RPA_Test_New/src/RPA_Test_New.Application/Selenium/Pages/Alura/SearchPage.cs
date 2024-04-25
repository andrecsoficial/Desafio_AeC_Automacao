using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using RPA_Test_New.Application.Selenium.Extensions;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;


namespace RPA_Test_New.Application.Selenium.Pages.Alura
{
    public class SearchPage
    {
        private IDriverFactoryService _driverManager { get; init; }
        private IWebDriver _driver => _driverManager.Instance;
        private IConfiguration _configuration { get; set; }
        private ILogger<SearchPage> _logger { get; init; }
        private IRpaRepository _rpaRepository { get; set; }

        public SearchPage(IDriverFactoryService driverManager
                         , IConfiguration configuration
                         , ILogger<SearchPage> logger
                            , IRpaRepository rpaRepository)
        {
            _driverManager = driverManager;
            _configuration = configuration;
            _logger = logger;
            _rpaRepository = rpaRepository;
        }

        public ResultProcess SearchPageAlura(string searchWord)
        {
            
            if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:busca"])) is not null)
            {
                //Insere texto na caixa de busca
                _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:busca"])).SendKeys(searchWord + Keys.Enter);

                Thread.Sleep(3000);

                if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:filtroBusca"])) is not null)
                {
                    _logger.LogInformation("Busca efetuada");
                    return new(true, "Busca", "Busca efetuada");
                }

                _logger.LogError("Falha ao executar a busca na caixa de pesquisa");
                return new(false, "Falha na busca", "Falha ao executar a busca na caixa de pesquisa");

            }

            _logger.LogError("Falha na busca");
            return new(false, "Falha da página", "Falha na busca");
        }

        public ResultProcess DetailsPageAlura()
        {

            try
            {
                //Filtra por cursos
                if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:filtroBusca"])) is not null)
                    _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:filtroBuscaCursos"])).Click();

                Thread.Sleep(3000);

                //Valida se retornou a busca
                if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:pagina"])) is not null)
                {

                    //Lista os resultados da busca
                    IList<IWebElement> elementList = _driver.FindElements(By.XPath(_configuration["Alura:SearchPage:resultadoBusca"])).ToList();

                    //Total de itens retornado na busca
                    var total = elementList.Count;
                   
                    List<DataExtracted> dataExtracted = new List<DataExtracted>();


                    for (int i = 0; i < total; i++)
                    {
                        _driver.WaitElement(By.XPath($"//*[@id='busca-resultados']/ul[1]/li[{i + 1}]/div/a/div[1]/div[1]/h4")).Click();

                        //Valida se carregou a página
                        if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:enroll"])) is not null)
                        {

                            try
                            {
                                dataExtracted.Add(new DataExtracted
                                {

                                    titulo = _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:titulo"])).Text,
                                    professor = _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:professor"])).Text,
                                    cargaHoraria = _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:cargaHoraria"])).Text,
                                    descricao = _driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:descricao"])).Text,
                                });

                                var record = _rpaRepository.InsertData(dataExtracted);

                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Falha ao acessar detalhes {ex.Message}");

                            }

                            //retorna para resultado da busca
                            _driver.Navigate().Back();

                        }

                    }

                }

                _logger.LogInformation("Extração concluída");
                return new(true, "Processado", "Extração concluída");

            }
            catch (Exception ex)
            {

                return new(false, "Falha da página", $"Falha ao acessar detalhes: {ex.Message}");

            }



        }

    }
}
