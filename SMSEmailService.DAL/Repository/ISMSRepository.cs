using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface ISMSRepository:IRepository<SMSs>
    {
        //IEnumerable<SMSModel> GetPendingSubscriptionSMS();
      //  IEnumerable<SMSModel> GetPendingSMS();
    }

    public class SMSRepository : Repository<SMSs>, ISMSRepository
    {
        public SMSRepository(DB context) : base(context) { }

        //public IEnumerable<SMSModel> GetPendingSMS()
        //{
        //    return Context.Database.SqlQuery<SMSModel>("SMS_GetPendingItem");
        //}

        //public IEnumerable<SMSModel> GetPendingSubscriptionSMS()
        //{
        //    return Context.Database.SqlQuery<SMSModel>("SMS_PendingSubscriberList");
        //}
    }
}
