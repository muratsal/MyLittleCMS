using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class PostCategoryMapping:EntityTypeConfiguration<PostCategory>
    {
        public PostCategoryMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.CategoryId).IsRequired();
            Property(x => x.PostId).IsRequired();

            this.HasRequired(x => x.Post)
                .WithMany(x => x.PostCategories)
                .HasForeignKey(x => x.PostId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Category)
                .WithMany(x => x.PostCategories)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
