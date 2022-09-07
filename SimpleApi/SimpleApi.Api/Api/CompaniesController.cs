using Microsoft.AspNetCore.Mvc;
using SimpleApi.Api.ApiModels;
using SimpleApi.Core.Interfaces;
using SimpleApi.Core.ProjectAggregate;

namespace SimpleApi.Api.Api
{
    public class CompaniesController : BaseApiController
    {
        private readonly ICompanyRepository _repository;

        public CompaniesController(ICompanyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var CompanyDTOs = (_repository.GetAll())
                .Select(Company => new CompanyDTO
                {
                    Id = Company.Id,
                    Name = Company.Name,
                    StockTicker = Company.StockTicker,
                    Exchange = Company.Exchange,
                    Isin = Company.Isin,
                    Website = Company.Website
                })
                .ToList();

            return Ok(CompanyDTOs);
        }

        // GET: api/Companies
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Company = _repository.GetById(id);

            var result = new CompanyDTO
            {
                Id = Company.Id,
                Name = Company.Name,
                StockTicker = Company.StockTicker,
                Exchange = Company.Exchange,
                Isin = Company.Isin,
                Website = Company.Website
            };

            return Ok(result);
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCompanyDTO request)
        {
            var newCompany = new Company(request.Name, request.StockTicker, request.Exchange, request.Isin, request.Website);

            if (_repository.GetByIsin(request.Isin) != null)
            {
                return Conflict($"Company already exists with Isin: {request.Isin}");
            }

            var createdCompany = _repository.Add(newCompany);

            var result = new CompanyDTO
            {
                Id = createdCompany.Id,
                Name = createdCompany.Name,
                StockTicker = createdCompany.StockTicker,
                Exchange = createdCompany.Exchange,
                Isin = createdCompany.Isin,
                Website = createdCompany.Website
            };

            return Ok(result);
        }
    }
}
