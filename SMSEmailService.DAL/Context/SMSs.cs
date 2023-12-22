using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class SMSs
    {
        public SMSs()
        {
            SMSLogs = new HashSet<SMSLog>();
        }

        public int SMSId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string SMSContent { get; set; }
        public string WebSiteUrl { get; set; }
        public string SMSType { get; set; }
        public string SMSTemplate { get; set; }
        public string Status { get; set; }
        public bool IsFailed { get; set; }
        public string FailedMessage { get; set; }
        public string SubscriptionRequestId { get; set; }
        public string SubscriptionResponse { get; set; }
        public string SMSRequestId { get; set; }
        public string SendSMSResponse { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual ICollection<SMSLog> SMSLogs { get; set; }
    }
}
