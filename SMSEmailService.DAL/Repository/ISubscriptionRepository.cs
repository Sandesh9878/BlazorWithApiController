using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
    }

    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(DB context) : base(context) { }

    }
}
