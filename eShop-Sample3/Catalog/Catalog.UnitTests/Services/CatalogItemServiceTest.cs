using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.ItemRequests;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogItemService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Name = "Name",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogTypeId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogItemService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(
            _dbContextWrapper.Object,
            _logger.Object,
            _catalogItemRepository.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Name, _testItem.Description, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogTypeId, _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(
            _testItem.Name,
            _testItem.Description,
            _testItem.Price,
            _testItem.AvailableStock,
            _testItem.CatalogBrandId,
            _testItem.CatalogTypeId,
            _testItem.PictureFileName);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Success()
    {
        // arrage
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Remove(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Remove(testResult);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Remove_Failed()
    {
        // arrage
        var testId = 1;
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Remove(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Remove(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Success()
    {
        // arrage
        int? testResult = 1;

        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<CatalogItem>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Update(new UpdateItemRequest
        {
            Name = _testItem.Name,
            Description = _testItem.Description,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrandId = _testItem.CatalogBrandId,
            Price = _testItem.Price
        });

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task Update_Failed()
    {
        // arrage
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<CatalogItem>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Update(new UpdateItemRequest
        {
            Name = _testItem.Name,
            Description = _testItem.Description,
            AvailableStock = _testItem.AvailableStock,
            PictureFileName = _testItem.PictureFileName,
            CatalogTypeId = _testItem.CatalogTypeId,
            CatalogBrandId = _testItem.CatalogBrandId,
            Price = _testItem.Price
        });

        // assert
        result.Should().Be(testResult);
    }
}