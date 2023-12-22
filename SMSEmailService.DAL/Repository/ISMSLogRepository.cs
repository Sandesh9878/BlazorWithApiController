using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface ISMSLogRepository:IRepository<SMSLog>
    {
    }

    public class SMSLogRepository : Repository<SMSLog>, ISMSLogRepository
    {
        public SMSLogRepository(DB context) : base(context) { }
    }
}
