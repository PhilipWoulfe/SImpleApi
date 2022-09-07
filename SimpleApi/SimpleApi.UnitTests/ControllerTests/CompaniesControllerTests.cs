

using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApi.Api.Api;
using SimpleApi.Api.ApiModels;
using SimpleApi.Core.Interfaces;
using SimpleApi.Core.ProjectAggregate;

namespace SimpleApi.UnitTests.ControllerTests
{

    public class CompaniesControllerTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly CompaniesController _controller;


        public CompaniesControllerTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _controller = new CompaniesController(_mockRepo.Object);
            
            SetUpRepository();
        }

        [Fact]
        public async void Post_Validation_CantAddSameIsin()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = It.IsAny<string>(),
                Exchange = It.IsAny<string>(),
                StockTicker = It.IsAny<string>(),
                Isin = "US0378331005"
            });

            var conflictObjectResult = result as ConflictObjectResult;

            Assert.NotNull(conflictObjectResult);
            Assert.Equal(409, conflictObjectResult.StatusCode);

        }

        private void SetUpRepository()
        {
            IList<Company> companies = new List<Company>
            {
                new Company("Apple Inc.", "NASDAQ", "AAPL", "US0378331005", "http://www.apple.com"),
                new Company("British Airways Plc", "Pink Sheets", "BAIRY", "US1104193065"),
                new Company("Heineken NV", "Euronext Amsterdam", "HEIA", "NL0000009165"),
                new Company("Panasonic Corp", "Tokyo Stock Exchange", "6752", "JP3866800000", "http://www.panasonic.co.jp"),
                new Company("Porsche Automobil", "Deutsche Börse", "PAH3", "DE000PAH0038", "https://www.porsche.com/")
            };


            _mockRepo.Setup(x => x.GetAll()).Returns(companies);

            _mockRepo.Setup(x => x.GetById(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"))).Returns(companies[0]);
            _mockRepo.Setup(x => x.GetById(new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"))).Returns(companies[1]);
            _mockRepo.Setup(x => x.GetById(new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"))).Returns(companies[2]);
            _mockRepo.Setup(x => x.GetById(new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"))).Returns(companies[3]);
            _mockRepo.Setup(x => x.GetById(new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"))).Returns(companies[4]);

            _mockRepo.Setup(x => x.GetByIsin("US0378331005")).Returns(companies[0]);
            _mockRepo.Setup(x => x.GetByIsin("US1104193065")).Returns(companies[1]);
            _mockRepo.Setup(x => x.GetByIsin("NL0000009165")).Returns(companies[2]);
            _mockRepo.Setup(x => x.GetByIsin("JP3866800000")).Returns(companies[3]);
            _mockRepo.Setup(x => x.GetByIsin("DE000PAH0038")).Returns(companies[4]);
        }
    }
}
