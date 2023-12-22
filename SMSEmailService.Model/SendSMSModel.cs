using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class SendSMSModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string SMSContent { get; set; }
        public string WebSiteUrl { get; set; }
        public string SMSType { get; set; }
        public string SMSTemplate { get; set; }
        public string Status { get; set; }
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
    }
}
