using MyLittleCMS.Core.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyLittleCMS.Web.Areas.Admin.Model
{
    public class MembershipEditViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Password Again")]
        public string PasswordAgain { get; set; }
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public IList<MembershipUserRole> Roles { get; set; }
        public int? UserRole { get; set; }
    }
    public class MembershipUpdateViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public IList<MembershipUserRole> Roles { get; set; }
        public int? UserRole { get; set; }
    }
    public class MembershipUserListViewModel
    {
        public X.PagedList.IPagedList<MembershipUser> MembershipList { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
    }

}