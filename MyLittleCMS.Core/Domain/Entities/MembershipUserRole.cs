using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class MembershipUserRole : Entity
    {
        public MembershipUserRole()
        {

        }
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsSytemRole { get; set; }
        public virtual ICollection<MembershipUser> MembershipUsers { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

    }
}