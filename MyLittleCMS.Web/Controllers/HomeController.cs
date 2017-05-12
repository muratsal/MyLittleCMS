using MyLittleCMS.Core.Domain.Entities;
using MyLittleCMS.Services;
using MyLittleCMS.Web.Utilies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLittleCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMembershipService _membershipService;
        public HomeController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public ActionResult Index()
        {
            MembershipUser User = new MembershipUser();
            User.Email = "test@gmail.com";
            User.IsApproved = true;
            User.PasswordHashed = Helper.GetHashedString("Deneme123");
            User.UserName = "Muratsal";
            User.MembershipUserRoleId=1;
            _membershipService.AddUser(User);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            MembershipUser user =  _membershipService.GetUser(4);

            return View(user);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}