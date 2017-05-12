using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class Page:Entity
    {
        public Page()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool AllowComments { get; set; }
        public string Tags { get; set; }



    }
}
