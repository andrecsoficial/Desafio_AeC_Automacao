using RPA_Test_New.Application.Selenium.Pages.Alura;

namespace RPA_Test_New.Application.Selenium.Controllers
{
    public class AluraController
    {

        private HomePage _homePage { get; set; }
        

        public AluraController(HomePage homePage)
        { 
            _homePage = homePage;
           
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

        public string Search(string searchWord)
        {
            var result = _homePage.Search(searchWord);
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

        public string Details()
        {
            var result =  _homePage.Details();
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }

       
    }
}
