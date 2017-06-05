using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Text;

namespace MyLittleCMS.Data.Mapping
{

    public partial class RolePermissionMapping : EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.MembershipUserRoleId).IsRequired();
            Property(x => x.PermissionId).IsRequired();

            this.HasRequired(x => x.MembershipUserRole)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.MembershipUserRoleId)
                .WillCascadeOnDelete(false);
            this.HasRequired(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId)
                .WillCascadeOnDelete(false);

        }
    }
}