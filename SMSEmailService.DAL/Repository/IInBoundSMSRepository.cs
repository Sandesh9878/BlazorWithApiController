using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface IInBoundSMSRepository:IRepository<InBoundSMS>
    {
    }
    public class InBoundSMSRepository : Repository<InBoundSMS>, IInBoundSMSRepository
    {
        public InBoundSMSRepository(DB context) : base(context) { }

    }
}
