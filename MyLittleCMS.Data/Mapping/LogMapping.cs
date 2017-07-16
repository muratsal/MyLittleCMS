using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class LogMapping : EntityTypeConfiguration<Log>
    {
        public LogMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.ShortMessage).HasMaxLength(250);
            Property(x => x.IpAddress).HasMaxLength(20);
            Property(x => x.PageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ReferrerUrl).IsOptional().HasMaxLength(300);
            Property(x => x.CreatedOnUtc).IsRequired();

            this.HasOptional(x => x.MembershipUser)
               .WithMany(x => x.UseLogs)
               .HasForeignKey(x => x.UserId)
               .WillCascadeOnDelete(false);
        }

    }
}
