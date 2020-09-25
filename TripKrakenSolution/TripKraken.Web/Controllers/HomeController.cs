using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripKraken.DataSource.Model;
using TripKraken.Model.Main;
using TripKraken.Service;
using TripKraken.Service.ViewModel;

namespace TripKraken.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new CountryInfoService();
            var model = new MainView();
            model.Countries = service.GetCountries();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? countryId= null,string countryName=null)
        {
            var service = new CountryInfoService();

            try
            {
                var model = service.GetCountryInfoValues(countryId, countryName);
                Session["countryId"] = model.Country.Id;
                Session["countryCode"] = model.Country.Code;
                return View("Details", model);
            }
            catch
            {
                return View("NoCountryInfo");
            }



        }

        public ActionResult BasicInfo()
        {
            if (Session["countryId"] != null)
            {
                var service = new CountryInfoService();
                var model = service.GetCountryInfoValues((int)Session["countryId"]);
                Session["countryId"] = model.Country.Id;
                Session["countryCode"] = model.Country.Code;
                return View("Details", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult CountryDetail(int countryId=0)
        {
            return View("Dashboard");
        }


        //public ActionResult CostOfLife()
        //{
        //    if(Session["countryId"] == null)
        //    {
        //        var service = new CountryInfoService();
        //        var model = new MainView();
        //        model.Countries = service.GetCountries();
        //        return View(model);
        //    }
        //    else
        //    {
        //        return CostOfLife((int)Session["countryId"] );
        //    }

        //}

        //[HttpPost]
        //public ActionResult CostOfLife(int? countryId = null, string countryName = null)
        //{
        //    var service = new CountryInfoService();
        //    try
        //    {
        //        var model = service.GetCountryInfoValues(countryId, countryName);
        //        Session["countryId"] = model.Country.Id;
        //        Session["countryCode"] = model.Country.Code;
        //        return View("DetailsCostOfLife", model);
        //    }
        //    catch
        //    {
        //        return View("NoCountryInfo");
        //    }

        //}

        public ActionResult CrimeRate()
        {
            if (Session["countryId"] == null)
            {
                var service = new CountryInfoService();
                var model = new MainView();
                model.Countries = service.GetCountries();
                return View(model);
            }
            else
            {
                return CrimeRate((int)Session["countryId"]);
            }
        }


        [HttpPost]
        public ActionResult CrimeRate(int? countryId = null, string countryName = null)
        {
            var service = new CountryInfoService();
            try
            {
                var model = service.GetCountryInfoValues(countryId, countryName);
                Session["countryId"] = model.Country.Id;
                Session["countryCode"] = model.Country.Code;
                return View("DetailsCrimeRate", model);
            }
            catch
            {
                return View("NoCountryInfo");
            }
        }

        public ActionResult HealthCare()
        {

            return View();
        }

        public ActionResult OtherInfo()
        {

            return View();
        }

        public ActionResult Search(string actionP, string controllerP)
        {
            var service = new CountryInfoService();
            var model = new SearchView();
            model.action = actionP;
            model.controller = controllerP;
            model.Countries = service.GetCountries();
            model.CountryId = (int?)Session["countryId"];
            return PartialView("_PartialSearch", model);
        }
    }
}