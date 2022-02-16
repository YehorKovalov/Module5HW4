using System;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public Task<int?> Add(CatalogBrand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandesAsync()
        {
            return await _dbContext.CatalogBrands
                .OrderBy(b => b.Brand)
                .ToListAsync();
        }

        public async Task<int?> Remove(int brandId)
        {
            var brand = await _dbContext.CatalogBrands.FirstOrDefaultAsync(c => c.Id == brandId);

            if (brand == null)
            {
                return null;
            }

            var result = _dbContext.Update(brand);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int?> Update(CatalogBrand brand)
{
            if (brand == null)
            {
                return null;
            }

            var result = _dbContext.CatalogBrands.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
