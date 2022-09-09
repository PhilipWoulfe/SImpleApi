using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleApi.Api.Api;
using SimpleApi.Api.ApiModels;
using SimpleApi.Core.Interfaces;
using SimpleApi.Core.ProjectAggregate;
using SimpleApi.Core.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using FluentValidation.Results;

namespace SimpleApi.UnitTests.ControllerTests
{

    public class CompaniesControllerTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly CompaniesController _controller;
        private readonly CompanyValidator _validator;


        public CompaniesControllerTests()
        {
            _validator = new CompanyValidator();
            _mockRepo = new Mock<ICompanyRepository>();
            _controller = new CompaniesController(_mockRepo.Object, _validator);
            
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

        [Fact]
        public async void Post_Validation_NameCannotBeNull()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = null,
                Exchange = "zz",
                StockTicker = "zz",
                Isin = "zz"
            });
                       
            ValidateResults(result, "'Name' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_NameCannotBeEmpty()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "",
                Exchange = "zz",
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Name' must not be empty.");            
        }

        [Fact]
        public async void Post_Validation_ExchangeCannotBeNull()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = null,
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Exchange' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_ExchangeCannotBeEmpty()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "",
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Exchange' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_StockTickerCannotBeNull()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "z",
                StockTicker = null,
                Isin = "zz"
            });

            ValidateResults(result, "'Stock Ticker' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_StockTickerCannotBeEmpty()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "zz",
                StockTicker = "",
                Isin = "zz"
            });

            ValidateResults(result, "'Stock Ticker' must not be empty.");
        }


        [Fact]
        public async void Post_Validation_IsinCannotBeNull()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "z",
                StockTicker = "zz",
                Isin = null
            });

            ValidateResults(result, "'Isin' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_IsinCannotBeEmpty()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "zz",
                StockTicker = "zz",
                Isin = ""
            });

            ValidateResults(result, "'Isin' must not be empty.");
        }

        [Fact]
        public async void Post_Validation_IsinFormat()
        {
            var result = await _controller.Post(new CreateCompanyDTO
            {
                Name = "zz",
                Exchange = "z",
                StockTicker = "zz",
                Isin = "12ABCDERS"
            });

            ValidateResults(result, "'Isin' is not in the correct format.");           
        }



        [Fact]
        public async void Put_Validation_CantAddSameIsin()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = It.IsAny<string>(),
                Exchange = It.IsAny<string>(),
                StockTicker = It.IsAny<string>(),
                Isin = "US0378331005"
            });

            var conflictObjectResult = result as ConflictObjectResult;

            Assert.NotNull(conflictObjectResult);
            Assert.Equal(409, conflictObjectResult.StatusCode);

        }

        [Fact]
        public async void Put_Validation_NameCannotBeNull()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = null,
                Exchange = "zz",
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Name' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_NameCannotBeEmpty()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "",
                Exchange = "zz",
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Name' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_ExchangeCannotBeNull()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = null,
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Exchange' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_ExchangeCannotBeEmpty()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "",
                StockTicker = "zz",
                Isin = "zz"
            });

            ValidateResults(result, "'Exchange' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_StockTickerCannotBeNull()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "z",
                StockTicker = null,
                Isin = "zz"
            });

            ValidateResults(result, "'Stock Ticker' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_StockTickerCannotBeEmpty()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "zz",
                StockTicker = "",
                Isin = "zz"
            });

            ValidateResults(result, "'Stock Ticker' must not be empty.");
        }


        [Fact]
        public async void Put_Validation_IsinCannotBeNull()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "z",
                StockTicker = "zz",
                Isin = null
            });

            ValidateResults(result, "'Isin' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_IsinCannotBeEmpty()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "zz",
                StockTicker = "zz",
                Isin = ""
            });

            ValidateResults(result, "'Isin' must not be empty.");
        }

        [Fact]
        public async void Put_Validation_IsinFormat()
        {
            var result = await _controller.Put(new CompanyDTO
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "zz",
                Exchange = "z",
                StockTicker = "zz",
                Isin = "12ABCDERS"
            });

            ValidateResults(result, "'Isin' is not in the correct format.");
        }

        private void ValidateResults(IActionResult result, string expectedError)
        {
            var objectResult = result as BadRequestObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(400, objectResult.StatusCode);
            Assert.NotNull(objectResult.Value);

            var valRes = objectResult.Value as List<ValidationFailure>;
            var valResStr = new List<string>();

            Assert.NotNull(valRes);

            foreach (var val in valRes)
            {
                valResStr.Add(val.ToString());
            }

            Assert.Contains<string>(expectedError, valResStr);
        }

        [Fact]
        public async void GetAll_Validation_ReturnsAll()
        {
            var result = await _controller.List();

            Assert.NotNull(result);
            
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CompanyDTO>>(objectResult.Value);
            Assert.Equal(5, model.Count());
        }

        [Fact]
        public async void GetAll_Validation_Returns200OnNoResults()
        {
            var result = await _controller.List();

            Assert.NotNull(result);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async void GetById_Validation()
        {
            var result = await _controller.GetById(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            Assert.NotNull(result);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CompanyDTO>(objectResult.Value);
            Assert.Equal("NASDAQ", model.Exchange);
        }

        [Fact]
        public async void GetById_Validation_Returns200OnNoResult()
        {
            var result = await _controller.GetById(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaac"));

            Assert.NotNull(result);

            var objectResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async void MAkeFakse()
        {
            Assert.True(false);
        }

        private void SetUpRepository()
        {
            IList<Company> companies = new List<Company>
            {
                new Company("Apple Inc.", "AAPL", "NASDAQ", "US0378331005", "http://www.apple.com"),
                new Company("British Airways Plc", "BAIRY", "Pink Sheets", "US1104193065"),
                new Company("Heineken NV", "HEIA", "Euronext Amsterdam", "NL0000009165"),
                new Company("Panasonic Corp", "6752", "Tokyo Stock Exchange", "JP3866800000", "http://www.panasonic.co.jp"),
                new Company("Porsche Automobil", "PAH3", "Deutsche Börse", "DE000PAH0038", "https://www.porsche.com/")
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