using System.Net;
using Catalog.Host.Models.Requests.BrandRequests;
using Catalog.Host.Models.Response.BrandResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(
        ILogger<CatalogBrandController> logger,
        ICatalogBrandService catalogBrandService)
    {
        _logger = logger;
        _catalogBrandService = catalogBrandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateBrandRequest request)
    {
        var res = await _catalogBrandService.Add(request);
        return Ok(new AddBrandResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var res = await _catalogBrandService.Update(request);
        return Ok(new UpdateBrandResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(int brandId)
    {
        var res = await _catalogBrandService.Remove(brandId);
        return Ok(new RemoveBrandResponse<int?>() { Id = res });
    }
}