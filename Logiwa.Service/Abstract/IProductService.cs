using Logiwa.Domain.Models;
using Logiwa.Domain.Responses;

namespace Logiwa.Service.Abstract
{
    public interface IProductService
    {
        Task<ServiceResult<int>> Create(ProductModel product);
        Task<ServiceResult<bool>> Update(ProductModel product);
        Task<ServiceResult<bool>> Delete(int productId);
        Task<ServiceResult<IList<ProductModel>>> GetProducts();
        Task<ServiceResult<ProductModel>> GetById(int id);
    }
}
