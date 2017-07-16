using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLittleCMS.Web.Areas.Admin.Core;
using MyLittleCMS.Web.Areas.Admin.Model;
using MyLittleCMS.Services;
using MyLittleCMS.Core.Domain.Entities;
using MyLittleCMS.Core.Constants;
using X.PagedList;
using X.PagedList.Mvc;
using MyLittleCMS.Core.Repository;
using CacheManager.Core;

namespace MyLittleCMS.Web.Areas.Admin.Controllers
{
    
    public class UserController : AdminBaseController
    {
        private readonly IMembershipService _membershipService;
        public UserController(IMembershipService membershipService,
            ICacheManager<object> cmsCache, IUnitOfWork unitOfWork) :base(cmsCache,unitOfWork)
        {
            _membershipService = membershipService;
        }
        // GET: Admin/User
        [AdminAuthorize(ACLKey = "User.View")]
        public ActionResult Index(int? pageIndex, int? pageSize,string search,string orderby)
        {
            if (pageIndex == null) pageIndex = 1;
            if (pageSize == null) pageSize = AppConstants.Instance.DefaultPageSize;

            
            X.PagedList.IPagedList<MembershipUser>  users =   _membershipService.GetUsers(pageIndex.Value, pageSize.Value,search, orderby);
            MembershipUserListViewModel userListVM = new MembershipUserListViewModel();
            userListVM.Search = search;
            userListVM.OrderBy = orderby;
            userListVM.MembershipList = users;
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

                if (membershipEditViewModel.Password != membershipEditViewModel.PasswordAgain)
                {
                    ModelState.AddModelError("PasswordAgain", "passwords must equal!");
                    membershipEditViewModel.Roles = _membershipService.GetAllRoles();
                    return View(membershipEditViewModel);
                }
                if (_membershipService.GetUserByName(membershipEditViewModel.UserName) != null)
                {
                    ModelState.AddModelError(string.Empty, "Username already exists.");
                    return View(membershipEditViewModel);
                }
                if (_membershipService.GetUserByEmail(membershipEditViewModel.Email) != null)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists.");
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
            if (id.HasValue)
            {
                MembershipUser editUser = _membershipService.GetUser(id.Value);
                MembershipUpdateViewModel userEditVM = new MembershipUpdateViewModel
                {
                    Id = editUser.Id,
                    IsApproved = editUser.IsApproved,
                    Email = editUser.Email,
                    UserName = editUser.UserName
                };
                return View(userEditVM);
            }
            return View();
        }

        [HttpPost]
        [AdminAuthorize(ACLKey = "User.Edit")]
        public ActionResult EditUser(int? id,MembershipUpdateViewModel userEdit)
        {
            if (id.HasValue)
            {
                MembershipUser updateUser = _membershipService.GetUser(userEdit.Id.Value);
                updateUser.IsApproved = userEdit.IsApproved;
               
                if (updateUser.UserName != userEdit.UserName)
                {
                    if(_membershipService.GetUserByName(userEdit.UserName)!=null)
                    {
                        ModelState.AddModelError(string.Empty, "Username already exists.");
                        return View(userEdit);
                    }
                    else
                    {
                        updateUser.UserName = userEdit.UserName;
                    }
                }
                if (updateUser.Email != userEdit.Email)
                {
                    if (_membershipService.GetUserByEmail(userEdit.Email) != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email already exists.");
                        return View(userEdit);
                    }
                    else
                    {
                        updateUser.Email = userEdit.Email;
                    }
                }
                UnitOfWork.SaveChanges();
                TempData["Success"] = "User updated successfully!";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [AdminAuthorize(ACLKey = "User.Edit")]
        public ActionResult DeleteUser(int? id)
        {
            if (id.HasValue)
            {
                MembershipUser deleteUser = _membershipService.GetUser(id.Value);
                _membershipService.DeleteUser(deleteUser);
                UnitOfWork.SaveChanges();

                TempData["Success"] = "User removed successfully!";
            }
            return RedirectToAction("Index");
        }

  
        [AdminAuthorize(ACLKey = "User.Edit")]
        [ActionName("DeleteUser")]
        public ActionResult DeleteUserById(int? id)
        {
            if (id.HasValue)
            {
                MembershipUser editUser = _membershipService.GetUser(id.Value);
                MembershipUpdateViewModel userEditVM = new MembershipUpdateViewModel
                {
                    Id = editUser.Id,
                    IsApproved = editUser.IsApproved,
                    Email = editUser.Email,
                    UserName = editUser.UserName
                };
                return View(userEditVM);
            }
            return View();
        }
    }
}