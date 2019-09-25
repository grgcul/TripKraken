using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripKraken.Model.Main;
using TripKraken.Service;
using TripKraken.Service.ViewModel;

namespace TripKraken.Web.Controllers
{
    public class CrimeRateController : Controller
    {
        // GET: CrimeRate
        public ActionResult Index()
        {
            if (Session["countryId"] == null)
            {
                var service = new CountryInfoService();
                var model = new MainView();
                model.Countries = service.GetCountries();
                return View("CrimeRate", model);
            }
            else
            {
                return Index((int)Session["countryId"]);
            }

        }

        [HttpPost]
        public ActionResult Index(int? countryId = null, string countryName = null)
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
                return View("~/Views/Home/NoCountryInfo.cshtml");
            }

        }

        public ActionResult CreateCrimeRate(int? countryInfoId, string type)
        {
            ViewBag.PriceType = type;
            var userService = new UserService();
            var service = new CountryInfoService();

            var model = new CreateCrimeRateInfo();
            model.CountryInfoID = (int)countryInfoId;
            model.CrimeRateTypeID = service.GetCrimeRateType(type).Id;
            model.ApplicationUserId = userService.GetUser(User.Identity.Name).Id;
            return View("CreateCrimeRate", model);
        }

        [HttpPost]
        public ActionResult CreateCrimeRate(CreateCrimeRateInfo createModel)
        {
            var service = new CountryInfoService();
            var model = AutoMapper.Mapper.Map<CreateCrimeRateInfo, CrimeRateInfo>(createModel);
            service.CreateCrimeRate(model);

            Session["CreateTxt"] = "Successfully added value.";
            return Index();
        }

        public ActionResult UserCrimeRateInput(string username = null, int? countryId = null, string countryName = null)
        {
            var service = new CountryInfoService();
            var userService = new UserService();
            var model = new UserCrimeRateInputView();
            model.Countries = service.GetCountries();
            if (username != null)
            {
                Session["selectedUser"] = username;
                model.CrimeRateList = userService.GetUserCrimeRateInfo(username, countryId);
            }
            else
                model.CrimeRateList = userService.GetUserCrimeRateInfo(User.Identity.Name, countryId);

            return View("UserCrimeRateInput", model);
        }

        [HttpPost]
        public ActionResult UserCrimeRateInputP(int? countryId = null, string countryName = null)
        {
            var userSelected = Session["selectedUser"] != null ? Session["selectedUser"].ToString() : null;
            Session["countryId"] = countryId;
            return UserCrimeRateInput(userSelected, countryId);
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = new CountryInfoService();
            service.DeleteCrimeRate(id);
            Session["DeleteTxt"] = "Successfully deleted value.";
            return UserCrimeRateInput();
        }
    }
}