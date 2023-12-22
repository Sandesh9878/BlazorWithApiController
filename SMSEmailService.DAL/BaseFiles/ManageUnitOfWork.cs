using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;
using System;


namespace SMSEmailService.DAL.Repository
{
    public class ManageUnitOfWork : IManageUnitOfWork
    {
        #region Constructor
        public ManageUnitOfWork(DB context)
        {
            _dbContext = context;
            var scontext = new SMSContext();
        }

        #endregion

        #region Fields
        private readonly DB _dbContext;
        IEmailRepository _Email;
        ISMSRepository _SMS;
        IAccessCodeRepository _AccessCode;
        IEmailLogRepository _EmailLog;
        ISubscriptionRepository _Subscription;
        ISMSLogRepository _SMSLog;
        ISMSExceptionRepository _SMSException;
        ICompanyRepository _Company;
        IInBoundSMSRepository _InBoundSMS;
        IUserProfileRepository _UserProfile;
        ILogRepository _Log;
        #endregion

        #region Properties

        public DB DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public IEmailRepository Email
        {
            get
            {
                if (_Email == null)
                    _Email = new EmailRepository(_dbContext);
                return _Email;
            }
        }

        public ISMSRepository SMS
        {
            get
            {
                if (_SMS == null)
                    _SMS = new SMSRepository(_dbContext);
                return _SMS;
            }
        }

        public IAccessCodeRepository AccessCode
        {
            get
            {
                if (_AccessCode == null)
                    _AccessCode = new AccessCodeRepository(_dbContext);
                return _AccessCode;
            }
        }

        public IEmailLogRepository EmailLog
        {
            get
            {
                if (_EmailLog == null)
                    _EmailLog = new EmailLogRepository(_dbContext);
                return _EmailLog;
            }
        }
        public ISubscriptionRepository Subscription {
            get
            {
                if (_Subscription == null)
                    _Subscription = new SubscriptionRepository(_dbContext);
                return _Subscription;
            }
        }

        public ISMSLogRepository SMSLog
        {
            get
            {
                if (_SMSLog == null)
                    _SMSLog = new SMSLogRepository(_dbContext);
                return _SMSLog;
            }
        }

        public ISMSExceptionRepository SMSException
        {
            get
            {
                if (_SMSException == null)
                    _SMSException = new SMSExceptionRepository(_dbContext);
                return _SMSException;
            }
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_Company == null)
                    _Company = new CompanyRepository(_dbContext);
                return _Company;
            }
        }

        public IInBoundSMSRepository InBoundSMS
        {
            get
            {
                if (_InBoundSMS == null)
                    _InBoundSMS = new InBoundSMSRepository(_dbContext);
                return _InBoundSMS;
            }
        }

        public IUserProfileRepository UserProfile
        {
            get
            {
                if (_UserProfile == null)
                    _UserProfile = new UserProfileRepository(_dbContext);
                return _UserProfile;
            }
        }

        public ILogRepository Log
        {
            get
            {
                if (_Log == null)
                    _Log = new LogRepository(_dbContext);
                return _Log;
            }
        }
        #endregion

        #region Utility
        public void Dispose()
        {
            //if (categories != null)
            //    categories.Dispose();        
            if (_dbContext != null)
                _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        #endregion




    }
}
