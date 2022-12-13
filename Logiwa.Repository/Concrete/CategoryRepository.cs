using Logiwa.Data;
using Logiwa.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logiwa.Repository.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private LogiwaDbContext _dbContext;
        private ILogger<CategoryRepository> _logger;

        public CategoryRepository(LogiwaDbContext dbContext, ILogger<CategoryRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> IsValid(int categoryId)
        {
            try
            {
                return await _dbContext.Categories.AnyAsync(cat => cat.Id == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return false;
        }

        public async Task<int> GetMinimumStockQuantity(int categoryId)
        {
            try
            {
                return await _dbContext.Categories.Where(cat => cat.Id == categoryId).Select(cat => cat.MinimumStockQuantity).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return 0;
        }
    }
}
