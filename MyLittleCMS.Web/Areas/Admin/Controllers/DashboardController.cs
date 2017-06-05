using MyLittleCMS.Services;
using MyLittleCMS.Web.Areas.Admin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLittleCMS.Web.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IMembershipService _membershipService;
        public DashboardController(IMembershipService membershipService)
        {
           _membershipService = membershipService;
        }
        [AdminAuthorize(ACLKey="Dashboard.View")]
        public ActionResult Index()
        {
            return View();
        }
    }
}