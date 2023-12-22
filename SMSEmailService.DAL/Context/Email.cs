using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class Email
    {
        public Email()
        {
            EmailLogs = new HashSet<EmailLog>();
        }

        public int EmailId { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public string Attachments { get; set; }
        public string WebsiteUrl { get; set; }
        public string EmailType { get; set; }
        public string TemplateName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string FailedMessage { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }

        public virtual ICollection<EmailLog> EmailLogs { get; set; }
    }
}
