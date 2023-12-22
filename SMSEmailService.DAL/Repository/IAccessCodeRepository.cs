using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;


namespace SMSEmailService.DAL.Repository
{
    public interface IAccessCodeRepository:IRepository<AccessCode>
    {
    }

    public class AccessCodeRepository : Repository<AccessCode>, IAccessCodeRepository
    {
        public AccessCodeRepository(DB context) : base(context) { }
    }
}
