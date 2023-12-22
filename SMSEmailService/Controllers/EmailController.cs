using Microsoft.AspNetCore.Mvc;
using SMSEmailService.BLL.EmailServices;
using SMSEmailService.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static SMSEmailService.Enumerations;
using SMSEmailService.BLL.smtpService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace SMSEmailService.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class EmailController : Controller
    {

        private readonly IEmailService emailService;
        private readonly IConfiguration _configuration;
        private readonly IsmtpServices smtpService;
        public EmailController(IEmailService _emailService, IConfiguration iconfig, IsmtpServices service)
        {
            emailService=_emailService;
            _configuration = iconfig;
            smtpService = service;
        }

        [HttpPost]
        [Route("Sendemail")]
        public JsonResult SendEmail(EmailModel email)
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = false;
            try
            {
                email.Status = EmailStatus.Pending.ToString();
                var id = emailService.AddEmail(email);
                var emailmodel = emailService.GetEmailDb(email);
                Task.Run(() => smtpService.SendEmail(emailmodel, id));
            }
            catch (Exception ex)
            {
                response.Message = ex.StackTrace.ToString();
                return Json(response);
            }
            response.IsSuccess = true;
            return Json(response);
        }


        [HttpPost]
        [Route("ReSendemail")]
        public JsonResult ReSendEmail(int id)
        {
            ResponseModel response = new ResponseModel();
            response.IsSuccess = false;
            var email = emailService.GetEmail(id);
            Task.Run(() => smtpService.SendEmail(email, id));
            response.IsSuccess = true;
            response.Message = "Email has been queued to send";
            return Json(response);
        }

        [HttpGet]
        [Route("FailedEmails")]
        public JsonResult FailedEmails()
        {
            ResponseModel model = new ResponseModel();
            model.responseData = JsonConvert.SerializeObject(emailService.GetFailedEmail());
            model.IsSuccess = true;
            return Json(model);
        }

        [HttpPost]
        [Route("GetEmail")]
        public JsonResult GetEmail([FromBody] FinalFilterParameters filters)
        {
            ResponseModel model = new ResponseModel();
            var source = emailService.GetEmail(filters);
            model.responseData = source;
            model.IsSuccess = true;
            return Json(model);
        }

        [HttpGet]
        [Route("GetDetail")]
        public JsonResult GetDetail(int id)
        {
            var model = new ResponseModel();
            var filter = emailService.GetDetail(id);
            model.responseData = filter;
            model.IsSuccess = true;
            return Json(model);
        }
    }
}