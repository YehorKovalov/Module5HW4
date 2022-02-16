using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            return await _dbContext.CatalogTypes
                .OrderBy(b => b.Type)
                .ToListAsync();
        }

        public async Task<int?> Add(CatalogType type)
        {
            if (type == null)
            {
                return null;
            }

            var result = await _dbContext.AddAsync(type);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int?> Remove(int typeId)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(c => c.Id == typeId);
            if (type == null)
            {
                return null;
            }

            var result = _dbContext.CatalogTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int?> Update(CatalogType type)
        {
            if (type == null)
            {
                return null;
            }

            var result = _dbContext.Update(type);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
