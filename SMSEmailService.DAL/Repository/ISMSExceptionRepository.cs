using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface ISMSExceptionRepository:IRepository<SMSException>
    {
    }
    public class SMSExceptionRepository : Repository<SMSException>, ISMSExceptionRepository
    {
        public SMSExceptionRepository(DB db) : base(db) { }
    }
}
