using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Post : Entity
    {

        public Post()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool AllowComments { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public bool IsPublished { get; set; }
      
        public int PostById { get; set; }

        public   virtual  MembershipUser PostBy { get; set; }
        public virtual ICollection<PostCategory> PostCategories {get;set;}
        public virtual ICollection<PostTag> PostTags { get; set; }

    }
}
