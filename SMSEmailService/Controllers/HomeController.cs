using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMSEmailService.Model;
using System;
using System.Linq;

namespace SMSEmailService.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
       
        public ActionResult Login()
        {
            return View();
        }

    }
}
