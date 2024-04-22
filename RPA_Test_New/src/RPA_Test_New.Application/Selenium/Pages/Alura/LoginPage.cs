using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using RPA_Test_New.Application.Selenium.Extensions;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;


namespace RPA_Test_New.Application.Selenium.Pages.Alura
{
    public class LoginPage
    {
        private IDriverFactoryService _driverManager { get; init; }
        private IWebDriver _driver => _driverManager.Instance;
        private IConfiguration _configuration { get; set; }
        private ILogger<LoginPage> _logger { get; init; }

        public LoginPage(IDriverFactoryService driverManager
                         ,IConfiguration configuration
                         ,ILogger<LoginPage> logger) 
        {
            _driverManager = driverManager;
            _configuration = configuration;
            _logger = logger;
        }

        public ResultProcess LoginPageAlura(AluraCredential aluraCredential)
        {

            //valida página do login
            _logger.LogInformation("Efetua o login");
            if (_driver.WaitElement(By.XPath("//*[@id='form-default']/button")) is not null)
            {
                _logger.LogInformation("Insere credenciais");
                _driver.WaitElement(By.XPath("//*[@id='login-email']")).SendKeys(aluraCredential.User);
                Thread.Sleep(2000);
                _driver.WaitElement(By.XPath("//*[@id='password']")).SendKeys(aluraCredential.Password + Keys.Enter);
                if (_driver.WaitElement(By.XPath("/html/body/header/div[2]/div[2]/div[1]/div/div/button")) is not null)
                    return new(true, "Login página", "Login realizado com sucesso");

            }

            _logger.LogError("Falha ao realizar login");
            return new(false, "Falha no login", "Falha ao realizar login");
        }
    }
}
