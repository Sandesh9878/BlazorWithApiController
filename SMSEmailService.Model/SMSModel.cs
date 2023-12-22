using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class SMSModel
    {
        public int smsid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string WebSiteUrl { get; set; }
        public string SMSType { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
