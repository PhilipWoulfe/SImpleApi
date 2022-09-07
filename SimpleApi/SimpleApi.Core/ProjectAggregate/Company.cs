using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Core.ProjectAggregate
{
    public class Company : BaseEntity
    {
        public Company (string name, string stockTicker, string exchange, string isin, string? website = null)
        {
            Name = name;
            StockTicker = stockTicker;
            Exchange = exchange;
            Isin = isin;
            Website = website;
        }

        public void UpdateCompany(Company company)
        {
            Name = company.Name;
            StockTicker = company.StockTicker;
            Exchange = company.Exchange;
            Isin = company.Isin;
            Website = company.Website;
        }

        public string Name { get; private set; } 

        public string StockTicker { get; private set; }

        public string Exchange { get; private set; }

        public string Isin { get; private set; }

        public string? Website { get; private set; }
    }
}
