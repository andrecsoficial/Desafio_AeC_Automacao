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
        private IRpaRepository _rpaRepository { get; set; }  

        public DetailsPage(IDriverFactoryService driverManager
                            ,IConfiguration configuration
                            ,IRpaRepository rpaRepository)
        {
            _driverManager = driverManager;
            _configuration = configuration;
            _rpaRepository = rpaRepository;
        }

        public  ResultProcess ExtractDetails()
        {
            /**
             * Executa coleta dos dados de:
             * Título
             * Professor
             * Carga horária
             * Descrição
             * */

            DataExtracted dataExtracted = new DataExtracted();

            if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:Conclusao"])) is not null)
            {
                dataExtracted.titulo = _driver.WaitElement(By.XPath("/html/body/section[1]/div/div[1]/p[2]")).Text;
                dataExtracted.professor = _driver.WaitElement(By.XPath("//*[@id='section-icon']/div[1]/section/div/div/div/h3")).Text;
                dataExtracted.cargaHoraria = _driver.WaitElement(By.XPath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]")).Text;
                dataExtracted.descricao = _driver.WaitElement(By.XPath("//*[@id='section-icon']/div[1]/div/div/p")).Text;

                var record = _rpaRepository.InsertData(dataExtracted);

                return new(true, "Processado", "Extração detalhes da página");

            }

            return new(false, "Falha na extração", "Falha ao extrair detalhes da página");
        }

    }
}
