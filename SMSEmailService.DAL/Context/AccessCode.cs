using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class AccessCode
    {
        public int AccessCodeId { get; set; }
        public string AccessType { get; set; }
        public string AccessCode1 { get; set; }
        public bool IsActive { get; set; }
    }
}
