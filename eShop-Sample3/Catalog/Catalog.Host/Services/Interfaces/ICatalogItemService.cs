using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.ItemRequests;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Update(UpdateItemRequest item);
    Task<int?> Remove(int itemId);
}