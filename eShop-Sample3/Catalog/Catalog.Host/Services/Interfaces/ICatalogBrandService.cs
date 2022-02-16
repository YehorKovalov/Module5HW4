using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.BrandRequests;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Update(UpdateBrandRequest brand);
        Task<int?> Add(CreateBrandRequest brand);
        Task<int?> Remove(int brandId);
    }
}