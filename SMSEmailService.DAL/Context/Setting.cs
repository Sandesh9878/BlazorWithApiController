using System;
using System.Collections.Generic;

#nullable disable

namespace SMSEmailService.DAL.Context
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
    }
}
