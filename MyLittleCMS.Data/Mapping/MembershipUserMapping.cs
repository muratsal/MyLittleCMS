using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using MyLittleCMS.Core.Domain.Entities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittleCMS.Data.Mapping
{
    public partial class MembershipUserMapping : EntityTypeConfiguration<MembershipUser>
    {
        public MembershipUserMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.UserName).IsRequired().HasMaxLength(50)
                                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_MembershipUser_UserName", 1) { IsUnique = true })); ;
            Property(x => x.PasswordHashed).IsRequired().HasMaxLength(150);
            Property(x => x.Email).IsRequired().HasMaxLength(75).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_MembershipUser_Email", 1) { IsUnique = true })); ; ;
            Property(x => x.IsApproved).IsRequired();
            Property(x => x.IsDeleted).IsRequired();

            this.HasRequired(x => x.MembershipUserRole)
                 .WithMany(x => x.MembershipUsers)
                 .HasForeignKey(x=>x.MembershipUserRoleId)
                 .WillCascadeOnDelete(false);
            Property(x => x.MembershipUserRoleId).IsRequired();
        }
    }

}
