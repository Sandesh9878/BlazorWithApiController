using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class SendSMS
    {
        public string description { get; set; }
        public Recipients recipents { get; set; }
        public string Content { get; set; }
    }
}
