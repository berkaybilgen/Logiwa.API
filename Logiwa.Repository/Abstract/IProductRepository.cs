using Logiwa.Data.Entities;
using Logiwa.Domain.Models;

namespace Logiwa.Repository.Abstract
{
    public interface IProductRepository
    {
        Task<int> Create(Product product);
        Task<bool> Update(ProductModel productModel);
        Task<bool> Delete(int productId);
        Task<bool> IsValid(int productId);
        Task<IList<ProductModel>> GetProducts();
        Task<ProductModel> GetById(int id);
        Task<IList<ProductModel>> FilterByKeyword(string keyword);
        Task<IList<ProductModel>> FilterStockQuantityRange(int minValue, int maxValue);
    }
}
