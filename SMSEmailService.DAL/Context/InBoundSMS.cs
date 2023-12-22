using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class InBoundSMS
    {
        public int InBoundSmsId { get; set; }
        public int CompanyId { get; set; }
        public string PushId { get; set; }
        public string InboundId { get; set; }
        public string SubscriptionId { get; set; }
        public string PhoneNumber { get; set; }
        public string Keywords { get; set; }
        public string Smscontent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public virtual Company Company { get; set; }
    }
}
