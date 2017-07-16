using CacheManager.Core;
using MyLittleCMS.Core.Repository;
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
        protected ICacheManager<object> CmsCache { get; set; }
        protected readonly IUnitOfWork UnitOfWork;

        public AdminBaseController(ICacheManager<object> cmsCache,IUnitOfWork unitOfWork)
        {
            CmsCache = cmsCache;
            UnitOfWork = unitOfWork;
        }
        public string  CurrentUserName
        {
           get { return System.Web.HttpContext.Current.User.Identity.Name; }
           set { }
        }



        
    }
}