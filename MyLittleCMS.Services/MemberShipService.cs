using MyLittleCMS.Core.Domain.Entities;
using MyLittleCMS.Data.Repositories;
using MyLittleCMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleCMS.Core.Repository;

namespace MyLittleCMS.Services
{
    public partial interface IMembershipService
    {
        MembershipUser GetUser(int Id);
        MembershipUser AddUser(MembershipUser user);
        void DeleteUser(MembershipUser user);

    }
   public partial  class MembershipService : IMembershipService
    {
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MembershipUser> _membershipUserRepository;
        public MembershipService(IUnitOfWork unitOfWork, IRepository<MembershipUser> membershipUserRepository)
        {

            _unitOfWork = unitOfWork;
            _membershipUserRepository = membershipUserRepository;
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
           return   _membershipUserRepository.Get(Id);
        }
    }
}
