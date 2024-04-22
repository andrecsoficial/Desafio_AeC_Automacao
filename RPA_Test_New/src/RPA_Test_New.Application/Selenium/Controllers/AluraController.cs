
using RPA_Test_New.Application.Selenium.Pages.Alura;
using RPA_Test_New.Domain.Entities;


namespace RPA_Test_New.Application.Selenium.Controllers
{
    public class AluraController
    {

        private HomePage _homePage { get; set; }
        private LoginPage _loginPage { get; set; }
        private SearchPage _searchPage { get; set; }

        public AluraController(HomePage homePage
                                ,LoginPage loginPage
                                ,SearchPage search)
        { 
            _homePage = homePage;
            _loginPage = loginPage;
            _searchPage = search;
        }

        public string Home(string url)
        {
            var result = _homePage.HomePageAlura(url);
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

        public string Login(AluraCredential aluraCredential)
        {
            var result = _loginPage.LoginPageAlura(aluraCredential);
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

        public string Search(string searchWord)
        {
            var result = _searchPage.SearchPageAlura(searchWord);
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

        public string Details()
        {
            var result =  _searchPage.DetailsPageAlura();
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

       
    }
}
