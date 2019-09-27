using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripKraken.Service;
using TripKraken.Service.ViewModel;
using TripKraken.Web.Models;

namespace TripKraken.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Users()
        {
            if (User.IsInRole("Admin"))
            {

            }
            var userService = new UserService();
            var model = new UserView();
            model.Roles = userService.GetRoles();
            model.UserList = userService.GetUserList();
            return View("Users",model);
        }

        [HttpPost]
        public ActionResult Delete(string userId)
        {
            var userService = new UserService();
            userService.DeleteUser(userId);
            Session["DeleteTxt"] = "Successfully deleted user.";
            return Users();
        }

        public ActionResult Edit(string userId)
        {
            var userService = new UserService();
            var model = new EditUserView();
            model.Roles = userService.GetRoles();
            var user = userService.GetUserById(userId);
            model.UserId = userId;
            model.RoleId = user.Roles.Count > 0 ? user.Roles.First().RoleId:model.Roles.SingleOrDefault(x => x.Name.ToLower() == "user").Id;
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserView model)
        {
            var userService = new UserService();
            //userService.AddRole(model.UserId, model.RoleId);
            userService.UserManager.AddToRoles(model.UserId, userService.GetRoleName(model.RoleId));
            var roler = userService.UserManager.GetRoles(model.UserId);
            Session["CreateTxt"] = "Successfully changed role.";
            return Users();
        }

        public ActionResult CrimeRate(string username = null, int? countryId = null, string countryName = null)
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
                model.CrimeRateList = userService.GetUserCrimeRateInfo(null, countryId);

            return View("CrimeRate", model);
        }

        [HttpPost]
        public ActionResult CrimeRate(int? countryId = null, string countryName = null)
        {
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
            return RedirectToAction("CrimeRate", new { countryId = countryId });
        }

        public ActionResult CostOfLiving(string username = null, int? countryId = null, string countryName = null)
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
                model.CostList = userService.GetUserCostOfLiving(null, countryId);

            return View("CostOfLiving", model);
        }

        [HttpPost]
        public ActionResult CostOfLiving(int? countryId = null, string countryName = null)
        {
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

            return RedirectToAction("CostOfLiving",new {countryId = countryId});
        }

        [HttpPost]
        public ActionResult DeleteCostOfLiving(int id)
        {
            var service = new CountryInfoService();
            service.DeleteCostOfLiving(id);
            Session["DeleteTxt"] = "Successfully deleted value.";
            return RedirectToAction("CostOfLiving");
        }

        [HttpPost]
        public ActionResult DeleteCrimeRate(int id)
        {
            var service = new CountryInfoService();
            service.DeleteCrimeRate(id);
            Session["DeleteTxt"] = "Successfully deleted value.";
            return RedirectToAction("CrimeRate");
        }

        public ActionResult EditCrimeRate(int id)
        {
            var userService = new UserService();
            var service = new CountryInfoService();

            var result = service.GetCrimeRateInfo(id);
            var model = new EditCrimeRateInfo()
            {
                Value = result.Value,
                Id = id
            };

            return View("EditCrimeRate", model);
        }

        [HttpPost]
        public ActionResult EditCrimeRate(EditCrimeRateInfo editModel)
        {
            var service = new CountryInfoService();
            service.EditCrimeRate(editModel.Id, editModel.Value);
            Session["CreateTxt"] = "Successfully edited value.";
            return RedirectToAction("CrimeRate");
        }

        public ActionResult EditCostOfLiving(int id)
        {
            var userService = new UserService();
            var service = new CountryInfoService();

            var result = service.GetCostOfLivingInfo(id);
            var model = new EditCostOfLivingInfoView()
            {
                Value = result.Value,
                Id = id
            };

            return View("EditCostOfLiving", model);
        }

        [HttpPost]
        public ActionResult EditCostOfLiving(EditCostOfLivingInfoView editModel)
        {
            var service = new CountryInfoService();
            service.EditCostOfLiving(editModel.Id, editModel.Value);
            Session["CreateTxt"] = "Successfully edited value.";
            return RedirectToAction("CostOfLiving");
        }

    }
}