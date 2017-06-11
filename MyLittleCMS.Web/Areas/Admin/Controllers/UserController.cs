using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLittleCMS.Web.Areas.Admin.Core;
using MyLittleCMS.Web.Areas.Admin.Model;
using MyLittleCMS.Services;
using MyLittleCMS.Core.General;
using MyLittleCMS.Core.Domain.Entities;
using MyLittleCMS.Core.Constants;
using X.PagedList;
using X.PagedList.Mvc;

namespace MyLittleCMS.Web.Areas.Admin.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IMembershipService _membershipService;
        public UserController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }
        // GET: Admin/User
        [AdminAuthorize(ACLKey = "User.View")]
        public ActionResult Index(int? pageIndex, int? pageSize,string search)
        {
            if (pageIndex == null) pageIndex = 1;
            if (pageSize == null) pageSize = AppConstants.Instance.DefaultPageSize;
            X.PagedList.IPagedList<MembershipUser>  users =   _membershipService.GetUsers(pageIndex.Value, pageSize.Value);
            MembershipUserListViewModel userListVM = new MembershipUserListViewModel();
            //userListVM.PageIndex = users.PageIndex;
            //userListVM.PageSize = users.PageSize;
            //userListVM.Search = search;
            //userListVM.MembershipList = users.ToList();
            return View(userListVM);
        }
        [AdminAuthorize(ACLKey = "User.Edit")]
        public ActionResult CreateUser()
        {
            MembershipEditViewModel userVM = new MembershipEditViewModel();
            userVM.Roles = _membershipService.GetAllRoles();
            return View(userVM);
        }
        [HttpPost]
        [AdminAuthorize(ACLKey = "User.Edit")]
        public ActionResult CreateUser(MembershipEditViewModel membershipEditViewModel)
        {
            if (ModelState.IsValid)
            {

                if (membershipEditViewModel.Password.Length > 5)
                {

                    if (membershipEditViewModel.Password != membershipEditViewModel.PasswordAgain)
                    {
                        ModelState.AddModelError("PasswordAgain", "passwords must equal!");
                        membershipEditViewModel.Roles = _membershipService.GetAllRoles();
                        return View(membershipEditViewModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "password length  least 6 digit!");
                    membershipEditViewModel.Roles = _membershipService.GetAllRoles();
                    return View(membershipEditViewModel);
                }

                _membershipService.AddUser(new MyLittleCMS.Core.Domain.Entities.MembershipUser
                {
                    Email = membershipEditViewModel.Email,
                    IsApproved = true,
                    IsDeleted = false,
                    PasswordHashed = Utilies.Helper.GetHashedString(membershipEditViewModel.Password),
                    UserName = membershipEditViewModel.UserName,
                    MembershipUserRoleId = membershipEditViewModel.UserRole.Value
                });
                return RedirectToAction("Index");
            }

            membershipEditViewModel.Roles = _membershipService.GetAllRoles();
            return View(membershipEditViewModel);
        }
        [AdminAuthorize(ACLKey = "User.Edit")]
        public ActionResult EditUser(int? id)
        {


            return View();
        }
    }
}