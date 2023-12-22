using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SMSEmailService.Model;
using Newtonsoft.Json;
using System;
using static SMSEmailService.Enumerations;
using SMSEmailService.BLL.SMSService;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace SMSEmailService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    [EnableCors("AllowOrigin")]
    public class SMSController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISMSServices _smsService;
        public SMSController(ISMSServices smsService, IConfiguration configuration)
        {
            _configuration = configuration;
            _smsService = smsService;
        }

        [HttpPost]
        [Route("SendSMS")]
        public JsonResult SendSMS(SendSMSModel model)
        {
            CompanyConfiguration smsconfig = new CompanyConfiguration();
            ResponseModel resmodel = new ResponseModel();
            resmodel.IsSuccess = false;
            int subid = _smsService.IsSubscribed(model);
            if (subid == 0)
                subid = _smsService.SubscribeUser(model);
            try
            {
                var id = _smsService.CreateSMS(model);
                resmodel.IsSuccess = true;
                try
                {
                    var response = _smsService.sendSMS(smsconfig, model, subid);
                    resmodel.responseData = response;
                    _smsService.UpdateSMSStatus(id, SMSStatus.Success.ToString());
                }
                catch (Exception ex)
                {
                    _smsService.UpdateSMSStatus(id,SMSStatus.Failed.ToString());
                    resmodel.ErrorCode = "404";
                    resmodel.Message = ex.ToString();
                }
            }
            catch (Exception ex) 
            {
                resmodel.ErrorCode = ex.ToString();
            }
            return Json(resmodel);
        }


        [HttpPost]
        [Route("ReSendSMS")]
        public JsonResult ReSendSMS(int smsId)
        {
            CompanyConfiguration smsconfig = new CompanyConfiguration();
            ResponseModel resmodel = new ResponseModel();
            resmodel.IsSuccess = false;
            var model = _smsService.GetSendSMSModel(smsId);
            int subid = _smsService.IsSubscribed(model);
            if (subid == 0)
                subid = _smsService.SubscribeUser(model);
            try
            {
                resmodel.IsSuccess = true;
                try
                {
                    var response = _smsService.sendSMS(smsconfig, model, subid);
                    resmodel.responseData = response;
                    _smsService.UpdateSMSStatus(smsId, SMSStatus.Success.ToString());
                }
                catch (Exception ex)
                {
                    _smsService.UpdateSMSStatus(smsId, SMSStatus.Failed.ToString());
                    resmodel.ErrorCode = "404";
                    resmodel.Message = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                resmodel.ErrorCode = ex.ToString();
            }
            return Json(resmodel);
        }

            [HttpPost]
        [Route("GetSMS")]
        public JsonResult GetSMS([FromBody] FinalFilterParameters filters)
        {
            ResponseModel model = new ResponseModel();
            var source = _smsService.GetSMS(filters);
            model.responseData = source;
            model.IsSuccess = true;
            return Json(model);
        }

        [HttpGet]
        [Route("GetDetail")]
        public JsonResult GetDetail(int id)
        {
            var model = new ResponseModel();
            var filter = _smsService.GetSMSDetail(id);
            model.responseData = filter;
            model.IsSuccess = true;
            return Json(model);
        }


    }
}
