using Logiwa.Data;
using Logiwa.Data.Entities;
using Logiwa.Repository.Abstract;
using Logiwa.Repository.Concrete;
using Logiwa.Service.Abstract;
using Logiwa.Service.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Logiwa.UnitTests
{
    public class Tests
    {
        private LogiwaDbContext _logiwaDbContext;
        private IProductService _productService;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private Mock<ILogger<ProductRepository>> _loggerProductRepositoryMock;
        private Mock<ILogger<CategoryRepository>> _loggerCategoryRepositoryMock;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LogiwaDbContext>()
            .UseInMemoryDatabase(databaseName: "Logiwa")
            .Options;

            using (var context = new LogiwaDbContext(options))
            {
                context.Categories.Add(new Category { Id = 1, Name = "Test category", MinimumStockQuantity = 20 });
                context.Products.Add(new Product { Id = 1, Name = "Berkay first product", Description = "test", CategoryId = 1, StockQuantity = 30 });
                context.Products.Add(new Product { Id = 2, Name = "new product", Description = "testasd rtr", CategoryId = 1, StockQuantity = 130 });
                context.Products.Add(new Product { Id = 3, Name = "last one", Description = "test dsfsrwr", CategoryId = 1, StockQuantity = 320 });
                context.SaveChanges();
            }

            _logiwaDbContext = new LogiwaDbContext(options);

            _loggerProductRepositoryMock = new Mock<ILogger<ProductRepository>>();
            _loggerCategoryRepositoryMock = new Mock<ILogger<CategoryRepository>>();
            _productRepository = new ProductRepository(_logiwaDbContext, _loggerProductRepositoryMock.Object);
            _categoryRepository = new CategoryRepository(_logiwaDbContext, _loggerCategoryRepositoryMock.Object);
            _productService = new ProductService(_productRepository, _categoryRepository);
        }

        [Test]
        [TestCase("", "Name is empty", 1, 50, 0)]
        [TestCase("CategoryId is 0", "New product description", 0, 50, 0)]
        [TestCase("Unavailable categoryId", "New product description", 110, 50, 0)]
        [TestCase("Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long. Title is too long.", "There are 208 characters.", 1, 150, 0)]
        [TestCase("Stock quantity is less", "New product description", 1, 5, 0)]
        [TestCase("Successful product", "New product description", 1, 150, 4)]
        public async Task Create(string name, string description, int categoryId, int stockQuantity, int expectedResult)
        {
            var result = await _productService.Create(new Domain.Models.ProductModel { Name = name, Description = description, CategoryId = categoryId, StockQuantity = stockQuantity });

            Assert.That(expectedResult, Is.EqualTo(result.Data));

            Assert.Pass();
        }
    }
}