using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSEmailService.Model;

namespace SMS.BLL.UserProfile
{
    public interface IUserProfileService
    {
        bool GetUserProfile(string Email, string Password);

    }
}
