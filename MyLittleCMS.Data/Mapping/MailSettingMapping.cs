using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class MailSettingMapping : EntityTypeConfiguration<MailSetting>
    {
        public MailSettingMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
            Property(x => x.Email).IsRequired().HasMaxLength(150);
            Property(x => x.EnableSsl).IsRequired();
            Property(x => x.Host).HasMaxLength(150);
            Property(x => x.Password).IsRequired().HasMaxLength(50);
            Property(x => x.Port).IsOptional();
            Property(x => x.UseDefaultCredentials).IsOptional();
        }
    }
}
