using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.BrandRequests;
using Catalog.Host.Models.Requests.TypeRequests;

namespace Catalog.UnitTests.Services
{
    public class CatalogTypeServiceTest
    {
        private readonly ICatalogTypeService _catalogTypeService;

        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;

        public CatalogTypeServiceTest()
        {
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogTypeService = new CatalogTypeService(
                _dbContextWrapper.Object,
                _logger.Object,
                _mapper.Object,
                _catalogTypeRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;
            var catalogType = new CatalogType { Type = "brand" };
            var catalogTypeRequest = new CreateTypeRequest { Type = "brand" };

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<CatalogType>())).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CreateTypeRequest>(
                It.Is<CatalogType>(b => b.Equals(catalogType)))).Returns(catalogTypeRequest);

            // act
            var result = await _catalogTypeService.Add(catalogTypeRequest);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            // arrange
            int? testResult = null;
            var catalogTypeRequest = new CreateTypeRequest { Type = "brand" };

            _catalogTypeRepository.Setup(s => s.Add(
                It.IsAny<CatalogType>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Add(catalogTypeRequest);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Remove_Success()
        {
            // arrage
            var testResult = 1;

            _catalogTypeRepository.Setup(s => s.Remove(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Remove(testResult);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Failed()
        {
            // arrage
            var testId = 1;
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Remove(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Remove(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Success()
        {
            // arrage
            int? testResult = 1;

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<CatalogType>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Update(new UpdateTypeRequest { Type = "test" });

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Failed()
        {
            // arrage
            int? testResult = null;

            _catalogTypeRepository.Setup(s => s.Update(
                It.IsAny<CatalogType>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogTypeService.Update(new UpdateTypeRequest { Type = "test" });

            // assert
            result.Should().Be(testResult);
        }
    }
}
