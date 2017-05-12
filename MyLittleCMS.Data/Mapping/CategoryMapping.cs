using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.CategoryName).IsRequired().HasMaxLength(250);
            Property(x => x.IsActive).IsRequired();
            Property(x => x.IsDeleted).IsRequired();
            Property(x => x.ParentId).IsOptional();
          
        }
    }
}
