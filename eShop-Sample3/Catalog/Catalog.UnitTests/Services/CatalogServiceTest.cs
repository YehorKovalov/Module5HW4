using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.ItemResponses;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(
            _dbContextWrapper.Object,
            _logger.Object,
            _catalogItemRepository.Object,
            _catalogBrandRepository.Object,
            _catalogTypeRepository.Object,
            _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Success()
    {
        // arrange
        var testId = 1;
        var catalogItemDto = new CatalogItemDto { Name = "test" };
        var catalogItem = new CatalogItem { Name = "test" };

        _catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(catalogItem);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem)))).Returns(catalogItemDto);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Item.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Failed()
    {
        // arrange
        var testId = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemByIdAsync(
            It.Is<int>(i => i == testId))).Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Success()
    {
        // arrange
        var testId = 1;
        var catalogs = new List<CatalogItem> { new CatalogItem { Name = "test" } };

        var catalogItem = new CatalogItem { Name = "test" };
        var catalogItemDto = new CatalogItemDto { Name = "test" };

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByBrandAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(catalogs);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem)))).Returns(catalogItemDto);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByBrandAsync_Failed()
    {
        // arrange
        var testId = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByBrandAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync((Func<List<CatalogItem>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsByBrandAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Success()
    {
        // arrange
        var testId = 1;
        var catalogs = new List<CatalogItem> { new CatalogItem { Name = "test" } };
        var catalogItemDto = new CatalogItemDto { Name = "test" };
        var catalogItem = new CatalogItem { Name = "test" };

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByTypeAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(catalogs);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItem)))).Returns(catalogItemDto);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByTypeAsync_Failed()
    {
        // arrange
        var testId = 1;

        _catalogItemRepository.Setup(s => s.GetCatalogItemsByTypeAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync((Func<List<CatalogItem>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsByTypeAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCalalogBrandesAsync_Success()
    {
        // arrange
        var brands = new List<CatalogBrand> { new CatalogBrand { Brand = "brand" } };
        var brand = new CatalogBrand { Brand = "brand" };
        var brandDto = new CatalogBrandDto { Brand = "brand" };
        _catalogBrandRepository.Setup(s => s.GetBrandesAsync()).ReturnsAsync(brands);

        _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(b => b.Equals(brand)))).Returns(brandDto);

        // act
        var result = await _catalogService.GetCalalogBrandesAsync();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCalalogBrandesAsync_Failed()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.GetBrandesAsync()).ReturnsAsync((Func<List<CatalogBrand>>)null!);

        // act
        var result = await _catalogService.GetCalalogBrandesAsync();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Success()
    {
        // arrange
        var types = new List<CatalogType> { new CatalogType { Type = "Type" } };
        var type = new CatalogType { Type = "Type" };
        var typeDto = new CatalogTypeDto { Type = "Type" };
        _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync(types);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(b => b.Equals(type)))).Returns(typeDto);

        // act
        var result = await _catalogService.GetCalalogBrandesAsync();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Failed()
    {
        // arrange
        _catalogTypeRepository.Setup(s => s.GetTypesAsync()).ReturnsAsync((Func<List<CatalogType>>)null!);

        // act
        var result = await _catalogService.GetCatalogTypesAsync();

        // assert
        result.Should().BeNull();
    }
}