using FluentValidation;
using SimpleApi.Core.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Core.Validators
{
    public class CompanyValidator: AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.Name)
                .NotEmpty();

            RuleFor(company => company.StockTicker)
                .NotEmpty();

            RuleFor(company => company.Isin)
                .NotEmpty();

            RuleFor(company => company.Isin)
                .Matches("^[A-Za-z]{2}[A-Za-z0-9]*");

            RuleFor(company => company.Exchange)
                .NotEmpty();    
        }
    }
}
