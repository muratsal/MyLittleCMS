using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public partial class MembershipUser : Entity
    {
        public MembershipUser()
        {

        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }
        public string Email { get; set; }
        public int  MembershipUserRoleId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }

        public virtual MembershipUserRole MembershipUserRole { get; set; }
        public virtual ICollection<Post> UserPosts { get; set; }
    }
}
