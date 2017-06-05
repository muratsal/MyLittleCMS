using CacheManager.Core;
using MyLittleCMS.Services;
using MyLittleCMS.Web.Areas.Admin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyLittleCMS.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {

        private readonly IMembershipService _membershipService;
        
        public AccountController(ICacheManager<object> cmsCache,MembershipService membershipService)
            :base(cmsCache)
        {
            _membershipService = membershipService;
            _cmsCache = cmsCache;

        }
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password, bool? rememberMe,string returnURL)
        {
            UserValidationResult vldRslt = _membershipService.IsUserValid(userName, password);

            if (string.IsNullOrEmpty(returnURL))
            {
                ViewBag.returnURL = "";
            }
            else ViewBag.returnURL = returnURL;
            if (vldRslt == UserValidationResult.UserNotFound || vldRslt == UserValidationResult.UserNotApproved)
            {
                ViewBag.Message = "Kullanıcı kaydı bulunamadı!";
            }
            else if( vldRslt == UserValidationResult.PasswordNotValid)
            {
                ViewBag.Message = "Kullanıcı bilgisi hatası!";
            }
            else
            {
                if (rememberMe == null)
                    rememberMe = false;
                FormsAuthentication.SetAuthCookie(userName, rememberMe.Value);
                if (!string.IsNullOrEmpty(returnURL))
                    return Redirect(returnURL);
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}