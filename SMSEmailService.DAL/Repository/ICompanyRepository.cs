using SMSEmailService.DAL.BaseFiles;
using SMSEmailService.DAL.Context;


namespace SMSEmailService.DAL.Repository
{
    public interface ICompanyRepository:IRepository<Company>
    {
    }

    public class CompanyRepository:Repository<Company>,ICompanyRepository
    {
        public CompanyRepository(DB context) : base(context) { }
    }
}
