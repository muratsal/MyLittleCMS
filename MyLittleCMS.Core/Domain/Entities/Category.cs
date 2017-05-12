using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Category:Entity
    {
        public Category()
        {

        }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
    }
}
