using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.Model
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
