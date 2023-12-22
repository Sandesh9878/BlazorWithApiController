using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class EmailListing
    {
        public int EmailID { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string WebsiteUrl { get; set; }
        public string EmailType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
