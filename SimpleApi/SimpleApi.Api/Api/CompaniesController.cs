using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleApi.Api.ApiModels;
using SimpleApi.Core.Interfaces;
using SimpleApi.Core.ProjectAggregate;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SimpleApi.Api.Api
{
    public class CompaniesController : BaseApiController
    {
        private readonly ICompanyRepository _repository;
        private IValidator<Company> _validator;

        public CompaniesController(ICompanyRepository repository, IValidator<Company> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var companies = _repository.GetAll();
            List<CompanyDTO> companyDTOs = new();

            if (companies != null)
            {                
                companyDTOs.AddRange(companies
                   .Select(company => new CompanyDTO
                   {
                       Id = company.Id,
                       Name = company.Name,
                       StockTicker = company.StockTicker,
                       Exchange = company.Exchange,
                       Isin = company.Isin,
                       Website = company.Website
                   })
                   .ToList());
            }

            return Ok(companyDTOs);
        }

        // GET: api/Companies
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var company = _repository.GetById(id);

            if (company == null)
            {
                return Ok();
            }

            var result = new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                StockTicker = company.StockTicker,
                Exchange = company.Exchange,
                Isin = company.Isin,
                Website = company.Website
            };

            return Ok(result);
        }


        // GET: api/Companies
        [HttpGet("{isin}")]
        public async Task<IActionResult> GetByIsin(string isin)
        {
            var company = _repository.GetByIsin(isin);

            if (company == null)
            {
                return Ok();
            }

            var result = new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                StockTicker = company.StockTicker,
                Exchange = company.Exchange,
                Isin = company.Isin,
                Website = company.Website
            };

            return Ok(result);
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCompanyDTO request)
        {
            var newCompany = new Company(request.Name, request.StockTicker, request.Exchange, request.Isin, request.Website);

            ValidationResult valResult = await _validator.ValidateAsync(newCompany);

            if (_repository.GetByIsin(request.Isin) != null)
            {
                return Conflict($"Company already exists with Isin: {request.Isin}");
            }

            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Errors);
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

        // PUT: api/Companies
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CompanyDTO request)
        {
            var updatedCompany = new Company(request.Name, request.StockTicker, request.Exchange, request.Isin, request.Website);

            var companyToUpdate = _repository.GetById(request.Id);

            if (companyToUpdate == null)
            {
                return NoContent();
            }

            ValidationResult valResult = await _validator.ValidateAsync(updatedCompany);

            if (_repository.GetByIsin(request.Isin) != null)
            {
                return Conflict($"Company already exists with Isin: {request.Isin}");
            }

            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Errors);
            }

            _repository.Update(companyToUpdate, updatedCompany);

            var result = new CompanyDTO
            {
                Id = updatedCompany.Id,
                Name = updatedCompany.Name,
                StockTicker = updatedCompany.StockTicker,
                Exchange = updatedCompany.Exchange,
                Isin = updatedCompany.Isin,
                Website = updatedCompany.Website
            };

            return Ok(result);
        }
    }
}
