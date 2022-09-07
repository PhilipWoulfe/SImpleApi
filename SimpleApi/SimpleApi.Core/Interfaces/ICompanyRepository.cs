using SimpleApi.Core.ProjectAggregate;

namespace SimpleApi.Core.Interfaces
{
    public interface ICompanyRepository: IGenericRepository<Company>
    {
        Company GetByIsin(string isin);
        Company Update(Company updatedCompany, Company updatedCompany1);
    }
}
