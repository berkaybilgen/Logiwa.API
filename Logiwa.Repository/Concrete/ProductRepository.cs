using Logiwa.Data;
using Logiwa.Data.Entities;
using Logiwa.Domain.Models;
using Logiwa.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logiwa.Repository.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private LogiwaDbContext _dbContext;
        private ILogger<ProductRepository> _logger;

        public ProductRepository(LogiwaDbContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Create(Product product)
        {
            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                return product.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return 0;
        }

        public async Task<bool> Update(ProductModel productModel)
        {
            try
            {
                var product = await _dbContext.Products.Where(p => p.Id == productModel.Id).FirstOrDefaultAsync();

                if (product == null)
                    return false;

                product.Name = productModel.Name;
                product.Description = productModel.Description;
                product.CategoryId = productModel.CategoryId;
                product.StockQuantity = productModel.StockQuantity;

                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return false;
        }

        public async Task<bool> Delete(int productId)
        {
            try
            {
                var product = await _dbContext.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

                if (product == null)
                    return false;

                product.IsDeleted = true;

                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return false;
        }

        public async Task<bool> IsValid(int productId)
        {
            try
            {
                return await _dbContext.Products.AnyAsync(p => p.Id == productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return false;
        }

        public async Task<IList<ProductModel>> GetProducts()
        {
            try
            {
                return await _dbContext.Products.Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    StockQuantity = p.StockQuantity
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<ProductModel> GetById(int id)
        {
            try
            {
                return await _dbContext.Products.Where(p => p.Id == id).Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    StockQuantity = p.StockQuantity
                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return null;
        }
    }
}
