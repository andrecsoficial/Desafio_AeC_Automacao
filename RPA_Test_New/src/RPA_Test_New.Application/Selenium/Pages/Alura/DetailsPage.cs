

using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RPA_Test_New.Application.Selenium.Extensions;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;

namespace RPA_Test_New.Application.Selenium.Pages.Alura
{
    public class DetailsPage
    {
        private IDriverFactoryService _driverManager { get; init; }
        private IWebDriver _driver => _driverManager.Instance;
        private IConfiguration _configuration { get; set; }

        public DetailsPage(IDriverFactoryService driverManager
                            ,IConfiguration configuration)
        {
            _driverManager = driverManager;
            _configuration = configuration;
        }

        public ResultProcess ExtractDetails()
        {
            /**
             * Executa coleta dos dados de:
             * Título
             * Professor
             * Carga horária
             * Descrição
             * */

            if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:Conclusao"])) is not null)
            {
                //Título


            }

            return new(false, "Falha na extração", "Falha ao extrair detalhes da página");
        }

    }
}
