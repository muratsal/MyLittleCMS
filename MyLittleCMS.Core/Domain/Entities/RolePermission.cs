using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class RolePermission
    {

        public RolePermission()
        {

        }
        public int Id { get; set; }
        public int MembershipUserRoleId { get; set; }
        public int PermissionId { get; set; }
        public virtual MembershipUserRole MembershipUserRole { get; set; }
        public virtual Permission Permission { get; set; }
    }
}