using System.Net;
using Catalog.Host.Models.Requests.TypeRequests;
using Catalog.Host.Models.Response.TypeResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<CatalogTypeController> _logger;

    public CatalogTypeController(
        ILogger<CatalogTypeController> logger,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateTypeRequest request)
    {
        var res = await _catalogTypeService.Add(request);
        return Ok(new AddTypeResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(UpdateTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        var res = await _catalogTypeService.Update(request);
        return Ok(new UpdateTypeResponse<int?>() { Id = res });
    }

    [HttpPost]
    [ProducesResponseType(typeof(RemoveTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Remove(int typeId)
    {
        var res = await _catalogTypeService.Remove(typeId);
        return Ok(new RemoveTypeResponse<int?>() { Id = res });
    }
}