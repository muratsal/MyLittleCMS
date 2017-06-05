using CacheManager.Core;
using Microsoft.Practices.Unity;
using MyLittleCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyLittleCMS.Web.Areas.Admin.Core
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _isAuthorized;
        public string ACLKey { get; set; }
        [Dependency]
        protected ICacheManager<object> cmsCache { get; set; }
        [Dependency]
        protected IMembershipService membershipService { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            _isAuthorized = base.AuthorizeCore(httpContext);
            return _isAuthorized;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            bool redirectToLogin = false;
            if (!_isAuthorized)
            {
                redirectToLogin = true;
            }
            else
            {
                var filterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(true).Where(a => a.GetType() == typeof(AdminAuthorizeAttribute));
                if (filterAttribute != null)
                {
                    foreach (AdminAuthorizeAttribute attr in filterAttribute)
                    {
                        ACLKey = attr.ACLKey;
                    }
                }
                List<string> userPermissions = cmsCache.Get<List<string>>("UserPermissions");
                if(userPermissions == null)
                {
                    userPermissions =   membershipService.GetUserRolePermissions(HttpContext.Current.User.Identity.Name);
                    
                }
                if(userPermissions.Where(x=>x == ACLKey).ToList().Any())
                {
                }
                else
                {
                    redirectToLogin = true;
                }
            }

            if(redirectToLogin)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "Admin", filterContext.RouteData.Values[ "Admin" ] },
                    { "controller", "Account" },
                    { "action", "Login" },
                    { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                   });
            }

            /* public string AllowFeature { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {

            var filterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(true)
                                    .Where(a => a.GetType() == 
                                   typeof(FeatureAuthenticationAttribute));
            if (filterAttribute != null)
            {
                foreach (FeatureAuthenticationAttribute attr in filterAttribute)
                {
                    AllowFeature = attr.AllowFeature;
                }
           List<Role> roles = 
           ((User)filterContext.HttpContext.Session["CurrentUser"]).Roles;
           bool allowed = SecurityHelper.IsAccessible(AllowFeature, roles);
             if (!allowed)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        */
        }
    }
}
