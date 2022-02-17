using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.BrandRequests;
using Catalog.Host.Models.Requests.ItemRequests;

namespace Catalog.UnitTests.Services
{
    public class CatalogBrandServiceTest
    {
        private readonly ICatalogBrandService _catalogBrandService;

        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogBrandService>> _logger;

        public CatalogBrandServiceTest()
        {
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogBrandService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogBrandService = new CatalogBrandService(
                _dbContextWrapper.Object,
                _logger.Object,
                _mapper.Object,
                _catalogBrandRepository.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;
            var catalogBrand = new CatalogBrand { Brand = "brand" };
            var catalogBrandRequest = new CreateBrandRequest { Brand = "brand" };

            _catalogBrandRepository.Setup(s => s.Add(
                It.IsAny<CatalogBrand>())).ReturnsAsync(testResult);

            _mapper.Setup(s => s.Map<CreateBrandRequest>(
                It.Is<CatalogBrand>(b => b.Equals(catalogBrand)))).Returns(catalogBrandRequest);

            // act
            var result = await _catalogBrandService.Add(catalogBrandRequest);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            // arrange
            int? testResult = null;
            var catalogBrandRequest = new CreateBrandRequest { Brand = "brand" };

            _catalogBrandRepository.Setup(s => s.Add(
                It.IsAny<CatalogBrand>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Add(catalogBrandRequest);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Remove_Success()
        {
            // arrage
            var testResult = 1;

            _catalogBrandRepository.Setup(s => s.Remove(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Remove(testResult);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Remove_Failed()
        {
            // arrage
            var testId = 1;
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.Remove(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Remove(testId);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Success()
        {
            // arrage
            int? testResult = 1;

            _catalogBrandRepository.Setup(s => s.Update(
                It.IsAny<CatalogBrand>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Update(new UpdateBrandRequest { Brand = "test" });

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task Update_Failed()
        {
            // arrage
            int? testResult = null;

            _catalogBrandRepository.Setup(s => s.Update(
                It.IsAny<CatalogBrand>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogBrandService.Update(new UpdateBrandRequest { Brand = "test" });

            // assert
            result.Should().Be(testResult);
        }
    }
}