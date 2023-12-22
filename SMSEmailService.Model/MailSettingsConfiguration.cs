using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class MailSettingsConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Debug { get; set; }
    }
}
