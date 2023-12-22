using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class SMSLogViewModel
    {
        public int SMSId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
