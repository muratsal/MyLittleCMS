using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Comment:Entity
    {
        public Comment()
        {

        }
        public int Id { get; set; }
        public string CommentText { get; set; }
        public bool IsPost { get; set; }
        public DateTime CommentUtc { get; set; }
        public int PostId { get; set; }
        public bool? IsApproved { get; set; }
        public int CreatedById { get; set; }
        public bool IsDeleted { get; set; }

        public virtual MembershipUser CreatedBy { get; set; }
    }
}
