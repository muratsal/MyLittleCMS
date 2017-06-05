using CacheManager.Core;
using MyLittleCMS.Data.Context;
using MyLittleCMS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLittleCMS.Web.Areas.Admin.Core
{
    public class AdminBaseController : Controller
    {
        // GET: Admin/AdminBase
        protected ICacheManager<object> _cmsCache { get; set; }

        public AdminBaseController(ICacheManager<object> cmsCache)
        {
            _cmsCache = cmsCache;
            
        }
        public string  CurrentUserName
        {
           get { return System.Web.HttpContext.Current.User.Identity.Name; }
           set { }
        }



        
    }
}