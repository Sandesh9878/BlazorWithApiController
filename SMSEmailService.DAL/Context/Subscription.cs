using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class Subscription
    {
        public int SMSSubscriptionId { get; set; }
        public string SubscriptionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
    }
}
