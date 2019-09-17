using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripKraken.DataSource.Model;
using TripKraken.Service;
using TripKraken.Service.ViewModel;

namespace TripKraken.Web.Controllers
{
    public class HomeController : Controller
    {
        
        [Authorize]
        public ActionResult Index()
        {
            var service = new CountryInfoService();
            var model = new MainView();
            model.Countries = service.GetCountries();
            model.CountryId = (int?)Session["countryId"] ?? 0;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int countryId)
        {
            Session["countryId"] = countryId;
            var service = new CountryInfoService();
            var model = service.GetCountryInfoValues(countryId);
            
            //model.Countries = service.GetCountries();
            //model.CountryId = (int?)Session["countryId"] ?? 0;
            return View("Details",model);
        }

        public ActionResult CountryDetail(int countryId=0)
        {
            return View("Dashboard");
        }

        public ActionResult CostOfLife()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult CrimeRate()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult HealthCare()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult OtherInfo()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}