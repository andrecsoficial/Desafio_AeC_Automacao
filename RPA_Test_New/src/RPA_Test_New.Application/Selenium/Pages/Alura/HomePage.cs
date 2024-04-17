using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RPA_Test_New.Application.Selenium.Extensions;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Application.Selenium.Pages.Alura
{
    public class HomePage
    {

        private IDriverFactoryService _driverManager { get; init; }
        private IWebDriver _driver => _driverManager.Instance;
        private IConfiguration _configuration { get; set; }

        public HomePage(IDriverFactoryService driverManager
                        , IConfiguration configuration)
        {
            _driverManager = driverManager;
            _configuration = configuration;
        }

        public ResultProcess HomePageAlura(string url)
        {

            try
            {
                _driver.Navigate().GoToUrl(url);
                _driver.WaitTime();

                if (_driver.WaitElement(By.XPath("//*[@id='header-barraBusca-form-campoBusca']")) is not null)
                    return new(true, "Acesso à página", "Página carregada com sucesso");

                return new(false, "Falha da página", "Falha ao acesso a URL");

            }
            catch (Exception ex)
            {
               
                return new(false, "Erro", ex.StackTrace);
                
            }
            
        }

    }
}
