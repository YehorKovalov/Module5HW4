using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task<int?> Update(CatalogType type);
        Task<int?> Add(CatalogType type);
        Task<int?> Remove(int typeId);
    }
}