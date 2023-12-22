using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static SMSEmailService.Enumerations;
using Newtonsoft.Json.Linq;

namespace SMSEmailService.BLL.SMSService
{
    public class SMSServices : ISMSServices
    {
        static readonly string _subscriptUri = "http://api.trumpia.com/rest/v1/{0}/subscription";
        static readonly string _sendUri = "http://api.trumpia.com/rest/v1/{0}/message";
        private readonly IManageUnitOfWork Context;
        public SMSServices(IManageUnitOfWork _context)
        {
            Context = _context;
        }

        public FilteredData GetSMS(FinalFilterParameters Filters)
        {
            var filters = Filters.filters;
            var smsList = Context.SMS.All();
            var order = Filters.Order;
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.ColumnName == SMSColumn.WebsiteUrl.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.WebSiteUrl.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.FailureUrl.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.CreatedDate.ToString())
                    {
                        DateTime date = Convert.ToDateTime(filter.Value);
                        if (filter.Type == checker.GreaterThan.ToString())
                            smsList = smsList.Where(x => x.CreatedDate >= date);
                        if (filter.Type == checker.LessThan.ToString())
                            smsList = smsList.Where(x => x.CreatedDate <= date);
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.FailureUrl.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.FirstName.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.FirstName.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.FirstName.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.LastName.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.LastName.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.LastName.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.MobileNo.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.MobileNo.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.MobileNo.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.SMSType.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.SMSType.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.SMSType.Equals(filter.Value));
                    }
                    if (filter.ColumnName == SMSColumn.Subject.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            smsList = smsList.Where(x => x.Subject.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            smsList = smsList.Where(x => x.Subject.Equals(filter.Value));
                    }
                }
            }

            if (!string.IsNullOrEmpty(Filters.Order.FieldName))
            {
                if (order.FieldName == SMSColumn.CreatedDate.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.CreatedDate);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.CreatedDate);
                    }
                }

                if (order.FieldName == SMSColumn.FirstName.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.FirstName);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.FirstName);
                    }
                }

                if (order.FieldName == SMSColumn.LastName.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.LastName);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.LastName);
                    }
                }
                if (order.FieldName == SMSColumn.WebsiteUrl.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.WebSiteUrl);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.WebSiteUrl);
                    }
                }

                if (order.FieldName == SMSColumn.MobileNo.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.MobileNo);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.MobileNo);
                    }
                }

                if (order.FieldName == SMSColumn.SMSType.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.SMSType);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.SMSType);
                    }
                }
                if (order.FieldName == SMSColumn.Subject.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        smsList = smsList.OrderByDescending(x => x.Subject);
                    }
                    else
                    {
                        smsList = smsList.OrderBy(x => x.Subject);
                    }
                }
            }
            var model = new FilteredData();
            model.Count = smsList.Count();
            List<SMSs> sms = smsList.Skip(Filters.size * (Filters.pageNo - 1)).Take(Filters.size).ToList();
            model.Data = filterSMS(sms);
            return model;
        }

        private List<SMSModel> filterSMS(List<SMSs> smss)
        {
            var results = new List<SMSModel>();
            foreach (var sms in smss)
            {
                var result = new SMSModel
                {
                    smsid = sms.SMSId,
                    FirstName = sms.FirstName,
                    LastName = sms.LastName,
                    MobileNo = sms.MobileNo,
                    SMSType = sms.SMSType,
                    Subject = sms.Subject,
                    WebSiteUrl = sms.WebSiteUrl,
                    CreatedDate = sms.CreatedDate,
                };
                results.Add(result);
            }
            return results;
        }


        public SMSDetailModel GetSMSDetail(int id)
        {
            FilteredData filter = new FilteredData();
            var results = Context.SMS.Filter(x => x.SMSId == id).Select(n => new SMSDetailModel
            {
                SMSId = n.SMSId,
                CompanyId = n.CompanyId,
                CreatedDate = n.CreatedDate,
                FailedMessage = n.FailedMessage,
                FailureUrl = n.FailureUrl,
                FirstName = n.FirstName,
                IsFailed = n.IsFailed,
                LastName = n.LastName,
                MobileNo = n.MobileNo,
                SendSMSResponse = n.SendSMSResponse,
                SMSContent = n.SMSContent,
                SMSRequestId = n.SMSRequestId,
                SMSTemplate = n.SMSTemplate,
                SMSType = n.SMSType,
                Status = n.Status,
                Subject = n.Subject,
                SubscriptionRequestId = n.SubscriptionRequestId,
                SubscriptionResponse = n.SubscriptionResponse,
                SuccessUrl = n.SuccessUrl,
                TransactionDate = n.TransactionDate,
                WebSiteUrl = n.WebSiteUrl
            }).FirstOrDefault();
            results.SMSLogs = Context.SMSLog.Filter(a => a.SMSId == id)
                .Select(n => new SMSLogViewModel
                {
                    SMSId = n.SMSId,
                    CreatedDate = n.CreatedDate,
                    Status = n.Status,
                }).ToList();
            return results;
        }

        public int IsSubscribed(SendSMSModel model)
        {
            var submodel = Context.Subscription.Filter(x => x.MobileNo.Equals(model.MobileNo)).FirstOrDefault();
            if(submodel!=null)
                if (submodel.MobileNo == model.MobileNo && submodel.LastName == model.LastName && submodel.FirstName == model.FirstName)
                    return submodel.SMSSubscriptionId;
            return 0;
        }

        public int SubscribeUser(SendSMSModel model)
        {
            var dbmodel = GetSubscriptionDb(model);
            Context.Subscription.Create(dbmodel);
            Context.SaveChanges();
            return dbmodel.SMSSubscriptionId;
        }

        public Subscription GetSubscriptionDb(SendSMSModel model)
        {
            var submodel = new Subscription
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNo = model.MobileNo
            };
            return submodel;
        }

        public JObject sendSMS(CompanyConfiguration smsconfig, SendSMSModel model, int subid)
        {
            APIRequest req = new APIRequest(smsconfig);
            JObject r1 = null;
            var smsModel = new SendSMS
            {
                description = "CONCIERGE NOTIFICATION",
                recipents = new Recipients { type = "subscription", value = subid },
                Content = model.SMSContent
            };
            r1 = req.ProcessRequest<JObject>(
                smsModel,
                string.Format(_sendUri, smsconfig.UserName),
                "PUT");
            return r1;

        }

        public int CreateSMS(SendSMSModel model)
        {
            var smsModel = new SMSs
            {
                FirstName = model.FirstName,
                CreatedDate = DateTime.Now,
                LastName = model.LastName,
                MobileNo = model.MobileNo,
                WebSiteUrl = model.WebSiteUrl,
                FailureUrl = model.FailureUrl,
                SMSContent = model.SMSContent,
                SMSType = model.SMSType,
                Status = SMSStatus.Pending.ToString(),
                Subject = model.Subject,
                SuccessUrl = model.SuccessUrl,
                SMSTemplate = model.SMSTemplate,
            };
            Context.SMS.Create(smsModel);
            Context.SaveChanges();
            return smsModel.SMSId;
        }

        public void UpdateSMSStatus(int id, string status)
        {
            var sms = Context.SMS.Find(x => x.SMSId == id);
            sms.Status = status;
            sms.TransactionDate = DateTime.Now;
            Context.SMS.Update(sms);
            Context.SaveChanges();
            var smsLog = new SMSLog
            {
                CreatedDate = DateTime.Now,
                SMSId = id,
                Status = status
            };
            Context.SMSLog.Create(smsLog);
            Context.SaveChanges();
        }

        public SendSMSModel GetSendSMSModel(int smsId)
        {
            var model = Context.SMS.Find(x => x.SMSId == smsId);
            var sendsmsModel = new SendSMSModel
            {
                FailureUrl = model.FailureUrl,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNo = model.MobileNo,
                SMSContent = model.SMSContent,
                SMSTemplate = model.SMSTemplate,
                SMSType = model.SMSType,
                Status = model.Status,
                Subject= model.Subject,
                SuccessUrl = model.SuccessUrl,
                 WebSiteUrl = model.WebSiteUrl
            };
            return sendsmsModel;
        }
    }
}
