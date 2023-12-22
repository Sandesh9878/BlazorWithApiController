using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;

namespace SMSEmailService.DAL.Repository
{
    public interface IEmailRepository:IRepository<Email>
    {
      //  IEnumerable<EmailModel> GetPendingEmails();

        
    }

    public class EmailRepository : Repository<Email>, IEmailRepository
    {
        public EmailRepository(DB context) : base(context) { }

      //  public IEnumerable<EmailModel> GetPendingEmails()
       // {
           // return Context.Database.SqlQuery<EmailModel>("Email_GetPendingItem");
      //  }

       
    }
}
