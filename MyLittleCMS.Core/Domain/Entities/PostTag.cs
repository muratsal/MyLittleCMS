using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
 

    public partial class PostTag : Entity
    {
        public PostTag()
        {

        }
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Post Post { get; set; }

    }
}
