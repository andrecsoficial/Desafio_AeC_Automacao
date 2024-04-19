﻿using RPA_Test_New.Application.Selenium.Pages.Alura;

namespace RPA_Test_New.Application.Selenium.Controllers
{
    public class AluraController
    {

        private HomePage _homePage { get; set; }
        private DetailsPage _detailsPage { get; set; }

        public AluraController(HomePage homePage
                                ,DetailsPage detailsPage)
        { 
            _homePage = homePage;
            _detailsPage = detailsPage;
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

        public string Extraction()
        {
            var result = _detailsPage.ExtractDetails();
            if (!result.sucess)
            {
                return null;
            }
            return result.obs.ToString();
        }
    }
}
