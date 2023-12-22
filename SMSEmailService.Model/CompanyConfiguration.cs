using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class CompanyConfiguration
    {
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        public string DistributeListName { get; set; }
        public int SMSApiRequestWaitTime { get; set; }
    }
}
