using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Core.Domain.Entities
{
    public enum LogType
    {
        SystemError = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Fatal = 5
    }
    public partial class Log : Entity
    {
        public int Id { get; set; }
        public int LogTypeId { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public LogType LogType
        {
            get
            {
                return (LogType)this.LogTypeId;
            }
            set
            {
                this.LogTypeId = (int)value;
            }
        }
        public virtual MembershipUser MembershipUser { get; set; }
       
    }

}
