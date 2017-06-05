using MyLittleCMS.Core.Domain.Entities;
using MyLittleCMS.Data.Repositories;
using MyLittleCMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleCMS.Core.Repository;
using MyLittleCMS.Core.Utilies;
using System.Data.Entity;
using MyLittleCMS.Core.General;

namespace MyLittleCMS.Services
{
    public partial interface IMembershipService
    {
        MembershipUser GetUser(int Id);
        IPagedList<MembershipUser> GetUsers(int pageIndex, int pageSize);
        MembershipUser AddUser(MembershipUser user);
        void DeleteUser(MembershipUser user);
        UserValidationResult IsUserValid(string userName, string password);
        List<string> GetUserRolePermissions(string userName);
        List<MembershipUserRole> GetAllRoles();

    }
    public enum UserValidationResult
    {
        UserNotFound,
        PasswordNotValid,
        UserNotApproved,
        UserValid,

    }
    public partial class MembershipService : IMembershipService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MembershipUser> _membershipUserRepository;
        private readonly IRepository<MembershipUserRole> _membershipUserRoleRepository;
        public MembershipService(IUnitOfWork unitOfWork, IRepository<MembershipUser> membershipUserRepository, IRepository<MembershipUserRole> membershipUserRoleRepository)
        {

            _unitOfWork = unitOfWork;
            _membershipUserRepository = membershipUserRepository;
            _membershipUserRoleRepository = membershipUserRoleRepository;
        }
        public IPagedList<MembershipUser> GetUsers(int pageIndex, int pageSize)
        {
            int totalCount = _membershipUserRepository.Where(x => x.IsDeleted == false).Count();
            IList<MembershipUser> membershipUsers = _membershipUserRepository.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<MembershipUser>(membershipUsers, pageIndex, pageSize, totalCount);
        }
        public MembershipUser AddUser(MembershipUser user)
        {
            user = _membershipUserRepository.Add(user);
            _unitOfWork.SaveChanges();
            return user;
        }


        public void DeleteUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public MembershipUser GetUser(int Id)
        {
            return _membershipUserRepository.Get(Id);
        }

        public UserValidationResult IsUserValid(string userName, string password)
        {
            MembershipUser user = _membershipUserRepository.Where(x => x.UserName == userName).SingleOrDefault();
            if (user != null)
            {

                if (user.IsApproved == false)
                {
                    return UserValidationResult.UserNotApproved;
                }

                if (user.PasswordHashed != Helper.GetHashedString(password))
                {
                    return UserValidationResult.PasswordNotValid;
                }
                else
                {
                    return UserValidationResult.UserValid;
                }
            }
            else
            {
                return UserValidationResult.UserNotFound;
            }

        }
        public List<string> GetUserRolePermissions(string userName)
        {
            MembershipUser userWithPermissions = _membershipUserRepository.Table().
                Include(x => x.MembershipUserRole.RolePermissions.Select(y => y.Permission)).
                Where(x => x.UserName == userName).SingleOrDefault();
            List<string> rolePermissions = new List<string>();
            if (userWithPermissions != null)
            {
                foreach (RolePermission rolePermission in userWithPermissions.MembershipUserRole.RolePermissions)
                {
                    rolePermissions.Add(rolePermission.Permission.PermissionName);
                }
            }

            return rolePermissions;
        }
        public List<MembershipUserRole> GetAllRoles()
        {
            List<MembershipUserRole> allRoles = _membershipUserRoleRepository.Get().ToList();
            return allRoles;
        }
    }
}
