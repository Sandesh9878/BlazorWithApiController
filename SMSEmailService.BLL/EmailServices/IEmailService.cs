using Microsoft.AspNetCore.Mvc;
using SMSEmailService.DAL.Context;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.BLL.EmailServices
{
    public interface IEmailService
    {
        List<EmailModel> GetFailedEmail();

        int AddEmail(EmailModel email);

        void UpdateStatus(int emailId, string status);

        FilteredData GetEmail(FinalFilterParameters filter);

        Email GetEmail(int emailId);

        Email GetEmailDb(EmailModel email);

        public EmailDetailViewModel GetDetail(int id);
    }
}
