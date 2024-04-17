using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace RPA_Test_New.Application.Selenium.Extensions
{
    public static class SeleniumExtensions
    {
        //Aguardar 30 segundos para o driver chegar na URL
        public static WebDriverWait WaitTime(this IWebDriver driver, int seconds = 30)
       => new(driver, System.TimeSpan.FromSeconds(seconds));

        //Aguardar elemento
        public static IWebElement WaitElement(this IWebDriver driver, By by, int seconds = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(seconds));
                var element = wait.Until(d => d.FindElement(by));
                return element;
            }
            catch
            {
                return null;
            }
        }

    }
}
