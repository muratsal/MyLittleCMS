using MyLittleCMS.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Mapping
{
    public partial class PostTagMapping : EntityTypeConfiguration<PostTag>
    {
        public PostTagMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            this.HasRequired(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId)
                .WillCascadeOnDelete(false);
            this.HasRequired(x => x.Tag)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.TagId)
                .WillCascadeOnDelete(false);

        }
    }
}
