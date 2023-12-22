using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface IUserProfileRepository:IRepository<UserProfile>
    {

    }
    public class UserProfileRepository: Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DB context) : base(context) { }
    }

}
