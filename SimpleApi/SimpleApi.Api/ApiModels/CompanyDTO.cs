namespace SimpleApi.Api.ApiModels
{
    public class CompanyDTO : CreateCompanyDTO
    {
        public Guid Id { get; set; }
    }

    public class CreateCompanyDTO
    {
        public string Name { get; set; }

        public string StockTicker { get; set; }

        public string Exchange { get; set; }

        public string Isin { get; set; }

        public string? Website { get; set; }
    }
}
