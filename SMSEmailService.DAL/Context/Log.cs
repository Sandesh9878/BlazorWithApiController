using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string LogData { get; set; }
        public DateTime LogDate { get; set; }
    }
}
