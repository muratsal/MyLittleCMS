using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    class MembershipUserRoleMapping : EntityTypeConfiguration<MembershipUserRole>
    {
        public MembershipUserRoleMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.RoleName).IsRequired().HasMaxLength(70);
        }
    }
}
