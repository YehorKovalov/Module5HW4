using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.TypeRequests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IMapper mapper,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _catalogTypeRepository = catalogTypeRepository;
        }

        public async Task<int?> Add(CreateTypeRequest type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var res = _mapper.Map<CatalogType>(type);
                return await _catalogTypeRepository.Add(res);
            });
        }

        public async Task<int?> Remove(int typeId) => await ExecuteSafeAsync(async () => await _catalogTypeRepository.Remove(typeId));

        public async Task<int?> Update(UpdateTypeRequest type)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var res = _mapper.Map<CatalogType>(type);
                return await _catalogTypeRepository.Update(res);
            });
        }
    }
}
