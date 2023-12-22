using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface IEmailLogRepository:IRepository<EmailLog>
    {
    }

    public class EmailLogRepository : Repository<EmailLog>, IEmailLogRepository
    {
        public EmailLogRepository(DB context) : base(context) { }
    }
}
