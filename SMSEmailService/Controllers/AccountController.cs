using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.BLL.UserProfile;
using SMSEmailService.Model;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;


namespace SMS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly IUserProfileService _context;


        public AccountController(IUserProfileService Context)
        {
            _context = Context;
            
        }
    }
}