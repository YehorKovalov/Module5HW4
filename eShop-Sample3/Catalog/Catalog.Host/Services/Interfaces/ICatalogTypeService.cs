using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.TypeRequests;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Update(UpdateTypeRequest type);
        Task<int?> Add(CreateTypeRequest type);
        Task<int?> Remove(int typeId);
    }
}