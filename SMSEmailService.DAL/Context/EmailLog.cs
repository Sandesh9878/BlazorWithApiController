using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class EmailLog
    {
        public int EmailLogId { get; set; }
        public int EmailId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Email Email { get; set; }
    }
}
