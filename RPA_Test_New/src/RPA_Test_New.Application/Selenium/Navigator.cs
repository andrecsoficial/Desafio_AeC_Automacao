﻿using Microsoft.Extensions.Configuration;
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

        public async Task<ResultProcess> NavigationAlura(string url, string searchWord, AluraCredential credential)
        {
            _logger.LogInformation("Acessando URL");
            if (_aluraController.Home(url) is null)
            {
                _logger.LogError("Falha ao acessar URL");
                return new(false, "Erro", "Falha ao acessar URL");
            }

            _logger.LogInformation("Realiza o Logon");
            if (_aluraController.Login(credential) is null)
            {
                _logger.LogError("Falha ao realizar o login");
                return new(false, "Erro", "Falha ao realizar o login");
            }

            _logger.LogInformation("Efetua pesquisa");
            if (_aluraController.Search(searchWord) is null)
            {
                _logger.LogError("Falha ao efetuar pesquisa");
                return new(false, "Erro", "Falha ao efetuar pesquisa");
            }
               
            _logger.LogInformation("Acessa itens pesquisa");
            if (_aluraController.Details() is null)
            {
                _logger.LogError("Falha ao exibir detalhes");
                return new(false, "Erro", "Falha ao exibir detalhes");
            }

            _logger.LogInformation("Navegação concluída");
            return new(true, "Concluído", "Navegação concluída");
        }

    }
}
