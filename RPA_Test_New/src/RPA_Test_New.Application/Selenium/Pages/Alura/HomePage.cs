﻿using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using RPA_Test_New.Application.Selenium.Extensions;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;
using RPA_Test_New.Infrastructure.Data.Repositories;

namespace RPA_Test_New.Application.Selenium.Pages.Alura
{
    public class HomePage
    {

        private IDriverFactoryService _driverManager { get; init; }
        private IWebDriver _driver => _driverManager.Instance;
        private IConfiguration _configuration { get; set; }
        private IRpaRepository _rpaRepository { get; set; }

        public HomePage(IDriverFactoryService driverManager
                        , IConfiguration configuration
                        , IRpaRepository rpaRepository)
        {
            _driverManager = driverManager;
            _configuration = configuration;
            _rpaRepository = rpaRepository; 
        }

        public ResultProcess HomePageAlura(string url)
        {

            //Inicializa a página
            _driver.Navigate().GoToUrl(url);
            _driver.WaitTime();

            if (_driver.WaitElement(By.XPath(_configuration["Alura:HomePage:txtSearch"])) is not null)
                return new(true, "Acesso à página", "Página carregada com sucesso");

            return new(false, "Falha da página", "Falha ao acesso a URL");

        }

        public ResultProcess Search(string searchWord)
        {

            //Insere texto na caixa de busca
            if (_driver.WaitElement(By.XPath(_configuration["Alura:HomePage:txtSearch"])) is not null)
            {
                _driver.WaitElement(By.XPath(_configuration["Alura:HomePage:txtSearch"])).SendKeys(searchWord + Keys.Enter);

                Thread.Sleep(5000);

                if (_driver.WaitElement(By.XPath(_configuration["Alura:HomePage:filter"])) is not null)
                    return new(true, "Busca", "Busca efetuada");

                return new(false, "Falha na busca", "Falha ao executar a busca na caixa de pesquisa");

            }

            return new(false, "Falha da página", "Falha ao acesso a URL");
        }

       
        public ResultProcess Details()
        {

            try
            {
                //Valida se retornou a busca
                bool flag = false;
                if (_driver.WaitElement(By.XPath(_configuration["Alura:HomePage:searchResult"])) is not null)
                {
                    //Lista os resultados da busca
                    IList<IWebElement> elementList = _driver.FindElements(By.XPath(_configuration["Alura:ResultPage:list"])).ToList();

                    //Total de itens retornado na busca
                    var total = elementList.Count;
                    total = total - 1;

                    List<DataExtracted> dataExtracted = new List<DataExtracted>();

                   
                    for (int i = 0; i < total; i++)
                    {
                        flag = false;
                        _driver.WaitElement(By.XPath($"//*[@id='busca-resultados']/ul/li[{i+1}]/a/div/h4")).Click();

                        //Valida se carregou a página
                        if (_driver.WaitElement(By.XPath(_configuration["Alura:SearchPage:Conclusao"])) is not null)
                        {
                            
                            dataExtracted.Add(new DataExtracted
                            {
                                
                                titulo = _driver.WaitElement(By.XPath(_configuration["Alura:ResultPage:titulo"])).Text,
                                professor = _driver.WaitElement(By.XPath(_configuration["Alura:ResultPage:professor"])).Text,
                                cargaHoraria = _driver.WaitElement(By.XPath(_configuration["Alura:ResultPage:cargaHoraria"])).Text,
                                descricao = _driver.WaitElement(By.XPath(_configuration["Alura:ResultPage:descricao"])).Text,
                            });

                            var record = _rpaRepository.InsertData(dataExtracted);
                            
                            //retorna para resultado da busca
                            _driver.Navigate().Back();

                            flag = true;
                        }

                        if (!flag)
                            return new(false, "Falha da página", "Falha ao acessar detalhes");

                    }

                    if (!flag)
                        return new(false, "Falha da página", "Falha ao acessar detalhes");

                }

                return new(true, "Processado", "Extração concluída");

            }
            catch (Exception ex)
            {

                return new(false, "Falha da página", $"Falha ao acessar detalhes: {ex.Message}");
               
            }
            

           
        }

    }
}
