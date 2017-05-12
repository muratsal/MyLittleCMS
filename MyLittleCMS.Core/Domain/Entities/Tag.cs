using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Tag : Entity
    {
        public Tag()
        {
        }
        public int Id { get; set; }
        public string TagName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }

    }

}
