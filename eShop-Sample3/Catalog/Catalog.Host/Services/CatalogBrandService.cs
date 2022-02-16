using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests.BrandRequests;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IMapper mapper,
            ICatalogBrandRepository catalogBrandRepository)
            : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _catalogBrandRepository = catalogBrandRepository;
        }

        public async Task<int?> Add(CreateBrandRequest brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var res = _mapper.Map<CatalogBrand>(brand);
                return await _catalogBrandRepository.Add(res);
            });
        }

        public async Task<int?> Remove(int brandId) => await ExecuteSafeAsync(async () => await _catalogBrandRepository.Remove(brandId));

        public async Task<int?> Update(UpdateBrandRequest brand)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var res = _mapper.Map<CatalogBrand>(brand);
                return await _catalogBrandRepository.Update(res);
            });
        }
    }
}
