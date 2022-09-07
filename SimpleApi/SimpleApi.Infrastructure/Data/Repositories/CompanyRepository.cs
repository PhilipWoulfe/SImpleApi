using SimpleApi.Core.ProjectAggregate;
using SimpleApi.Core.Interfaces;

namespace SimpleApi.Infrastructure.Data.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext dbContext): base(dbContext)
        {

        }

        public Company GetByIsin(string isin)
        {
            return _dbContext.Companies.SingleOrDefault(c => c.Isin == isin);
        }

        public Company Update(Company companyToUpdate, Company updatedCompany)
        {
            companyToUpdate.UpdateCompany(updatedCompany);
            
            _dbContext.Companies.Update(companyToUpdate);
            _dbContext.SaveChanges();

            return companyToUpdate;
        }
    }
}
