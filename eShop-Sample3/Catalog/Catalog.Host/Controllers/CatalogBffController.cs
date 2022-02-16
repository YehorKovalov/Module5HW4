using System.Net;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.ItemRequests;
using Catalog.Host.Models.Response.BrandResponses;
using Catalog.Host.Models.Response.ItemResponses;
using Catalog.Host.Models.Response.TypeResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByIdResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemById(int id)
    {
        var result = await _catalogService.GetCatalogItemByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByIdResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemsByBrand(int brandId)
    {
        var result = await _catalogService.GetCatalogItemsByBrandAsync(brandId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByIdResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetItemsByType(int id)
    {
        var result = await _catalogService.GetCatalogItemsByTypeAsync(id);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetBrandesResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrandes()
    {
        var result = await _catalogService.GetCalalogBrandesAsync();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetTypesResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _catalogService.GetCatalogTypesAsync();
        return Ok(result);
    }
}