using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class PageMapping : EntityTypeConfiguration<Page>
    {
        public PageMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).IsRequired().HasMaxLength(200);
            Property(x => x.Content).IsRequired();
            Property(x => x.MetaTitle).IsRequired().HasMaxLength(400);
            Property(x => x.MetaKeywords).IsRequired().HasMaxLength(400);
            Property(x => x.MetaDescription).IsRequired().HasMaxLength(400);
            Property(x => x.AllowComments).IsRequired();
            Property(x => x.Tags).IsRequired().HasMaxLength(400);
        }
    }
}
