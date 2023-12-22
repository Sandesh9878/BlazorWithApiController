using Newtonsoft.Json.Linq;
using SMSEmailService.DAL.Context;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMSEmailService.BLL.SMSService
{
    public interface ISMSServices
    {
        SMSDetailModel GetSMSDetail(int id);

        FilteredData GetSMS(FinalFilterParameters filters);

        int IsSubscribed(SendSMSModel model);

        int SubscribeUser(SendSMSModel model);

        JObject sendSMS(CompanyConfiguration config, SendSMSModel model,int subid);

        int CreateSMS(SendSMSModel model);

        void UpdateSMSStatus(int id, string status);

        SendSMSModel GetSendSMSModel(int smsId);
    }
}
