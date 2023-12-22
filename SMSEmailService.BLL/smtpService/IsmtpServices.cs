using SMSEmailService.DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.BLL.smtpService
{
    public interface IsmtpServices
    {
        public void SendEmail(Email email, int id);
    }
}
