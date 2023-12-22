
using SMSEmailService.DAL.Repository;
using System;
namespace SMSEmailService.DAL.BaseFiles
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

    }

    public interface IManageUnitOfWork : IUnitOfWork
    {

        DB DbContext { get; }
        IEmailRepository Email { get; }
        ISMSRepository SMS { get; }
        IAccessCodeRepository AccessCode { get; }
        IEmailLogRepository EmailLog { get; }
        ISubscriptionRepository Subscription { get; }
        ISMSLogRepository SMSLog { get; }
        ISMSExceptionRepository SMSException { get; }
        ICompanyRepository Company { get; }
        IInBoundSMSRepository InBoundSMS { get; }
        IUserProfileRepository UserProfile { get; }
        ILogRepository Log { get; }
    }

}
