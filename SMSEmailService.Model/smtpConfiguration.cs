using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class smtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool DefaultCredentials { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
