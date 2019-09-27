using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripKraken.Service;
using TripKraken.Service.ViewModel;
using AutoMapper;
using TripKraken.Model.Main;
using Microsoft.AspNet.Identity;

namespace TripKraken.Web.Controllers
{
    [Authorize]
    public class CostOfLifeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["countryId"] == null)
            {
                var service = new CountryInfoService();
                var model = new MainView();
                model.Countries = service.GetCountries();
                return View("CostOfLife",model);
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
                return View("DetailsCostOfLife", model);
            }
            catch
            {
                return View("~/Views/Home/NoCountryInfo.cshtml");
            }

        }

        public ActionResult Create(int? countryInfoId, string type)
        {
            ViewBag.PriceType = type;
            var userService = new UserService();
            var service = new CountryInfoService();

            var model = new CreateCostOfLivingInfoView();
            model.CountryInfoID = (int)countryInfoId;
            model.PriceForTypeID = service.GetPriceForType(type).Id;
            model.ApplicationUserId = userService.GetUser(User.Identity.Name).Id;
            return View("CreateCostOfLife", model);
        }

        [HttpPost]
        public ActionResult Create(CreateCostOfLivingInfoView createModel)
        {
            var service = new CountryInfoService();
            var model = AutoMapper.Mapper.Map<CreateCostOfLivingInfoView, CostOfLivingInfo>(createModel);      
            Session["CreateTxt"] = service.CreateCostOfLiving(model);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var userService = new UserService();
            var service = new CountryInfoService();

            var result = service.GetCostOfLivingInfo(id);
            var model = new EditCostOfLivingInfoView()
            {
                Value = result.Value,
                Id = id
            };
            
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditCostOfLivingInfoView editModel)
        {
            var service = new CountryInfoService();
            service.EditCostOfLiving(editModel.Id,editModel.Value);
            Session["CreateTxt"] = "Successfully edited value.";
            return RedirectToAction("UserCostOfValueRate");
        }


        public ActionResult UserCostOfValueRate(string username = null, int? countryId = null, string countryName = null)
        {
            var service = new CountryInfoService();
            var userService = new UserService();
            var model = new UserValueForCostView();
            model.Countries = service.GetCountries();
            if (username != null)
            {
                Session["selectedUser"] = username;
                model.CostList = userService.GetUserCostOfLiving(username, countryId);
            }
            else
                model.CostList = userService.GetUserCostOfLiving(User.Identity.Name, countryId);

            return View("UserCostOfValueRate", model);
        }

        [HttpPost]
        public ActionResult UserCostOfValueRateP(int? countryId = null, string countryName = null)
        {
            var userSelected = Session["selectedUser"] != null ? Session["selectedUser"].ToString() : null;
            try
            {
                var service = new CountryInfoService();
                var model = service.GetCountryInfoValues(countryId, countryName);
                Session["countryId"] = model.Country.Id;
                Session["countryCode"] = model.Country.Code;
            }
            catch
            {
                return View("NoCountryInfo");
            }
            return RedirectToAction("UserCostOfValueRate",new { username = userSelected,countryId = countryId});
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = new CountryInfoService();
            service.DeleteCostOfLiving(id);
            Session["DeleteTxt"] = "Successfully deleted value.";
            return RedirectToAction("UserCostOfValueRate");
        }
    }
}