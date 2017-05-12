using MyLittleCMS.Services;
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
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}