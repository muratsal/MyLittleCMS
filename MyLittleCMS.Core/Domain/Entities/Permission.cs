
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Permission
    {

        public Permission()
        {

        }
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}