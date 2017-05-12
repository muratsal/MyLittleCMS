using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class PostMapping : EntityTypeConfiguration<Post>
    {
        public PostMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.AllowComments).IsRequired();
            Property(x => x.Content).IsRequired();
            Property(x => x.CreatedOnUtc).IsRequired();
            Property(x => x.EndDateUtc).IsOptional();
            Property(x => x.IsPublished).IsRequired();
            Property(x => x.MetaDescription).HasMaxLength(500);
            Property(x => x.MetaKeywords).HasMaxLength(400);
            Property(x => x.MetaTitle).HasMaxLength(300);
            Property(x => x.PostById).IsRequired();
            Property(x => x.StartDateUtc).IsOptional();
            Property(x => x.MetaTitle).HasMaxLength(300);
            Property(x => x.Title).HasMaxLength(500);

            this.HasRequired(x => x.PostBy)
                .WithMany(x => x.UserPosts)
                .HasForeignKey(x => x.PostById)
                .WillCascadeOnDelete(false);
        }
    }
}
