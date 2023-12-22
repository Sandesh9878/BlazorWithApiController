using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SMSEmailService.BLL.EmailServices;
using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;
using SMSEmailService.DAL.Repository;
using SMSEmailService.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using static SMSEmailService.Enumerations;

namespace SMSEmailService.BLL.smtpService
{
    public class smtpServices : IsmtpServices
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<smtpConfiguration> _options;
        public smtpServices(IConfiguration iconfig, IOptions<smtpConfiguration> options)
        {
            _configuration = iconfig;
            _options = options;
        }

        public void SendEmail(Email email, int id)
        {
            var smtp = new SmtpClient
            {
                Host = _options.Value.Host,
                Port = _options.Value.Port,
                EnableSsl = _options.Value.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _options.Value.DefaultCredentials,
                Credentials = new NetworkCredential(_options.Value.email ,_options.Value.password)
            };
            var eService = new EmailService(new ManageUnitOfWork(new DB(_configuration.GetConnectionString("DefaultConnection"))));
            var message = new MailMessage(email.SenderEmail, email.ReceiverEmail, email.Subject, email.EmailContent);
            try
            {
                smtp.Send(message);
                eService.UpdateStatus(id, EmailStatus.Success.ToString());
                _ = (HttpWebRequest)WebRequest.Create(email.SuccessUrl);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        System.Threading.Thread.Sleep(5000);
                        smtp.Send(message);
                        eService.UpdateStatus(id, EmailStatus.Success.ToString());
                        _ = (HttpWebRequest)WebRequest.Create(email.SuccessUrl);
                    }
                    else
                    {
                        eService.UpdateStatus(id, EmailStatus.Failed.ToString());
                        _ = (HttpWebRequest)WebRequest.Create(email.FailureUrl);
                    }
                }
            }
            catch (Exception)
            {
                eService.UpdateStatus(id, EmailStatus.Failed.ToString());
                _ = (HttpWebRequest)WebRequest.Create(email.FailureUrl);
            }
        }
    }
}