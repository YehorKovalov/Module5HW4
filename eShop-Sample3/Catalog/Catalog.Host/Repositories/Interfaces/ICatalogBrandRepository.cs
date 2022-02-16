using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrand>> GetBrandesAsync();
        Task<int?> Update(CatalogBrand brand);
        Task<int?> Add(CatalogBrand brand);
        Task<int?> Remove(int brandId);
    }
}