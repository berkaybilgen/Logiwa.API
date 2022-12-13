using Logiwa.Data.Entities;
using Logiwa.Domain.Models;
using Logiwa.Domain.Responses;
using Logiwa.Repository.Abstract;
using Logiwa.Service.Abstract;

namespace Logiwa.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ServiceResult<int>> Create(ProductModel product)
        {
            var result = new ServiceResult<int>();

            var validationResult = await ValidateProductModel(product);

            if (!validationResult.IsValid)
            {
                result.AddError(validationResult.Errors.FirstOrDefault());
                return result;
            }

            result.Data = await _productRepository.Create(MapProduct(product));

            return result;
        }

        public async Task<ServiceResult<bool>> Update(ProductModel product)
        {
            var result = new ServiceResult<bool>();

            var validationResult = await ValidateProductModel(product);

            if (!validationResult.IsValid)
            {
                result.AddError(validationResult.Errors.FirstOrDefault());
                return result;
            }

            result.Data = await _productRepository.Update(product);

            return result;
        }

        public async Task<ServiceResult<bool>> Delete(int productId)
        {
            var result = new ServiceResult<bool>();

            if (!await _productRepository.IsValid(productId))
            {
                result.AddError("Product is not exist!");
                return result;
            }

            result.Data = await _productRepository.Delete(productId);

            return result;
        }

        public async Task<ServiceResult<IList<ProductModel>>> GetProducts()
        {
            var result = new ServiceResult<IList<ProductModel>>
            {
                Data = await _productRepository.GetProducts()
            };

            return result;
        }

        public async Task<ServiceResult<ProductModel>> GetById(int id)
        {
            var result = new ServiceResult<ProductModel>();

            if (id <= 0)
            {
                result.AddError("id parameter must be greater than 0!");
                return result;
            }

            if (!await _productRepository.IsValid(id))
            {
                result.AddError("Product is not exist!");
                return result;
            }

            result.Data = await _productRepository.GetById(id);

            return result;
        }

        private async Task<bool> IsValidProduct(int productId)
        {
            return await _productRepository.IsValid(productId);
        }

        private async Task<ValidationModel> ValidateProductModel(ProductModel product)
        {
            var validationResult = new ValidationModel();

            if (string.IsNullOrEmpty(product.Name))
            {
                validationResult.AddError("Product name cannot be null or empty!");
            }

            if (product.Name.Length > 200)
            {
                validationResult.AddError("Product name cannot be more than 200 character!");
            }

            if (product.CategoryId <= 0)
            {
                validationResult.AddError("Product must have a category!");
            }

            if (!await _categoryRepository.IsValid(product.CategoryId))
            {
                validationResult.AddError("Product must have an available category!");
            }

            var categoryMinimumStockQuantity = await _categoryRepository.GetMinimumStockQuantity(product.CategoryId);

            if (product.StockQuantity < categoryMinimumStockQuantity)
            {
                validationResult.AddError($"Stock quantity must be minimum {categoryMinimumStockQuantity}!");
            }

            return validationResult;
        }

        private Product MapProduct(ProductModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                CategoryId = productModel.CategoryId,
                StockQuantity = productModel.StockQuantity
            };

            if (productModel.Id.HasValue && productModel.Id > 0)
                product.Id = productModel.Id.Value;

            return product;
        }
    }
}
