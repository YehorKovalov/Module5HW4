using System.Net;
using Catalog.Host.Models.Requests.ItemRequests;
using Catalog.Host.Models.Response.ItemResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateProductRequest request)
    {
        var result = await _catalogItemService.Add(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItemRequest request)
    {
        var res = await _catalogItemService.Update(request);
        return Ok(new UpdateItemResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(int itemId)
    {
        var res = await _catalogItemService.Remove(itemId);
        return Ok(new RemoveItemResponse<int?>() { Id = res });
    }
}