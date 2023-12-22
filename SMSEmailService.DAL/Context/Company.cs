using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class Company
    {
        public Company()
        {
            InBoundSMs = new HashSet<InBoundSMS>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string ApiKey { get; set; }
        public string DistributeListName { get; set; }
        public string SuccessUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<InBoundSMS> InBoundSMs { get; set; }
    }
}
