using Microsoft.AspNetCore.Mvc;
using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using static SMSEmailService.Enumerations;

namespace SMSEmailService.BLL.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IManageUnitOfWork Context;
        public EmailService(IManageUnitOfWork context)
        {
            Context = context;
        }

        public int AddEmail(EmailModel email)
        {
            var model = GetEmailDb(email);
            Context.Email.Create(model);
            Context.SaveChanges();
            var elog = new EmailLog
            {
                EmailId = model.EmailId,
                Status = model.Status,
                CreatedDate = DateTime.Now
            };
            Context.EmailLog.Create(elog);
            Context.SaveChanges();
            return model.EmailId;
        }

        public Email GetEmailDb(EmailModel email)
        {
            var model = new Email
            {
                SenderEmail = email.SenderEmail,
                ReceiverEmail = email.ReceiverEmail,
                Subject = email.Subject,
                Status = email.Status,
                EmailContent = email.EmailContent,
                CreatedDate = DateTime.Now,
                EmailType = email.EmailType,
                FailureUrl = email.FailureUrl,
                SuccessUrl = email.FailureUrl,
                SenderName = email.SenderName,
                TemplateName = email.TemplateName,
                WebsiteUrl = email.WebsiteUrl,
                FailedMessage = email.FailedMessage,
            };
            return model;
        }

        public Email GetEmail(int emailId)
        {
            var emailEntity = Context.Email.Find(a => a.EmailId == emailId);
            return emailEntity;
        }

        public void UpdateStatus(int emailId, string status)
        {
            var emailEntity = Context.Email.Find(a => a.EmailId == emailId);
            emailEntity.Status = status;
            emailEntity.TransactionDate = DateTime.Now;
            Context.Email.Update(emailEntity);
            Context.SaveChanges();
            var emailLog = new EmailLog
            {
                CreatedDate = DateTime.Now,
                EmailId = emailId,
                Status = status
            };
            Context.EmailLog.Create(emailLog);
            Context.SaveChanges();
        }

        public List<EmailModel> GetFailedEmail()
        {
            List<EmailModel> models = new List<EmailModel>();
            var results = Context.Email.All().Where(x => x.Status == "Failed");
            foreach (var result in results)
            {
                var model = new EmailModel
                {
                    SenderEmail = result.SenderEmail,
                    EmailContent = result.EmailContent,
                    ReceiverEmail = result.ReceiverEmail,
                    Subject = result.ReceiverEmail,
                    Status = result.Status,
                    SenderName = result.SenderName,
                    EmailType = result.EmailType,
                    WebsiteUrl = result.WebsiteUrl,
                    TemplateName = result.TemplateName,
                };
                models.Add(model);
            }
            return models;
        }


        public FilteredData GetEmail(FinalFilterParameters Filters)
        {
            var filters = Filters.filters;
            var emailList = Context.Email.All();
            var order = Filters.Order;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.ColumnName == EmailColumns.SenderName.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.SenderName.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.SenderName.Equals(filter.Value));
                    }

                    if (filter.ColumnName == EmailColumns.EmailType.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.EmailType.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.EmailType.Equals(filter.Value));
                    }

                    if (filter.ColumnName == EmailColumns.SenderEmail.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.SenderEmail.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.SenderEmail.Equals(filter.Value));
                    }

                    if (filter.ColumnName == EmailColumns.ReceiverEmail.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.ReceiverEmail.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.ReceiverEmail.Equals(filter.Value));
                    }

                    if (filter.ColumnName == EmailColumns.Subject.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.Subject.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.Subject.Equals(filter.Value));

                    }


                    if (filter.ColumnName == EmailColumns.WebsiteUrl.ToString())
                    {
                        if (filter.Type == checker.Contains.ToString())
                            emailList = emailList.Where(a => a.WebsiteUrl.Contains(filter.Value));
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.WebsiteUrl.Equals(filter.Value));

                    }


                    if (filter.ColumnName == EmailColumns.Status.ToString())
                    {
                        if (filter.Type == checker.Equals.ToString())
                        {
                            if (filter.Value == EmailStatus.Failed.ToString())
                                emailList = emailList.Where(a => a.Status == EmailStatus.Failed.ToString());
                            if (filter.Value == EmailStatus.Success.ToString())
                                emailList = emailList.Where(a => a.Status == EmailStatus.Success.ToString());
                            if (filter.Value == EmailStatus.Pending.ToString())
                                emailList = emailList.Where(a => a.Status == EmailStatus.Pending.ToString());
                        }
                    }
                    if (filter.ColumnName == EmailColumns.CreatedDate.ToString())
                    {
                        DateTime date = Convert.ToDateTime(filter.Value);
                        if (filter.Type == checker.Equals.ToString())
                            emailList = emailList.Where(a => a.CreatedDate.Equals(filter.Value));
                        if (filter.Type == checker.GreaterThan.ToString())
                            emailList = emailList.Where(a => a.CreatedDate >= date);
                        if (filter.Type == checker.LessThan.ToString())
                            emailList = emailList.Where(a => a.CreatedDate <= date);

                    }
                }
            }

            var model = new FilteredData();
            if (!string.IsNullOrEmpty(Filters.Order.FieldName))
            {
                if (order.FieldName == EmailColumns.CreatedDate.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.CreatedDate);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.CreatedDate);
                    }
                }
                if (order.FieldName == EmailColumns.ReceiverEmail.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.ReceiverEmail);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.ReceiverEmail);
                    }
                }

                if (order.FieldName == EmailColumns.EmailType.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.EmailType);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.EmailType);
                    }
                }

                if (order.FieldName == EmailColumns.SenderEmail.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.SenderEmail);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.SenderEmail);
                    }
                }
                if (order.FieldName == EmailColumns.SenderName.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.SenderName);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.SenderName);
                    }
                }
                if (order.FieldName == EmailColumns.Status.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.Status);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.Status);
                    }
                }
                if (order.FieldName == EmailColumns.Subject.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.Subject);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.Subject);
                    }
                }
                if (order.FieldName == EmailColumns.WebsiteUrl.ToString())
                {
                    if (order.IsOrderByAscending == false)
                    {
                        emailList = emailList.OrderByDescending(x => x.WebsiteUrl);
                    }
                    else
                    {
                        emailList = emailList.OrderBy(x => x.WebsiteUrl);
                    }
                }

            }

            model.Count = emailList.Count();
            List<Email> email = emailList.Skip(Filters.size * (Filters.pageNo - 1)).Take(Filters.size).ToList();
            model.Data = filterEmail(email);
            return model;
        }

        private List<EmailListing> filterEmail(List<Email> emails)
        {
            var results = new List<EmailListing>();
            foreach(var email in emails)
            {
                var result = new EmailListing
                {
                    EmailID = email.EmailId,
                    SenderName = email.SenderName,
                    SenderEmail = email.SenderEmail,
                    ReceiverEmail = email.ReceiverEmail,
                    Status = email.Status,
                    EmailType = email.EmailType,
                    CreatedDate = email.CreatedDate,
                    Subject = email.Subject,
                    WebsiteUrl = email.WebsiteUrl
                };
                results.Add(result);
            }
            return results;
        }

        public EmailDetailViewModel GetDetail(int id)
        {
            FilteredData filter = new FilteredData();
            var results = Context.Email.Filter(x => x.EmailId == id).Select(n => new EmailDetailViewModel
            {
                EmailId = n.EmailId,
                SenderName = n.SenderName,
                SenderEmail = n.SenderEmail,
                ReceiverEmail = n.ReceiverEmail,
                Status = n.Status,
                EmailType = n.EmailType,
                CreatedDate = n.CreatedDate,
                Subject = n.Subject,
                WebsiteUrl = n.WebsiteUrl,
                Attachments = n.Attachments,
                EmailContent = n.EmailContent,
                FailedMessage = n.FailedMessage,
                FailureUrl = n.FailureUrl,
                SuccessUrl = n.SuccessUrl,
                TemplateName = n.TemplateName,
                TransactionDate = n.TransactionDate
            }).FirstOrDefault();
            results.EmailLogs = Context.EmailLog.Filter(a => a.EmailId == id)
                .Select(n => new EmailLogViewModel
                {
                    EmailId = n.EmailId,
                    CreatedDate = n.CreatedDate,
                    Status = n.Status
                }).ToList();
            return results;
        }
    }
}