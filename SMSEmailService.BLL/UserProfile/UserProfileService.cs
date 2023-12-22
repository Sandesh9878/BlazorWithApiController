using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.Model;

namespace SMS.BLL.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IManageUnitOfWork Context;
        public UserProfileService(IManageUnitOfWork context)
        {
            Context = context;
        }

        public bool GetUserProfile(string Email, string Password)
        {
            var result = (from o in Context.UserProfile.All()
                         where o.Email == Email && o.PasswordHash == Password
                         select new
                         {
                             o.Email,
                             o.PasswordHash
                         }).Any();
            return result;

        }

    }
}
