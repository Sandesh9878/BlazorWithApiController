using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class SMSLog
    {
        public int SMSLogId { get; set; }
        public int SMSId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual SMSs SMS { get; set; }
    }
}
