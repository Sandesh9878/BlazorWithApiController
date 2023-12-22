using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface ILogRepository:IRepository<Log>
    {
    }

    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(DB context) : base(context) { }
    }
}
